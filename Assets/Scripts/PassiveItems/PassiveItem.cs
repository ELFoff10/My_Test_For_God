using UnityEngine;

public class PassiveItem : MonoBehaviour
{
	public PassiveItemScriptableObject PassiveItemData;
	protected PlayerStats PlayerStats;

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