public class UpRegenHpArmorItem : ArmorItem
{
	protected override void ApplyModifier()
	{
		PlayerStats.CurrentRegenHp *= 1 + InventoryItemData.Multiplier / 100f;
	}
}