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
        private readonly Player.Common.Scripts.Player _player;
        private readonly float _percentRatio = 100f;

        private bool _isPause;

        public LoseLevelHandler(LevelProgress levelProgress,
            PlayerPosition playerStartPosition,
            GameOverView gameOverView,
            RewardedPanel rewardedPanel,
            RetryButton retryButton,
            LevelProgressView levelProgressView,
            Player.Common.Scripts.Player player)
        {
            _levelProgress = levelProgress;
            _playerStartPosition = playerStartPosition;
            _gameOverView = gameOverView;
            _rewardedPanel = rewardedPanel;
            _retryButton = retryButton;
            _levelProgressView = levelProgressView;
            _player = player;
        }

        public void Subscribe()
        {
            _player.Died += OnPlayerDied;
        }

        public void Unsubscribe()
        {
            _player.Died -= OnPlayerDied;
        }

        public void SetPause(bool isPause)
            => _isPause = isPause;

        private void OnPlayerDied()
        {
            _levelProgress.DeleteSavedDistance();
            float percent = Mathf.Ceil(_levelProgress.CurrentDistance * _percentRatio);
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
