using Sourse.Game;

namespace Sourse.UI.Shop.Scripts.Buttons
{
    public class RewardedButton : UIButton, IPauseHandler
    {
        public void SetPause(bool isPause)
        {
            SwitchEnableState(!isPause);
        }
    }
}
