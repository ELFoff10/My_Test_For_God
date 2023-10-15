public class UpProjectileSpeedArmorItem : ArmorItem
{
	protected override void ApplyModifier()
	{
		PlayerStats.CurrentProjectileSpeed *= 1 + InventoryItemData.Multiplier / 100f;
	}
}