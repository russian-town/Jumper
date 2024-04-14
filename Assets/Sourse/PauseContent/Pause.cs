using System;
using System.Collections.Generic;

namespace Sourse.PauseContent
{
    public class Pause
    {
        private readonly List<IPauseHandler> _pauseHandlers;

        public Pause(List<IPauseHandler> pauseHandlers)
        {
            _pauseHandlers = pauseHandlers;
        }

        public event Action Enabled;

        public event Action Disabled;

        public void Enable()
        {
            foreach (var pauseHandler in _pauseHandlers)
            {
                pauseHandler.SetPause(true);
            }

            Enabled?.Invoke();
        }

        public void Disable()
        {
            foreach (var pauseHandler in _pauseHandlers)
            {
                pauseHandler.SetPause(false);
            }

            Disabled?.Invoke();
        }
    }
}
