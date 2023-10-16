using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        private TMP_Text _text;
        private WeaponController _weaponController;

        private void Start()
        {
            _weaponController = FindObjectOfType<WeaponController>();
            _weaponController.OnCartridgesUpdate += UpdateText;
            _text = GetComponent<TMP_Text>();
        }

        private void UpdateText()
        {
            _text.text = _weaponController.CurrentCartridges.ToString();
        }
    }
}

