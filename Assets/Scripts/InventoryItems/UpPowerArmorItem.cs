public class UpPowerArmorItem : ArmorItem
{
	protected override void ApplyModifier()
	{
		PlayerStats.CurrentMight *= 1 + InventoryItemData.Multiplier / 100f;
	}
}