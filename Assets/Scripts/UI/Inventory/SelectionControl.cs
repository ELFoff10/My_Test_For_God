using UnityEngine;
using UnityEngine.UI;


public class SelectionControl : MonoBehaviour
{
    [SerializeField] private Selectable _startSelection;
    private void Start()
    {
        _startSelection.Select();
    }    
}
