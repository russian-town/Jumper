using System;
using Sourse.UI;
using Sourse.UI.Shop.Scripts.Buttons;
using UnityEngine;

namespace Sourse.Pause
{
    public class PauseView : UIElement
    {
        [SerializeField] private ContinueButton _continueButton;
        [SerializeField] private RetryButton _restartButton;
        [SerializeField] private ExitButton _exitButton;

        private Pause _pause;

        public event Action ContinueButtonClicked;
        public event Action RestatrButtonClicked;
        public event Action ExitButtonClicked;

        public void Subscribe()
        {
            _pause.Enabled += OnPauseEnabled;
            _pause.Disabled += OnPauseDisabled;
            _continueButton.AddListener(()
                => ContinueButtonClicked?.Invoke());
            _restartButton.AddListener(()
                => RestatrButtonClicked?.Invoke());
            _exitButton.AddListener(()
                => ExitButtonClicked?.Invoke());
        }

        public void Unsubscribe()
        {
            _pause.Enabled -= OnPauseEnabled;
            _pause.Disabled -= OnPauseDisabled;
            _continueButton.RemoveListener(()
               => ContinueButtonClicked?.Invoke());
            _restartButton.RemoveListener(()
                => RestatrButtonClicked?.Invoke());
            _exitButton.RemoveListener(()
                => ExitButtonClicked?.Invoke());
        }

        public void Initialize(Pause pause)
        {
            _pause = pause;
            _continueButton.Initialize();
            _restartButton.Initialize();
            _exitButton.Initialize();
        }

        private void OnPauseDisabled()
            => Hide();

        private void OnPauseEnabled()
            => Show();
    }
}
