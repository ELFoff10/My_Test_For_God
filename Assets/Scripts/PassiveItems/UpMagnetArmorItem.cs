public class UpMagnetArmorItem : ArmorItem
{
	protected override void ApplyModifier()
	{
		PlayerStats.CurrentMagnet *= 1 + ArmorItemData.Multiplier / 100f;
	}
}