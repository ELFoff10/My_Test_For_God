using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ItemGUI : MonoBehaviour, ISelectHandler
{
    [SerializeField]
    private Button _deleteItemButton;
    [SerializeField] 
    private InventoryItem _item;

    public static event Action<InventoryItem> OnItemSelected;
    
    public void OnSelect(BaseEventData eventData)
    {
        OnItemSelected?.Invoke(_item);
        
        if (Input.GetMouseButtonDown(0))
        {
            _deleteItemButton.gameObject.SetActive(true);
        }
    }
}
