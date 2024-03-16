using Sourse.Level;
using Sourse.Pause;
using Sourse.Player.Common.Scripts;
using Sourse.UI;
using Sourse.UI.LevelCompletePanel;
using Sourse.UI.Shop.Scripts.Buttons;
using Sourse.UI.Shop.SkinView.OpenableSkinView;
using UnityEngine;

namespace Sourse.Game
{
    [RequireComponent(typeof(OpenableSkinHandler))]
    public class Game : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private LevelProgressView _levelProgressView;
        [SerializeField] private Level.Level _level;
        [SerializeField] private LevelCompletePanel _levelCompletePanel;
        [SerializeField] private float _percentOpeningSkin;
        [SerializeField] private int _moneyOfLevel;
        [SerializeField] private Pause.Pause _pause;
        [SerializeField] private RetryButton _retryButton;
        [SerializeField] private RewardedPanel _rewardedPanel;
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private CloseAdOfferScreenButton _noThanksButton;
        [SerializeField] private RewardedButton _rewardedButton;
        [SerializeField] private NextLevelButton _nextLevelButton;
        [SerializeField] private Wallet.Wallet _wallet;
        [SerializeField] private bool _hideHUD;

        private OpenableSkinHandler _openableSkinHandler;
        private Player.Common.Scripts.Player _player;
        private LevelProgress _levelProgress;
        private PlayerInput _playerInput;
        private bool _isPause;
        private PlayerPosition _playerLastPosition;
        private PlayerPosition _playerStartPosition;
        private YandexAds.YandexAds _yandexAds;
        private ApplicationStatusChecker.ApplicationStatusChecker _applicationStatusChecker;
        private bool _isStart;
        private bool _isRewarded = false;
        private float _percentRatio = 100f;

        public bool IsLevelComplete { get; private set; }

        public void Enable()
        {
            _yandexAds.OpenInterstitialCallback += OnOpenInterstitialCallback;
            _yandexAds.CloseInterstitialCallback += OnCloseInterstitialCallback;
            _player.Died += OnPlayerDied;
            _player.LevelCompleted += OnLevelCompleted;
            _playerInput.Pressed += StartGame;
        }

        public void Disable()
        {
            if (_player == null)
                return;

            _isStart = false;
            _player.Died -= OnPlayerDied;
            _player.LevelCompleted -= OnLevelCompleted;
            _playerInput.Pressed -= StartGame;
            _yandexAds.OpenInterstitialCallback -= OnOpenInterstitialCallback;
            _yandexAds.CloseInterstitialCallback -= OnCloseInterstitialCallback;
            _gameOverView.Hide();
        }

        public void Initialaize(Player.Common.Scripts.Player player, LevelProgress levelProgress, PlayerInput playerInput, PlayerPosition playerStartPosition, ApplicationStatusChecker.ApplicationStatusChecker applicationStatusChecker)
        {
            _openableSkinHandler = GetComponent<OpenableSkinHandler>();
            _yandexAds = new YandexAds.YandexAds();
            _applicationStatusChecker = applicationStatusChecker;
            _applicationStatusChecker.Initialize(_yandexAds);
            _openableSkinHandler.Initialize(_levelCompletePanel);
            _player = player;
            _playerInput = playerInput;
            _playerStartPosition = playerStartPosition;
            _levelProgress = levelProgress;
            _nextLevelButton.Initialize();
            _rewardedButton.Initialize();
            _noThanksButton.Initialize();
            _retryButton.Initialize();
            IPauseHandler[] pauseHandlers = new IPauseHandler[]
            {
                player, this, _nextLevelButton, _retryButton, _noThanksButton, _rewardedButton 
            };
            _pause.Initialize(pauseHandlers, this);
        }

        public void StartGame()
        {
            if (_isStart == true || _isPause == true)
                return;

            _isStart = true;
            _player.SetStart(_isStart);

            if (_hideHUD)
            {
                _gameOverView.Hide();
                _pauseButton.Hide();
                return;
            }

            _levelProgressView.Show();
            _gameOverView.Hide();
        }

        public void SetPause(bool isPause)
        {
            _isPause = isPause;

            if (_isPause == true && IsLevelComplete == true)
                _nextLevelButton.Hide();
            else
                _nextLevelButton.Show();

            if (_isStart == false && _isPause == true)
                _retryButton.Hide();
            else if (_isStart == false && _isPause == true && _isRewarded == true)
                _retryButton.Hide();
            else if (_isPause == false && _isRewarded == false)
                _retryButton.Show();
        }

        public void SetLastPosition(PlayerPosition playerLastPosition)
        {
            _playerLastPosition = playerLastPosition;
        }

        private void OnOpenInterstitialCallback()
        {
            _applicationStatusChecker.ChangeSoundStatus(true);
        }

        private void OnCloseInterstitialCallback(bool isClose)
        {
            _applicationStatusChecker.ChangeSoundStatus(false);
        }

        private void OnPlayerDied()
        {
            _isStart = false;
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
                _isRewarded = true;
                _rewardedPanel.Show();
                _retryButton.Hide();
            }

            _gameOverView.ShowProgress(percent);
            _levelProgressView.Hide();
        }

        private void OnLevelCompleted()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        _yandexAds.ShowInterstitial();
#endif

            if (_hideHUD)
                return;

            IsLevelComplete = true;
            _levelProgress.DeleteSavedDistance();
            _levelProgressView.Hide();
            _levelCompletePanel.Show();

            _wallet.AddMoney(_moneyOfLevel);

            if (_openableSkinHandler.GetOpenableSkin() != null)
            {
                _levelCompletePanel.Initialize(_openableSkinHandler.GetOpenableSkin());
                _levelCompletePanel.StartFillSkinBarCoroutine(_percentOpeningSkin);
            }
            else
            {
                _levelCompletePanel.HideOpeningSkinBar();
            }

            _levelCompletePanel.SetText(_level.CurrentLevelNumber);
        }
    }
}
