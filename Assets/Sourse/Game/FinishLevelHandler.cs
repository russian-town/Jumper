using Sourse.Balance;
using Sourse.Level;
using Sourse.Player.Common.Scripts;
using Sourse.UI.LevelCompletePanel;

namespace Sourse.Game
{
    public class FinishLevelHandler
    {
        private const string FillAmountKey = "FillAmount";

        private bool _isLevelCompleted = false;

        private readonly LevelCompletePanel _levelCompletePanel;
        private readonly Wallet _wallet;
        private readonly float _percentOpeningSkin;
        private readonly int _moneyOfLevel;
        private readonly Level.Level _level;
        private readonly LevelProgress _levelProgress;
        private readonly LevelProgressView _levelProgressView;
        private readonly PlayerInitializer _player;

        public FinishLevelHandler(LevelCompletePanel levelCompletePanel, 
            Wallet wallet,
            float percentOpeningSkin,
            int moneyOfLevel,
            Level.Level level,
            LevelProgress levelProgress,
            LevelProgressView levelProgressView,
            PlayerInitializer player)
        {
            _levelCompletePanel = levelCompletePanel;
            _wallet = wallet;
            _percentOpeningSkin = percentOpeningSkin;
            _moneyOfLevel = moneyOfLevel;
            _level = level;
            _levelProgress = levelProgress;
            _levelProgressView = levelProgressView;
            _player = player;
        }

        public void Subscribe()
        { }
            /*=> _player.LevelCompleted += OnLevelCompleted;*/

        public void Unsubscribe()
        { }
           /* => _player.LevelCompleted -= OnLevelCompleted;*/

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
/*
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
            }*/

            _levelCompletePanel.HideOpeningSkinBar();
            _levelCompletePanel.SetText(_level.CurrentLevelNumber);
        }
    }
}
