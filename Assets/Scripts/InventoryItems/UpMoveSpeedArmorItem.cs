public class UpMoveSpeedArmorItem : ArmorItem
{
	protected override void ApplyModifier()
	{
		PlayerStats.CurrentMoveSpeed *= 1 + InventoryItemData.Multiplier / 100f;
	}
}