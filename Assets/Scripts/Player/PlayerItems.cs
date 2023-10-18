using UnityEngine;

public class PlayerItems : MonoBehaviour
{
	public bool[] HasItems = new bool[] { false, false, false, false, false, false};

	private int _currentItem;
	
	[SerializeField]
	private GameObject[] _armorEquip;

	public void Equip(int index)
	{
		if (HasItems[index])
		{
			_currentItem = index;
			_armorEquip[_currentItem].SetActive(true);
		}
	}

	public void AddItem(int index)
	{
		HasItems[index] = true;
	}
}