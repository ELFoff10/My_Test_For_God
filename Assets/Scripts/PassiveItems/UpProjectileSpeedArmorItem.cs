public class UpProjectileSpeedArmorItem : ArmorItem
{
	protected override void ApplyModifier()
	{
		PlayerStats.CurrentProjectileSpeed *= 1 + ArmorItemData.Multiplier / 100f;
	}
}