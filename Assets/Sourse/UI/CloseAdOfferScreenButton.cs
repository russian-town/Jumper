public class CloseAdOfferScreenButton : UIButton, IPauseHandler
{
    public void SetPause(bool isPause)
         => SwitchEnableState(!isPause);
}
