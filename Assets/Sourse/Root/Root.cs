using System.Collections.Generic;
using Sourse.Balance;
using Sourse.Camera;
using Sourse.Constants;
using Sourse.Enviroment.Common;
using Sourse.Game;
using Sourse.Game.Finish;
using Sourse.Game.Lose;
using Sourse.Level;
using Sourse.Pause;
using Sourse.Player.Common;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using Sourse.UI;
using Sourse.UI.LevelCompletePanel;
using Sourse.UI.Shop.Scripts.Buttons;
using Sourse.UI.Shop.SkinConfiguration;
using Sourse.Yandex;
using UnityEngine;

namespace Sourse.Root
{
    public class Root : MonoBehaviour, IDataReader
    {
        [SerializeField] private FollowCamera _followCamera;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private ApplicationStatusChecker.ApplicationStatusChecker _applicationStatusChecker;
        [SerializeField] private PlayerPosition _startPoint;
        [SerializeField] private GameLossView _gameLossView;
        [SerializeField] private LevelProgressView _levelProgressView;
        [SerializeField] private LevelFinishView _levelCompletePanel;
        [SerializeField] private int _moneyOfLevel;
        [SerializeField] private PausePanel _pausePanel;
        [SerializeField] private RetryButton _retryButton;
        [SerializeField] private RewardedPanel _rewardedPanel;
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private NextLevelButton _nextLevelButton;
        [SerializeField] private List<Props> _props = new();
        [SerializeField] private FinishPosition _finishPosition;
        [SerializeField] private List<SkinConfig> _skinConfigs = new();
        [SerializeField] private OpenableSkinBar _openableSkinBar;

        private readonly Wallet _wallet = new();
        private readonly PlayerSpawner _playerSpawner = new();

        private PlayerInitializer _startPlayer;
        private LocalSave _localSave;
        private Vector3 _targetRotation = new(0f, 90f, 0f);
        private YandexAds _yandexAds;
        private LevelFinisher _levelFinisher;
        private GameLoss _gameLoss;
        private int _id;
        private LastPropsSaver _lastPropsSaver;
        private LevelProgress _levelProgress;
        private OpenableSkinViewFiller _openableSkinViewFiller;
        private List<IDataReader> _dataReaders = new();
        private List<IDataWriter> _dataWriters = new();
        private PlayerInitializer _playerTemplate;
        private Pause.Pause _pause;
        private Level.Level _level = new Level.Level();

        private void OnDestroy()
            => Unsubscribe();

        private void Start()
            => Initialize();

        private void Update()
            => _levelProgressView.UpdateProgressBar();

        private void Initialize()
        {
            GetPlayerTemplate();
            _yandexAds = new YandexAds();
            _applicationStatusChecker.Initialize(_yandexAds);
            _nextLevelButton.Initialize();
            _retryButton.Initialize();
            _startPlayer = _playerSpawner.GetPlayer(_playerTemplate, _startPoint, _targetRotation);
            _startPlayer.Initialize(_playerInput);
            _levelProgress = new LevelProgress(_startPlayer, _finishPosition, _startPoint);
            _gameLoss = new GameLoss(_startPoint,
                _levelProgress,
                _startPlayer.Death);
            _gameLoss.Subscribe();
            _openableSkinViewFiller = new(_levelCompletePanel,
                _skinConfigs,
                _openableSkinBar);
            _dataReaders = new List<IDataReader>()
            {
                _wallet,
                _openableSkinViewFiller
            };
            _dataWriters = new List<IDataWriter>()
            {
                _wallet,
                _openableSkinViewFiller
            };
            _localSave = new(_dataReaders, _dataWriters);
            _localSave.Load();
            _followCamera.SetTarget(_startPlayer);
            _levelProgressView.Initialize(_levelProgress);
            _levelProgressView.UpdateProgressBar();
            List<IPauseHandler> pauseHandlers = new()
            {
                _nextLevelButton,
                _retryButton,
                _playerInput,
                _gameLoss,
            };
            _pause = new Pause.Pause(_pauseButton, _pausePanel, pauseHandlers);
            _levelFinisher = new LevelFinisher(_startPlayer.GetComponent<GroundDetector>(),
                _level);
            _levelFinisher.Subscribe();
            _levelFinisher.LevelCompleted += OnLevelCompleted;
            _levelProgressView.Show();
            _gameLossView.Hide();
            _lastPropsSaver = new LastPropsSaver(_props, _startPlayer.GetComponent<GroundDetector>());
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

            foreach (var openableSkinSaveData in playerData.OpenableSkinSaveDatas)
            {
                if (openableSkinSaveData.IsSelect == true)
                {
                    _id = openableSkinSaveData.ID;
                    return;
                }
            }

            _id = PlayerParameter.DefaultPlayerID;
        }

        private void OnLevelCompleted(int levelNumber)
            => _localSave.Save();

        private void GetPlayerTemplate()
        {
            _dataReaders.Add(this);
            _localSave = new LocalSave(_dataReaders, _dataWriters);
            _localSave.Load();
            string path = $"{PlayerParameter.PlayerPrefabsPath}{_id}";
            _playerTemplate = Resources.Load<PlayerInitializer>(path);
        }

        private void Unsubscribe()
        {
            _startPlayer.Unsubscribe();
            //_nextLevelButton.Unsubscribe();
            _levelFinisher.Unsubscribe();
            _gameLoss.Unsubscribe();
            _lastPropsSaver.Unsubscribe();
        }
    }
}
