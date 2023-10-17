using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private PlayerItems _playerItems;
    [SerializeField]
    private List<Slot> _slots;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    
    public void UpdateUI()
    {
        for (var i = 0; i < _slots.Count; i++)
        {
            var active = _playerItems.HasItems[i];

            _slots[i].UpdateSlot(active);
        }
    }
}
