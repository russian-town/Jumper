public class RetryButton : UIButton, IPauseHandler
{
    public void SetPause(bool isPause)
    {
        SwitchEnableState(!isPause);
    }
}
