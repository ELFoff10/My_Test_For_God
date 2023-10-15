using TMPro;
using UnityEngine;

public class ItemShowInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName, _itemDescription;
    private void Awake()
    {
        ItemGUI.OnItemSelected += (InventoryItem ii) =>
        {
            _itemName.text = ii.Name;
            _itemDescription.text = ii.Description;
        };
    }
}
