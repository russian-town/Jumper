using Sourse.Camera;
using Sourse.Game;
using Sourse.Level;
using Sourse.Pause;
using Sourse.Player.Common.Scripts;
using Sourse.UI;
using Sourse.UI.LevelCompletePanel;
using Sourse.UI.Shop.Scripts.Buttons;
using Sourse.UI.Shop.SkinView.OpenableSkinView;
using Sourse.YandexAds;
using UnityEngine;

namespace Sourse.Root
{
    [RequireComponent(typeof(PlayerSpawner), typeof(Saver.Saver), typeof(OpenableSkinHandler))]
    public class Root : MonoBehaviour
    {
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
        [SerializeField] private RewardedVideo _rewardedVideo;

        private Vector3 _targetRotation = new Vector3(0f, 90f, 0f);
        private PlayerSpawner _playerSpawner;
        private Saver.Saver _saver;
        private Player.Common.Scripts.Player _startPlayer;
        private OpenableSkinHandler _openableSkinHandler;
        private YandexAds.YandexAds _yandexAds;
        private FinishLevelHandler _finishLevelHandler;
        private LoseLevelHandler _loseLevelHandler;
        private PlayerUI _playerUI;

        private void Awake()
        {
            _playerSpawner = GetComponent<PlayerSpawner>();
            _saver = GetComponent<Saver.Saver>();
        }

        private void OnDestroy()
        {
            _startPlayer.Unsubscribe();
            _nextLevelButton.Unsubscribe();
            _rewardedButton.Unsubscribe();
            _noThanksButton.Unsubscribe();
            _retryButton.Unsubscribe();
            _level.Unsubscribe();
            _openableSkinHandler.Unsubscribe();
            _finishLevelHandler.Unsubscribe();
            _loseLevelHandler.Unsubscribe();
            _playerUI.Unsibscribe();
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
            _finishLevelHandler = new FinishLevelHandler(_levelCompletePanel,
                _wallet,
                _openableSkinHandler,
                _percentOpeningSkin,
                _moneyOfLevel,
                _level,
                _levelProgress,
                _levelProgressView,
                _startPlayer);
            _finishLevelHandler.Subscribe();
            _loseLevelHandler = new LoseLevelHandler(_levelProgress,
                _startPlayerPosition,
                _gameOverView,
                _rewardedPanel,
                _retryButton,
                _levelProgressView,
                _startPlayer);
            _loseLevelHandler.Subscribe();
            _playerUI = new PlayerUI(_nextLevelButton, _retryButton, _rewardedVideo);
            _playerUI.Subscribe();
            IPauseHandler[] pauseHandlers = new IPauseHandler[]
            {
                _nextLevelButton,
                _retryButton,
                _noThanksButton,
                _rewardedButton,
                _playerInput,
                _loseLevelHandler,
                _playerUI
            };
            _pause.Initialize(pauseHandlers);
            _levelProgressView.Show();
            _gameOverView.Hide();
        }
    }
}
