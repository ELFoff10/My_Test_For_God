public class PickupArmor : Pickup
{
	public override void Collect()
	{
		if (HasBeenCollected)
		{
			return;
		}
		
		// Событие что подняли определенную броню?
		// var player = FindObjectOfType<PlayerInventory>(); 
		// player.Add определенную броню

		AudioManager.Instance.EventInstances[(int)AudioNameEnum.PickUpArmor].start();
	}
}