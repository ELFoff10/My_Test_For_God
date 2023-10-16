public class UpRegenHpArmorItem : ArmorItem
{
	protected override void ApplyModifier()
	{
		PlayerStats.CurrentRegenHp *= 1 + ArmorItemData.Multiplier / 100f;
	}
}