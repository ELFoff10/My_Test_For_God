public class UpHealthArmorItem : ArmorItem
{
	protected override void ApplyModifier()
	{
		PlayerStats.CurrentHealth *= 1 + ArmorItemData.Multiplier / 100f;
	}
}