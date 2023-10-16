public class UpPowerArmorItem : ArmorItem
{
	protected override void ApplyModifier()
	{
		PlayerStats.CurrentMight *= 1 + ArmorItemData.Multiplier / 100f;
	}
}