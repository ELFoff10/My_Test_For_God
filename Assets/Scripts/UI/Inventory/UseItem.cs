using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
	[SerializeField]
	private Image _itemImage;
	[SerializeField]
	private Button _deleteButton;
	private InventoryItem _item;

	private void SetItem(InventoryItem item)
	{
		_item = item;
		_itemImage.sprite = item.ItemSprite;
	}

	private void Awake()
	{
		ItemGUI.OnItemSelected += SetItem;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_deleteButton.gameObject.SetActive(true);
		}
	}
}