using UnityEngine;
using UnityEngine.UI;


public class UseItem : MonoBehaviour
{
    private InventoryItem m_Item;
    [SerializeField] private Image itemImage;
    public void SetItem(InventoryItem item)
    {
        m_Item = item;
        itemImage.sprite = item.ItemSprite;
    }
    void Awake()
    {
        ItemGUI.OnItemSelected += SetItem;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            print($"{m_Item.Name} is used");
        }
    }
}
