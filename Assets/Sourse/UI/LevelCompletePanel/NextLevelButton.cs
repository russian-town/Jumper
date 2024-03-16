public class NextLevelButton : UIButton, IPauseHandler
{
    public void SetPause(bool isPause)
    {
        SwitchEnableState(!isPause);
    }
}
