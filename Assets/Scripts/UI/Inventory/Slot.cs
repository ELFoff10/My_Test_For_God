using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private Image _icon;

    public void UpdateSlot(bool active)
    {
        if (active)
        {
            _icon.sprite = _sprite;
        }
        else
        {
            _icon.sprite = null;
        }
    }
}
