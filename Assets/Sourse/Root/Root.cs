using Sourse.Camera;
using Sourse.Game;
using Sourse.Level;
using Sourse.Pause;
using Sourse.Player.Common.Scripts;
using Sourse.UI;
using Sourse.UI.LevelCompletePanel;
using Sourse.UI.Shop.Scripts.Buttons;
using Sourse.UI.Shop.SkinView.OpenableSkinView;
using UnityEngine;

namespace Sourse.Root
{
    [RequireComponent(typeof(PlayerSpawner), typeof(Saver.Saver), typeof(OpenableSkinHandler))]
    public class Root : MonoBehaviour, IPauseHandler
    {
        private const string FillAmountKey = "FillAmount";

        [SerializeField] private FollowCamera _followCamera;
        [SerializeField] private LevelProgress _levelProgress;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Level.Level _level;
        [SerializeField] private ApplicationStatusChecker.ApplicationStatusChecker _applicationStatusChecker;
        [SerializeField] private PlayerPositionHandler _playerPositionHandler;
        [SerializeField] private PlayerPosition _startPlayerPosition;
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private LevelProgressView _levelProgressView;
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

        private Vector3 _targetRotation = new Vector3(0f, 90f, 0f);
        private PlayerSpawner _playerSpawner;
        private Saver.Saver _saver;
        private Player.Common.Scripts.Player _startPlayer;
        private OpenableSkinHandler _openableSkinHandler;
        private bool _isPause;
        private PlayerPosition _playerLastPosition;
        private PlayerPosition _playerStartPosition;
        private YandexAds.YandexAds _yandexAds;
        private bool _isStart;
        private bool _isRewarded = false;
        private float _percentRatio = 100f;

        private void Awake()
        {
            _playerSpawner = GetComponent<PlayerSpawner>();
            _saver = GetComponent<Saver.Saver>();
        }

        private void OnDestroy()
        {
            _startPlayer.Died -= OnPlayerDied;
            _startPlayer.LevelCompleted -= OnLevelCompleted;
            _startPlayer.Unsubscribe();
            _nextLevelButton.Unsubscribe();
            _rewardedButton.Unsubscribe();
            _noThanksButton.Unsubscribe();
            _retryButton.Unsubscribe();
            _level.Unsubscribe();
            _openableSkinHandler.Unsubscribe();
        }

        private void Start()
        {
            _level.Initialize(_levelProgress, _playerPositionHandler);
            _playerPositionHandler.Initialize();
            PlayerPosition startPositon = _playerPositionHandler.GetLastPosition();

            if (startPositon == null)
                startPositon = _startPlayerPosition;

            _playerPositionHandler.RemoveCurrentPropsID();
            Initialize(_saver.GetSelectedID(), startPositon);
        }

        private void Initialize(int id, PlayerPosition playerPosition)
        {
            _startPlayer = _playerSpawner.GetPlayer(id, playerPosition);
            _startPlayer.transform.localRotation = Quaternion.Euler(_targetRotation);
            _startPlayer.Initialize(_playerPositionHandler, _playerInput);
            _startPlayer.Died += OnPlayerDied;
            _startPlayer.LevelCompleted += OnLevelCompleted;
            _levelProgress.Initialize(_startPlayer);
            _followCamera.SetTarget(_startPlayer);
            _openableSkinHandler = GetComponent<OpenableSkinHandler>();
            _yandexAds = new YandexAds.YandexAds();
            _applicationStatusChecker.Initialize(_yandexAds);
            _openableSkinHandler.Initialize(_levelCompletePanel);
            _nextLevelButton.Initialize();
            _rewardedButton.Initialize();
            _noThanksButton.Initialize();
            _retryButton.Initialize();
            IPauseHandler[] pauseHandlers = new IPauseHandler[]
            {
                this,
                _nextLevelButton,
                _retryButton,
                _noThanksButton,
                _rewardedButton,
                _playerInput
            };
            _pause.Initialize(pauseHandlers);
            _levelProgressView.Show();
            _gameOverView.Hide();
        }

        public void SetPause(bool isPause)
        {
            _isPause = isPause;

            if (_isPause == true)
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
