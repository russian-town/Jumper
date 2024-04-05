using Sourse.PauseContent;
using Sourse.Settings.Audio;

namespace Sourse.Root
{
    public class ApplicationFocus
    {
        private readonly Audio _audio;
        private readonly Pause _pause;

        private bool _isEnable;

        public ApplicationFocus(Audio audio, Pause pause)
        {
            _audio = audio;
            _pause = pause;
            _isEnable = true;
        }

        public void SetFocus(bool focus)
        {
            if (_isEnable == false)
                return;

            if (focus)
            {
                _audio.Unmute();
            }
            else
            {
                _audio.Mute();
                _pause.Enable();
            }
        }

        public void Enable()
            => _isEnable = true;

        public void Disable()
            => _isEnable = false;
    }
}
