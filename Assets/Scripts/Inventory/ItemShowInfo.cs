using UnityEngine;
using UnityEngine.UI;


public class ItemShowInfo : MonoBehaviour
{
    [SerializeField] private Text itemName, itemDescription;
    private void Awake()
    {
        ItemGUI.OnItemSelected += (InventoryItem ii) =>
        {
            itemName.text = ii.Name;
            itemDescription.text = ii.Description;
        };
    }
}
