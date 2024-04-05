using System;
using Lean.Localization;
using Sourse.Constants;
using Sourse.UI;
using Sourse.UI.Shop.Scripts.Buttons;
using TMPro;
using UnityEngine;

namespace Sourse.Game.Lose
{
    public class GameLossView : UIElement
    {
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private RewardedPanel _rewardedPanel;
        [SerializeField] private RetryButton _retryButton;
        [SerializeField] private RewardedButton _rewardedButton;
        [SerializeField] private CloseAdOfferScreenButton _closeAdOfferScreenButton;

        private GameLoss _gameLoss;
        private LastPropsSaver _lastPropsSaver;

        public event Action RewardedButtonClicked;
        public event Action CloseAdOfferScreenButtonClicked;
        public event Action RetryButtonClicked;

        public void Initialize(GameLoss gameLoss,
            LastPropsSaver lastPropsSaver)
        {
            _gameLoss = gameLoss;
            _lastPropsSaver = lastPropsSaver;
            _retryButton.Initialize();
            _rewardedButton.Initialize();
            _closeAdOfferScreenButton.Initialize();
            _rewardedPanel.Hide();
            Hide();
        }

        public void Subscribe()
        {
            _gameLoss.GameOver += OnGameOver;
            _rewardedButton.AddListener(()
                => RewardedButtonClicked?.Invoke());
            _closeAdOfferScreenButton.AddListener(()
                => CloseAdOfferScreenButtonClicked?.Invoke());
            _retryButton.AddListener(()
                => RetryButtonClicked?.Invoke());
        }

        public void Unsubscribe()
        {
            _gameLoss.GameOver -= OnGameOver;
            _rewardedButton.RemoveListener(()
                => RewardedButtonClicked?.Invoke());
            _closeAdOfferScreenButton.RemoveListener(()
                => CloseAdOfferScreenButtonClicked?.Invoke());
            _retryButton.RemoveListener(()
                => RetryButtonClicked?.Invoke());
        }

        public void ShowProgress(float progress)
        {
            Show();
            string translatedCompleted =
                LeanLocalization.GetTranslationText(TranslationText.Completed);
            _progressText.text = $"{progress} % {translatedCompleted}";
        }

        private void OnGameOver(float progress)
        {
            ShowProgress(progress);

            if (_lastPropsSaver.LastPropsIndex <= 0)
                return;

            _rewardedPanel.Show();
            _retryButton.Hide();
        }
    }
}
