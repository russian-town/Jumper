using System.Collections.Generic;
using Sourse.Game;
using UnityEngine;

namespace Sourse.Pause
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private PausePanel _pausePanel;

        private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();
        private Game.Game _game;

        public void Initialize(IPauseHandler[] pauseHandlers, Game.Game game)
        {
            _pauseHandlers.AddRange(pauseHandlers);
            _game = game;
        }

        public void Enable()
        {
            if (_game.IsLevelComplete == true)
                _pausePanel.DisableButtons();

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
