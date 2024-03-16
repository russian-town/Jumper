public class RewardedButton : UIButton, IPauseHandler
{
    public void SetPause(bool isPause)
    {
        SwitchEnableState(!isPause);
    }
}
