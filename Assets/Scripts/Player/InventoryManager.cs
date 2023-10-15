using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InventoryManager : MonoBehaviour
{
	public List<WeaponController> WeaponSlots = new List<WeaponController>(6);
	public int[] WeaponLevels = new int[6];
	public List<Image> WeaponUiSlots = new List<Image>(6);
	public List<ArmorItem> PassiveItemSlots = new List<ArmorItem>(6);
	public int[] PassiveItemLevels = new int[6];
	public List<Image> PassiveItemUiSlots = new List<Image>(6);

	[Serializable]
	public class WeaponUpgrade
	{
		public int WeaponUpgradeIndex;
		public GameObject InitialWeapon;
		public WeaponScriptableObject WeaponData;
	}

	[Serializable]
	public class PassiveItemUpgrade
	{
		public int PassiveItemUpgradeIndex;
		public GameObject InitialPassiveItem;
		public InventoryItemScriptableObject InventoryItemData;
	}

	[Serializable]
	public class UpgradeUI
	{
		public TMP_Text UpgradeNameDisplay;
		public TMP_Text UpgradeDescriptionDisplay;
		public Image UpgradeIcon;
		public Button UpgradeButton;
	}

	public List<WeaponUpgrade> WeaponUpgradeOptions = new List<WeaponUpgrade>();
	public List<PassiveItemUpgrade> PassiveItemUpgradeOptions = new List<PassiveItemUpgrade>();
	public List<UpgradeUI> UpgradeUIOptions = new List<UpgradeUI>();

	private PlayerStats _player;

	private void Start()
	{
		_player = GetComponent<PlayerStats>();
	}

	public void AddWeapon(int slotIndex, WeaponController weapon)
	{
		WeaponSlots[slotIndex] = weapon;
		WeaponLevels[slotIndex] = weapon.WeaponData.Level;
		WeaponUiSlots[slotIndex].enabled = true;
		WeaponUiSlots[slotIndex].sprite = weapon.WeaponData.Icon;

		if (GameManager.Instance != null && GameManager.Instance.ChoosingUpgrade)
		{
			GameManager.Instance.EndLevelUp();
		}
	}

	public void AddPassiveItem(int slotIndex, ArmorItem armorItem)
	{
		PassiveItemSlots[slotIndex] = armorItem;
		PassiveItemLevels[slotIndex] = armorItem.InventoryItemData.Level;
		PassiveItemUiSlots[slotIndex].enabled = true;
		PassiveItemUiSlots[slotIndex].sprite = armorItem.InventoryItemData.Icon;

		if (GameManager.Instance != null && GameManager.Instance.ChoosingUpgrade)
		{
			GameManager.Instance.EndLevelUp();
		}
	}

	private void LevelUpWeapon(int slotIndex, int upgradeIndex)
	{
		if (WeaponSlots.Count > slotIndex)
		{
			var weapon = WeaponSlots[slotIndex];
			if (!weapon.WeaponData.NextLevelPrefab)
			{
				return;
			}

			var upgradeWeapon = Instantiate(weapon.WeaponData.NextLevelPrefab, transform.position, Quaternion.identity);
			upgradeWeapon.transform.SetParent(transform);
			AddWeapon(slotIndex, upgradeWeapon.GetComponent<WeaponController>());
			Destroy(weapon.gameObject);
			WeaponLevels[slotIndex] = upgradeWeapon.GetComponent<WeaponController>().WeaponData.Level;

			WeaponUpgradeOptions[upgradeIndex].WeaponData = upgradeWeapon.GetComponent<WeaponController>().WeaponData;

			if (GameManager.Instance != null && GameManager.Instance.ChoosingUpgrade)
			{
				GameManager.Instance.EndLevelUp();
			}
		}
	}

	private void LevelUpPassiveItem(int slotIndex, int upgradeIndex)
	{
		if (PassiveItemSlots.Count > slotIndex)
		{
			var passiveItem = PassiveItemSlots[slotIndex];
			if (!passiveItem.InventoryItemData.NextLevelPrefab)
			{
				return;
			}

			var upgradePassiveItem = Instantiate(passiveItem.InventoryItemData.NextLevelPrefab, transform.position,
				Quaternion.identity);
			upgradePassiveItem.transform.SetParent(transform);
			AddPassiveItem(slotIndex, upgradePassiveItem.GetComponent<ArmorItem>());
			Destroy(passiveItem.gameObject);
			PassiveItemLevels[slotIndex] = upgradePassiveItem.GetComponent<ArmorItem>().InventoryItemData.Level;

			PassiveItemUpgradeOptions[upgradeIndex].InventoryItemData =
				upgradePassiveItem.GetComponent<ArmorItem>().InventoryItemData;

			if (GameManager.Instance != null && GameManager.Instance.ChoosingUpgrade)
			{
				GameManager.Instance.EndLevelUp();
			}
		}
	}

	private void ApplyUpgradeOptions()
	{
		List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(WeaponUpgradeOptions);
		List<PassiveItemUpgrade> availablePassiveItemUpgrades = new List<PassiveItemUpgrade>(PassiveItemUpgradeOptions);

		foreach (var upgradeOption in UpgradeUIOptions)
		{
			if (availableWeaponUpgrades.Count == 0 && availablePassiveItemUpgrades.Count == 0)
			{
				return;
			}

			int upgradeType;

			if (availableWeaponUpgrades.Count == 0)
			{
				upgradeType = 2;
			}
			else if (availablePassiveItemUpgrades.Count == 0)
			{
				upgradeType = 1;
			}
			else
			{
				upgradeType = Random.Range(1, 3);
			}

			if (upgradeType == 1)
			{
				WeaponUpgrade chosenWeaponUpgrade =
					availableWeaponUpgrades[Random.Range(0, availableWeaponUpgrades.Count)];

				availableWeaponUpgrades.Remove(chosenWeaponUpgrade);

				if (chosenWeaponUpgrade != null)
				{
					EnableUpgradeUI(upgradeOption);

					bool newWeapon = false;

					for (int i = 0; i < WeaponSlots.Count; i++)
					{
						if (WeaponSlots[i] != null && WeaponSlots[i].WeaponData == chosenWeaponUpgrade.WeaponData)
						{
							newWeapon = false;
							if (!newWeapon)
							{
								if (!chosenWeaponUpgrade.WeaponData.NextLevelPrefab)
								{
									DisableUpgradeUI(upgradeOption);
									break;
								}

								upgradeOption.UpgradeButton.onClick.AddListener(() =>
									LevelUpWeapon(i, chosenWeaponUpgrade.WeaponUpgradeIndex));
								upgradeOption.UpgradeDescriptionDisplay.text = chosenWeaponUpgrade.WeaponData
									.NextLevelPrefab.GetComponent<WeaponController>().WeaponData.Description;
								upgradeOption.UpgradeNameDisplay.text = chosenWeaponUpgrade.WeaponData.NextLevelPrefab
									.GetComponent<WeaponController>().WeaponData.Name;
							}

							break;
						}
						else
						{
							newWeapon = true;
						}
					}

					if (newWeapon)
					{
						upgradeOption.UpgradeButton.onClick.AddListener(() =>
							_player.SpawnWeapon(chosenWeaponUpgrade.InitialWeapon));
						upgradeOption.UpgradeDescriptionDisplay.text = chosenWeaponUpgrade.WeaponData.Description;
						upgradeOption.UpgradeNameDisplay.text = chosenWeaponUpgrade.WeaponData.Name;
					}

					upgradeOption.UpgradeIcon.sprite = chosenWeaponUpgrade.WeaponData.Icon;
				}
			}
			else if (upgradeType == 2)
			{
				PassiveItemUpgrade chosenPassiveItemUpgrade =
					availablePassiveItemUpgrades[Random.Range(0, availablePassiveItemUpgrades.Count)];

				availablePassiveItemUpgrades.Remove(chosenPassiveItemUpgrade);

				if (chosenPassiveItemUpgrade != null)
				{
					EnableUpgradeUI(upgradeOption);

					bool newPassiveItem = false;
					for (int i = 0; i < PassiveItemSlots.Count; i++)
					{
						if (PassiveItemSlots[i] != null && PassiveItemSlots[i].InventoryItemData ==
						    chosenPassiveItemUpgrade.InventoryItemData)
						{
							newPassiveItem = false;

							if (!newPassiveItem)
							{
								if (!chosenPassiveItemUpgrade.InventoryItemData.NextLevelPrefab)
								{
									DisableUpgradeUI(upgradeOption);

									break;
								}

								upgradeOption.UpgradeButton.onClick.AddListener(() =>
									LevelUpPassiveItem(i, chosenPassiveItemUpgrade.PassiveItemUpgradeIndex));

								upgradeOption.UpgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.InventoryItemData
									.NextLevelPrefab.GetComponent<ArmorItem>().InventoryItemData.Description;
								upgradeOption.UpgradeNameDisplay.text = chosenPassiveItemUpgrade.InventoryItemData
									.NextLevelPrefab
									.GetComponent<ArmorItem>().InventoryItemData.Name;
							}

							break;
						}
						else
						{
							newPassiveItem = true;
						}
					}

					if (newPassiveItem)
					{
						upgradeOption.UpgradeButton.onClick.AddListener(() =>
							_player.SpawnPassiveItem(chosenPassiveItemUpgrade.InitialPassiveItem));

						upgradeOption.UpgradeDescriptionDisplay.text =
							chosenPassiveItemUpgrade.InventoryItemData.Description;
						upgradeOption.UpgradeNameDisplay.text = chosenPassiveItemUpgrade.InventoryItemData.Name;
					}

					upgradeOption.UpgradeIcon.sprite = chosenPassiveItemUpgrade.InventoryItemData.Icon;
				}
			}
		}
	}

	private void RemoveUpgradeOptions()
	{
		foreach (var upgradeOption in UpgradeUIOptions)
		{
			upgradeOption.UpgradeButton.onClick.RemoveAllListeners();
			DisableUpgradeUI(upgradeOption);
		}
	}

	public void RemoveAndApplyUpgrades()
	{
		RemoveUpgradeOptions();
		ApplyUpgradeOptions();
	}

	private void DisableUpgradeUI(UpgradeUI upgradeUI)
	{
		upgradeUI.UpgradeNameDisplay.transform.parent.gameObject.SetActive(false);
	}

	private void EnableUpgradeUI(UpgradeUI upgradeUI)
	{
		upgradeUI.UpgradeNameDisplay.transform.parent.gameObject.SetActive(true);
	}
}