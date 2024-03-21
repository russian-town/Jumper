using System.Collections.Generic;
using Sourse.Game;
using UnityEngine;

namespace Sourse.Pause
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private PausePanel _pausePanel;

        private readonly List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

        public void Initialize(IPauseHandler[] pauseHandlers)
          =>  _pauseHandlers.AddRange(pauseHandlers);

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
