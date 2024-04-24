using Sourse.PauseContent;
using System;

namespace Sourse.UI.Shop.Scripts
{
    public class StartPanel : UIElement
    {
        private Pause _pause;

        public void Initialize(Pause pause)
        {
            _pause = pause;
            _pause.Enabled += OnPauseEnabled;
            _pause.Disabled += OnPauseDisable;
        }

        public void Unsubscribe()
        {
            _pause.Enabled -= OnPauseEnabled;
            _pause.Disabled -= OnPauseDisable;
        }

        private void OnPauseEnabled()
            => Hide();

        private void OnPauseDisable()
            => Show();
    }
}
