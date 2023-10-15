using UnityEngine;

public class ArmorItem : MonoBehaviour
{
	protected PlayerStats PlayerStats;
	public InventoryItemScriptableObject InventoryItemData;

	protected virtual void ApplyModifier()
	{
		// Apply the boost value to the appropriate stat in the child classes
	}

	private void Start()
	{
		PlayerStats = FindObjectOfType<PlayerStats>();
		ApplyModifier();
	}
}