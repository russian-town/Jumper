using UnityEngine;

public class NoThanksButton : UIButton, IPauseHandler
{
    public void SetPause(bool isPause)
    {
        SwitchEnableState(!isPause);
    }
}
