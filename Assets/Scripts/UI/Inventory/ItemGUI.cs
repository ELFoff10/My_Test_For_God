using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemGUI : MonoBehaviour, ISelectHandler
{
    public static event Action<InventoryItem> OnItemSelected;
    [SerializeField] private InventoryItem _item;
    [SerializeField] private Image _targetImage;

    public void OnSelect(BaseEventData eventData)
    {
        OnItemSelected?.Invoke(_item);
    }

    private void Start()
    {
        _targetImage.sprite = _item.ItemSprite;
    }
}
