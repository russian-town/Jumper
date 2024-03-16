using Sourse.UI.Shop.Scripts.Buttons;

namespace Sourse.Game
{
    public class RetryButton : UIButton, IPauseHandler
    {
        public void SetPause(bool isPause)
        {
            SwitchEnableState(!isPause);
        }
    }
}
