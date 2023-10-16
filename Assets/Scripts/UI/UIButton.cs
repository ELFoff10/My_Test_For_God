using UnityEngine;

public class UIButton : MonoBehaviour
{
	public void Click()
	{
		AudioManager.Instance.PlayButtonClick();
	}
	
	public void MenuBackgroundMusicStop()
	{
		AudioManager.Instance.EventInstances[(int)AudioNameEnum.MenuBackgroundMusic].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
	
	public void GameBackgroundMusicStart()
	{
		AudioManager.Instance.EventInstances[(int)AudioNameEnum.GameBackgroundMusic].start();
	}	
}