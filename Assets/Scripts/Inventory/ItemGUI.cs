using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class ItemGUI : MonoBehaviour, ISelectHandler
{
    public static event Action<InventoryItem> OnItemSelected;
    [SerializeField] private InventoryItem m_item;
    [SerializeField] private Image m_targetImage;

    public void OnSelect(BaseEventData eventData)
    {
        OnItemSelected?.Invoke(m_item);
    }

    private void Start()
    {
        m_targetImage.sprite = m_item.ItemSprite;
    }
}
