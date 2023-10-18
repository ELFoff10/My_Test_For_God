using UnityEngine;
using UnityEngine.UI;


public class SelectionControl : MonoBehaviour
{
    [SerializeField] private Selectable m_startSelection;
    private void Start()
    {
        m_startSelection.Select();
    }    
}
