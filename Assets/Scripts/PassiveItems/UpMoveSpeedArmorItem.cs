public class UpMoveSpeedArmorItem : ArmorItem
{
	protected override void ApplyModifier()
	{
		PlayerStats.CurrentMoveSpeed *= 1 + ArmorItemData.Multiplier / 100f;
	}
}