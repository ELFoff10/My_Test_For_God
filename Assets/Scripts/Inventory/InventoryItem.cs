using UnityEngine;


[CreateAssetMenu]
public class InventoryItem : ScriptableObject
{
    [SerializeField] private string m_name = "UnnamedItem";
    public string Name { get => m_name; }
    
    [SerializeField] private string m_description = "Nondescript";
    public string Description { get => m_description; }

    [SerializeField] private Sprite m_itemSprite;
    public Sprite ItemSprite { get => m_itemSprite; }
}
