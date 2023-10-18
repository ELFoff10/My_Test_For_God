using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
	[SerializeField]
	private Image _itemImage;

	private void SetItem(InventoryItem item)
	{
		_itemImage.sprite = item.ItemSprite;
	}

	private void Awake()
	{
		ItemGUI.OnItemSelected += SetItem;
	}
}