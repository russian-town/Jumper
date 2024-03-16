using Sourse.Game;
using Sourse.UI.Shop.Scripts.Buttons;

namespace Sourse.UI.LevelCompletePanel
{
    public class NextLevelButton : UIButton, IPauseHandler
    {
        public void SetPause(bool isPause)
        {
            SwitchEnableState(!isPause);
        }
    }
}
