using Sourse.Level;
using Sourse.UI.LevelCompletePanel;
using Sourse.UI.Shop.SkinView.OpenableSkinView;
using UnityEngine;

namespace Sourse.Game
{
    public class FinishLevelHandler
    {
        private const string FillAmountKey = "FillAmount";

        private bool _isLevelCompleted = false;

        private readonly LevelCompletePanel _levelCompletePanel;
        private readonly Wallet.Wallet _wallet;
        private readonly OpenableSkinHandler _openableSkinHandler;
        private readonly float _percentOpeningSkin;
        private readonly int _moneyOfLevel;
        private readonly Level.Level _level;
        private readonly LevelProgress _levelProgress;
        private readonly LevelProgressView _levelProgressView;
        private readonly Player.Common.Scripts.Player _player;

        public FinishLevelHandler(LevelCompletePanel levelCompletePanel, 
            Wallet.Wallet wallet,
            OpenableSkinHandler openableSkinHandler,
            float percentOpeningSkin,
            int moneyOfLevel,
            Level.Level level,
            LevelProgress levelProgress,
            LevelProgressView levelProgressView,
            Player.Common.Scripts.Player player)
        {
            _levelCompletePanel = levelCompletePanel;
            _wallet = wallet;
            _openableSkinHandler = openableSkinHandler;
            _percentOpeningSkin = percentOpeningSkin;
            _moneyOfLevel = moneyOfLevel;
            _level = level;
            _levelProgress = levelProgress;
            _levelProgressView = levelProgressView;
            _player = player;
        }

        public void Subscribe()
            => _player.LevelCompleted += OnLevelCompleted;

        public void Unsubscribe()
            => _player.LevelCompleted -= OnLevelCompleted;

        private void OnLevelCompleted()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        _yandexAds.ShowInterstitial();
#endif

            if (_isLevelCompleted == true)
                return;

            _isLevelCompleted = true;
            _levelProgress.DeleteSavedDistance();
            _levelProgressView.Hide();
            _levelCompletePanel.Show();
            _wallet.AddMoney(_moneyOfLevel);

            if (_openableSkinHandler.GetOpenableSkin() != null)
            {
                float targetFillAmount;

                if (PlayerPrefs.HasKey(FillAmountKey) == true)
                    targetFillAmount = PlayerPrefs.GetFloat(FillAmountKey) + _percentOpeningSkin;
                else
                    targetFillAmount = _levelCompletePanel.CurrentFillAmount + _percentOpeningSkin;

                _levelCompletePanel.Initialize(_openableSkinHandler.GetOpenableSkin());
                _levelCompletePanel.StartFillSkinBarCoroutine(targetFillAmount);
            }
            else
            {
                _levelCompletePanel.HideOpeningSkinBar();
            }

            _levelCompletePanel.SetText(_level.CurrentLevelNumber);
        }
    }
}
