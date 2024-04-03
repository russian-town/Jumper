using System.Collections.Generic;
using Sourse.UI.Shop.Scripts.Buttons;
using UnityEngine;

namespace Sourse.Pause
{
    public class Pause
    {
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private PausePanel _pausePanel;

        private readonly List<IPauseHandler> _pauseHandlers;

        public Pause(PauseButton pauseButton,
            PausePanel pausePanel,
            List<IPauseHandler> pauseHandlers)
        {
            _pauseButton = pauseButton;
            _pausePanel = pausePanel;
            _pauseHandlers = pauseHandlers;
        }

        public void Enable()
        {
            _pauseButton.Hide();
            _pausePanel.Show();

            foreach (var pauseHandler in _pauseHandlers)
            {
                pauseHandler.SetPause(true);
            }
        }

        public void Disable()
        {
            _pauseButton.Show();
            _pausePanel.Hide();

            foreach (var pauseHandler in _pauseHandlers)
            {
                pauseHandler.SetPause(false);
            }
        }
    }
}
