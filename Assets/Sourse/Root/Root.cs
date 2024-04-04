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
        [SerializeField] private PlayerPosition _startPosition;
        [SerializeField] private GameLossView _gameLossView;
        [SerializeField] private LevelProgressView _levelProgressView;
        [SerializeField] private LevelFinishView _levelFinishView;
        [SerializeField] private int _moneyOfLevel;
        [SerializeField] private PauseView _pauseView;
        [SerializeField] private RetryButton _retryButton;
        [SerializeField] private RewardedPanel _rewardedPanel;
        [SerializeField] private NextLevelButton _nextLevelButton;
        [SerializeField] private List<Props> _props = new();
        [SerializeField] private FinishPosition _finishPosition;
        [SerializeField] private List<SkinConfig> _skinConfigs = new();
        [SerializeField] private OpenableSkinBar _openableSkinBar;
        [SerializeField] private HUD _hud;

        private readonly Wallet _wallet = new();
        private readonly PlayerSpawner _playerSpawner = new();
        private readonly LevelLoader _levelLoader = new();
        private readonly List<IDataReader> _dataReaders = new();
        private readonly List<IDataWriter> _dataWriters = new();

        private PlayerInitializer _startPlayer;
        private LocalSave _localSave;
        private Vector3 _targetRotation = new(0f, 90f, 0f);
        private YandexAds _yandexAds;
        private GameLoss _gameLoss;
        private int _id;
        private LastPropsSaver _lastPropsSaver;
        private LevelProgress _levelProgress;
        private OpenableSkinViewFiller _openableSkinViewFiller;
        private PlayerInitializer _template;
        private Pause.Pause _pause;

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
            _startPlayer = _playerSpawner.GetPlayer(_template);
            _startPlayer.Initialize(_playerInput);
            _lastPropsSaver = new LastPropsSaver(_props,
                _startPlayer.GroundDetector,
                _startPosition);
            _lastPropsSaver.Subscribe();
            _dataReaders.Add(_lastPropsSaver);
            _dataWriters.Add(_lastPropsSaver);
            _localSave = new(_dataReaders, _dataWriters);
            _localSave.Load();
            PlayerPosition spawnPosition = _lastPropsSaver.GetPlayerPositon();
            _openableSkinViewFiller = new(_skinConfigs, _openableSkinBar);
            _dataReaders.AddRange(new IDataReader[]
            {
                _wallet,
                _openableSkinViewFiller,
                _lastPropsSaver
            });
            _dataWriters.AddRange(new IDataWriter[]
            {
                _wallet,
                _openableSkinViewFiller,
                _lastPropsSaver
            });
            _startPlayer.SetPosition(spawnPosition.Position, _targetRotation);
            _levelProgress = new LevelProgress(_startPlayer, _finishPosition, _startPosition);
            _gameLoss = new GameLoss(_levelProgress, _startPlayer.Death);
            _gameLoss.Subscribe();
            _localSave = new(_dataReaders, _dataWriters);
            _localSave.Load();
            _followCamera.SetTarget(_startPlayer);
            _levelProgressView.Initialize(_levelProgress,
                _startPlayer.Finisher,
                _startPlayer.Death);
            _levelProgressView.Subscribe();
            _levelProgressView.UpdateProgressBar();
            _levelFinishView.Initialize(_startPlayer.Finisher, _levelLoader);
            _levelFinishView.Subscribe();
            _levelFinishView.NextLevelButtonClicked += OnNextLevelButtonClicked;
            _gameLossView.Initialize(_gameLoss, _lastPropsSaver);
            _gameLossView.Subscribe();
            _gameLossView.RetryButtonClicked += OnRetryButtonClicked;
            _gameLossView.RewardedButtonClicked += OnRewardedButtonClicked;
            _gameLossView.CloseAdOfferScreenButtonClicked += OnCloseAdOfferScreenButtonClicked;
            List<IPauseHandler> pauseHandlers = new()
            {
                _nextLevelButton,
                _retryButton,
                _playerInput,
                _gameLoss,
            };
            _pause = new Pause.Pause(pauseHandlers);
            _pauseView.Initialize(_pause);
            _pauseView.Subscribe();
            _pauseView.ContinueButtonClicked += OnContinueButtonClicked;
            _pauseView.ExitButtonClicked += OnExitButtonClicked;
            _pauseView.RestatrButtonClicked += OnRestartButtonClicked;
            _hud.Initialize(_levelLoader.GetCurrentNumber());
            _hud.Subscribe();
            _hud.PauseButtonClicked += OnPauseButtonClicked;
            _startPlayer.Finisher.LevelCompleted += OnLevelCompleted;
            _levelProgressView.Show();
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

        private void OnRewardedButtonClicked()
        {
            _localSave.Save();
            _levelLoader.Restart();
        }

        private void OnPauseButtonClicked()
            => _pause.Enable();

        private void OnContinueButtonClicked()
            => _pause.Disable();

        private void OnCloseAdOfferScreenButtonClicked()
        {
            _lastPropsSaver.ClearIndex();
            _levelLoader.Restart();
        }

        private void OnLevelCompleted()
            => _localSave.Save();

        private void OnNextLevelButtonClicked()
            => _levelLoader.GoNext();

        private void OnExitButtonClicked()
            => _levelLoader.ExitToMainMenu();

        private void OnRestartButtonClicked()
        {
            _lastPropsSaver.ClearIndex();
            _levelLoader.Restart();
        }

        private void GetPlayerTemplate()
        {
            _dataReaders.Add(this);
            _localSave = new LocalSave(_dataReaders, _dataWriters);
            _localSave.Load();
            string path = $"{PlayerParameter.PlayerPrefabsPath}{_id}";
            _template = Resources.Load<PlayerInitializer>(path);
        }

        private void OnRetryButtonClicked()
        {
            _lastPropsSaver.ClearIndex();
            _levelLoader.Restart();
        }

        private void Unsubscribe()
        {
            _startPlayer.Finisher.LevelCompleted -= OnLevelCompleted;
            _startPlayer.Unsubscribe();
            _levelFinishView.Unsubscribe();
            _gameLossView.RewardedButtonClicked -= OnRewardedButtonClicked;
            _gameLoss.Unsubscribe();
            _gameLossView.Unsubscribe();
            _gameLossView.RetryButtonClicked -= OnRetryButtonClicked;
            _gameLossView.CloseAdOfferScreenButtonClicked -= OnCloseAdOfferScreenButtonClicked;
            _levelProgressView.Unsubscribe();
            _lastPropsSaver.Unsubscribe();
            _pauseView.Unsubscribe();
            _hud.Unsubscribe();
            _hud.PauseButtonClicked -= OnPauseButtonClicked;
            _pauseView.ContinueButtonClicked -= OnContinueButtonClicked;
            _pauseView.ExitButtonClicked -= OnExitButtonClicked;
            _pauseView.RestatrButtonClicked -= OnRestartButtonClicked;
            _levelFinishView.NextLevelButtonClicked -= OnNextLevelButtonClicked;
        }
    }
}
