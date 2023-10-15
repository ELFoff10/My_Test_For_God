public class UpHealthArmorItem : ArmorItem
{
	protected override void ApplyModifier()
	{
		PlayerStats.CurrentHealth *= 1 + InventoryItemData.Multiplier / 100f;
	}
}