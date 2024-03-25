using Sourse.Balance;
using Sourse.Camera;
using Sourse.Constants;
using Sourse.Enviroment.Common;
using Sourse.Game;
using Sourse.Level;
using Sourse.Pause;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using Sourse.UI;
using Sourse.UI.LevelCompletePanel;
using Sourse.UI.Shop.Scripts.Buttons;
using Sourse.YandexAds;
using System.Collections.Generic;
using UnityEngine;

namespace Sourse.Root
{
    public class Root : MonoBehaviour, IDataReader
    {
        [SerializeField] private FollowCamera _followCamera;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Level.Level _level;
        [SerializeField] private ApplicationStatusChecker.ApplicationStatusChecker _applicationStatusChecker;
        [SerializeField] private PlayerPosition _startPoint;
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
        [SerializeField] private RewardedVideo _rewardedVideo;
        [SerializeField] private List<Props> _props = new List<Props>();

        private PlayerInitializer _startPlayer;
        private Wallet _wallet = new Wallet();
        private PlayerSpawner _playerSpawner = new PlayerSpawner();
        private LocalSave _localSave;
        private Vector3 _targetRotation = new Vector3(0f, 90f, 0f);
        private YandexAds.YandexAds _yandexAds;
        private FinishLevelHandler _finishLevelHandler;
        private LoseLevelHandler _loseLevelHandler;
        private PlayerUI _playerUI;
        private int _id;
        private LastPropsSaver _lastPropsSaver;

        private void OnDestroy()
            => Unsubscribe();

        private void Start()
            => Initialize();

        private void Update()
        {
            _levelProgressView.UpdateProgressBar();
        }

        private void Initialize()
        {
            //_level.Initialize(_levelProgress);
            List<IDataReader> dataReaders = new List<IDataReader>()
            {
                this,
                _wallet
            };
            List<IDataWriter> dataWriters = new List<IDataWriter>()
            {
                _wallet
            };
            _localSave = new LocalSave(dataReaders, dataWriters);
            _localSave.Load();
            string path = $"{PlayerParameter.PlayerPrefabsPath}{_id}";
            var playerTemplate = Resources.Load<PlayerInitializer>(path);
            _startPlayer = _playerSpawner.GetPlayer(playerTemplate, _startPoint, _targetRotation);
            _startPlayer.Initialize(_playerInput);
            _followCamera.SetTarget(_startPlayer);
            _yandexAds = new YandexAds.YandexAds();
            _applicationStatusChecker.Initialize(_yandexAds);
            _nextLevelButton.Initialize();
            _rewardedButton.Initialize();
            _noThanksButton.Initialize();
            _retryButton.Initialize();
            _finishLevelHandler = new FinishLevelHandler(_levelCompletePanel,
                _wallet,
                _percentOpeningSkin,
                _moneyOfLevel,
                _level,
                _levelProgressView,
                _startPlayer);
            _finishLevelHandler.Subscribe();
            _loseLevelHandler = new LoseLevelHandler(
                _startPoint,
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
            //_lastPropsSaver = new LastPropsSaver(_props);
            _lastPropsSaver.Subscribe();
        }

        public void Read(PlayerData playerData)
        {
            foreach (var skinSaveData in playerData.SkinSaveDatas)
            {
                if (skinSaveData.IsSelect == true)
                {
                    _id = skinSaveData.ID;
                    return;
                }
            }

            _id = PlayerParameter.DefaultPlayerID;
        }

        private void Unsubscribe()
        {
            _startPlayer.Unsubscribe();
            _nextLevelButton.Unsubscribe();
            _rewardedButton.Unsubscribe();
            _noThanksButton.Unsubscribe();
            _retryButton.Unsubscribe();
            _level.Unsubscribe();
            _finishLevelHandler.Unsubscribe();
            _loseLevelHandler.Unsubscribe();
            _playerUI.Unsibscribe();
            _lastPropsSaver.Unsubscribe();
        }
    }
}
