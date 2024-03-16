using Sourse.Game;
using Sourse.UI.Shop.Scripts.Buttons;

namespace Sourse.UI
{
    public class CloseAdOfferScreenButton : UIButton, IPauseHandler
    {
        public void SetPause(bool isPause)
            => SwitchEnableState(!isPause);
    }
}
