using UnityEngine;

[CreateAssetMenu]
public class InventoryItem : ScriptableObject
{
    [SerializeField] 
    private string _name = "UnnamedItem";
    public string Name => _name;

    [SerializeField] 
    private string _description = "Nondescript";
    public string Description => _description;

    [SerializeField] 
    private Sprite _itemSprite;
    public Sprite ItemSprite => _itemSprite;
}
