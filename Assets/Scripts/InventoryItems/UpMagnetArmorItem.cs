public class UpMagnetArmorItem : ArmorItem
{
	protected override void ApplyModifier()
	{
		PlayerStats.CurrentMagnet *= 1 + InventoryItemData.Multiplier / 100f;
	}
}