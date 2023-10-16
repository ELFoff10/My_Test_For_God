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
	public List<ArmorItem> ArmorItemSlots = new List<ArmorItem>(6);
	public int[] ArmorItemLevels = new int[6];
	public List<Image> ArmorItemUiSlots = new List<Image>(6);

	[Serializable]
	public class WeaponUpgrade
	{
		public int WeaponUpgradeIndex;
		public GameObject InitialWeapon;
		public WeaponScriptableObject WeaponData;
	}

	[Serializable]
	public class ArmorItemUpgrade
	{
		public int ArmorItemUpgradeIndex;
		public GameObject InitialArmorItem;
		public ArmorItemScriptableObject ArmorItemData;
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
	public List<ArmorItemUpgrade> ArmorItemUpgradeOptions = new List<ArmorItemUpgrade>();
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

	public void AddArmorItem(int slotIndex, ArmorItem armorItem)
	{
		ArmorItemSlots[slotIndex] = armorItem;
		ArmorItemLevels[slotIndex] = armorItem.ArmorItemData.Level;
		ArmorItemUiSlots[slotIndex].enabled = true;
		ArmorItemUiSlots[slotIndex].sprite = armorItem.ArmorItemData.Icon;

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

	private void LevelUpArmorItem(int slotIndex, int upgradeIndex)
	{
		if (ArmorItemSlots.Count > slotIndex)
		{
			var armorItemSlot = ArmorItemSlots[slotIndex];
			if (!armorItemSlot.ArmorItemData.NextLevelPrefab)
			{
				return;
			}

			var upgradeArmorItem = Instantiate(armorItemSlot.ArmorItemData.NextLevelPrefab, transform.position,
				Quaternion.identity);
			upgradeArmorItem.transform.SetParent(transform);
			AddArmorItem(slotIndex, upgradeArmorItem.GetComponent<ArmorItem>());
			Destroy(armorItemSlot.gameObject);
			ArmorItemLevels[slotIndex] = upgradeArmorItem.GetComponent<ArmorItem>().ArmorItemData.Level;

			ArmorItemUpgradeOptions[upgradeIndex].ArmorItemData =
				upgradeArmorItem.GetComponent<ArmorItem>().ArmorItemData;

			if (GameManager.Instance != null && GameManager.Instance.ChoosingUpgrade)
			{
				GameManager.Instance.EndLevelUp();
			}
		}
	}

	private void ApplyUpgradeOptions()
	{
		List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(WeaponUpgradeOptions);
		List<ArmorItemUpgrade> availableArmorItemUpgrades = new List<ArmorItemUpgrade>(ArmorItemUpgradeOptions);

		foreach (var upgradeOption in UpgradeUIOptions)
		{
			if (availableWeaponUpgrades.Count == 0 && availableArmorItemUpgrades.Count == 0)
			{
				return;
			}

			int upgradeType;

			if (availableWeaponUpgrades.Count == 0)
			{
				upgradeType = 2;
			}
			else if (availableArmorItemUpgrades.Count == 0)
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
				ArmorItemUpgrade chosenArmorItemUpgrade =
					availableArmorItemUpgrades[Random.Range(0, availableArmorItemUpgrades.Count)];

				availableArmorItemUpgrades.Remove(chosenArmorItemUpgrade);

				if (chosenArmorItemUpgrade != null)
				{
					EnableUpgradeUI(upgradeOption);

					bool newArmorItem = false;
					for (int i = 0; i < ArmorItemSlots.Count; i++)
					{
						if (ArmorItemSlots[i] != null && ArmorItemSlots[i].ArmorItemData ==
						    chosenArmorItemUpgrade.ArmorItemData)
						{
							newArmorItem = false;

							if (!newArmorItem)
							{
								if (!chosenArmorItemUpgrade.ArmorItemData.NextLevelPrefab)
								{
									DisableUpgradeUI(upgradeOption);

									break;
								}

								upgradeOption.UpgradeButton.onClick.AddListener(() =>
									LevelUpArmorItem(i, chosenArmorItemUpgrade.ArmorItemUpgradeIndex));

								upgradeOption.UpgradeDescriptionDisplay.text = chosenArmorItemUpgrade.ArmorItemData
									.NextLevelPrefab.GetComponent<ArmorItem>().ArmorItemData.Description;
								upgradeOption.UpgradeNameDisplay.text = chosenArmorItemUpgrade.ArmorItemData
									.NextLevelPrefab
									.GetComponent<ArmorItem>().ArmorItemData.Name;
							}

							break;
						}
						else
						{
							newArmorItem = true;
						}
					}

					if (newArmorItem)
					{
						upgradeOption.UpgradeButton.onClick.AddListener(() =>
							_player.SpawnArmorItem(chosenArmorItemUpgrade.InitialArmorItem));

						upgradeOption.UpgradeDescriptionDisplay.text =
							chosenArmorItemUpgrade.ArmorItemData.Description;
						upgradeOption.UpgradeNameDisplay.text = chosenArmorItemUpgrade.ArmorItemData.Name;
					}

					upgradeOption.UpgradeIcon.sprite = chosenArmorItemUpgrade.ArmorItemData.Icon;
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