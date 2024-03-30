using Sourse.Balance;
using Sourse.Finish;
using Sourse.Level;
using Sourse.Player.Common.Scripts;
using Sourse.UI.LevelCompletePanel;
using UnityEngine;

namespace Sourse.Game
{
    public class LevelFinisher
    {
        private bool _isLevelCompleted = false;

        private readonly LevelCompletePanel _levelCompletePanel;
        private readonly Wallet _wallet;
        private readonly float _percentOpeningSkin;
        private readonly int _moneyOfLevel;
        private readonly Level.Level _level;
        private readonly LevelProgressView _levelProgressView;
        private readonly GroundDetector _groundDetector;

        public LevelFinisher(LevelCompletePanel levelCompletePanel, 
            Wallet wallet,
            float percentOpeningSkin,
            int moneyOfLevel,
            Level.Level level,
            LevelProgressView levelProgressView,
            GroundDetector groundDetector)
        {
            _levelCompletePanel = levelCompletePanel;
            _wallet = wallet;
            _percentOpeningSkin = percentOpeningSkin;
            _moneyOfLevel = moneyOfLevel;
            _level = level;
            _levelProgressView = levelProgressView;
            _groundDetector = groundDetector;
        }

        public void Subscribe()
            => _groundDetector.Fell += OnFell;

        public void Unsubscribe()
            => _groundDetector.Fell -= OnFell;

        private void CompleteLevel()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        _yandexAds.ShowInterstitial();
#endif

            if (_isLevelCompleted == true)
                return;

            _isLevelCompleted = true;
            _levelProgressView.Hide();
            _levelCompletePanel.Show();
            _wallet.AddMoney(_moneyOfLevel);
            _levelCompletePanel.HideOpeningSkinBar();
            _levelCompletePanel.SetText(_level.CurrentLevelNumber);
        }

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out LevelCompleteSoundPlayer _))
                CompleteLevel();
        }

    }
}
