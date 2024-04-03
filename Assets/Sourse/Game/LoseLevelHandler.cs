using System;
using Sourse.Level;
using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Game
{
    public class LoseLevelHandler : IPauseHandler
    {
        private readonly LevelProgress _levelProgress;
        private readonly PlayerPosition _playerLastPosition;
        private readonly PlayerPosition _playerStartPosition;
        private readonly GameOverView _gameOverView;
        private readonly RewardedPanel _rewardedPanel;
        private readonly RetryButton _retryButton;
        private readonly LevelProgressView _levelProgressView;
        private readonly GroundDetector _groundDetector;
        private readonly float _percentRatio = 100f;

        private bool _isPause;

        public LoseLevelHandler(PlayerPosition playerStartPosition,
            GameOverView gameOverView,
            RewardedPanel rewardedPanel,
            RetryButton retryButton,
            LevelProgress levelProgress,
            LevelProgressView levelProgressView,
            GroundDetector groundDetector)
        {
            _playerStartPosition = playerStartPosition;
            _gameOverView = gameOverView;
            _rewardedPanel = rewardedPanel;
            _retryButton = retryButton;
            _levelProgress = levelProgress;
            _levelProgressView = levelProgressView;
            _groundDetector = groundDetector;
        }

        public event Action RetryButtonClicked;

        public void Subscribe()
        {
            _rewardedPanel.Subscribe();
            _retryButton.AddListener(() => RetryButtonClicked?.Invoke());
            _groundDetector.Fell += OnFell;
        }

        public void Unsubscribe()
        {
            _rewardedPanel.Unsubscribe();
            _retryButton.RemoveListener(() => RetryButtonClicked?.Invoke());
        }

        public void Initialize()
        {
            _rewardedPanel.Initialize();
            _retryButton.Initialize();
        }

        public void SetPause(bool isPause)
            => _isPause = isPause;

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Ground.Ground _))
                GameOver();
        }

        private void GameOver()
        {
            float percent = Mathf.Ceil(_levelProgress.GetCurrentDistance() * _percentRatio);
            _gameOverView.Show();

            if (_playerLastPosition == null || _playerLastPosition == _playerStartPosition)
            {
                _rewardedPanel.Hide();

                if (!_isPause)
                    _retryButton.Show();
            }
            else
            {
                _rewardedPanel.Show();
                _retryButton.Hide();
            }

            _gameOverView.ShowProgress(percent);
            _levelProgressView.Hide();
        }
    }
}
