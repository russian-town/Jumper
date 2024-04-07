using System.Collections.Generic;
using Sourse.Balance;
using Sourse.Camera;
using Sourse.Constants;
using Sourse.Enviroment.Common;
using Sourse.Game.Finish;
using Sourse.Game.Lose;
using Sourse.Level;
using Sourse.PauseContent;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using Sourse.Settings.Audio;
using Sourse.UI;
using Sourse.UI.LevelCompletePanel;
using Sourse.UI.Shop.SkinConfiguration;
using Sourse.Yandex;
using UnityEngine;
using UnityEngine.Audio;

namespace Sourse.Root
{
    public class Root : MonoBehaviour, IDataReader, IDataWriter
    {
        [SerializeField] private FollowCamera _followCamera;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerPosition _startPosition;
        [SerializeField] private GameLossView _gameLossView;
        [SerializeField] private LevelProgressView _levelProgressView;
        [SerializeField] private LevelFinishView _levelFinishView;
        [SerializeField] private int _moneyOfLevel;
        [SerializeField] private PauseView _pauseView;
        [SerializeField] private RewardedPanel _rewardedPanel;
        [SerializeField] private List<Props> _props = new();
        [SerializeField] private FinishPosition _finishPosition;
        [SerializeField] private List<SkinConfig> _skinConfigs = new();
        [SerializeField] private OpenableSkinBar _openableSkinBar;
        [SerializeField] private HUD _hud;
        [SerializeField] private AudioMixerGroup _soundGroup;
        [SerializeField] private AudioMixerGroup _musicGroup;

        private readonly Wallet _wallet = new();
        private readonly LevelLoader _levelLoader = new();
        private readonly YandexAds _yandexAds = new();
        private readonly PlayerSpawner _playerSpawner = new();
        private readonly PlayerTemplateLoader _playerTemplateLoader = new();

        private List<IDataReader> _dataReaders = new();
        private List<IDataWriter> _dataWriters = new();
        private PlayerInitializer _startPlayer;
        private LocalSave _localSave;
        private GameLoss _gameLoss;
        private LevelProgress _levelProgress;
        private OpenableSkinViewFiller _openableSkinViewFiller;
        private ApplicationFocus _applicationFocus;
        private Pause _pause;
        private Audio _audio;
        private Vector3 _spawnPosition;
        private RestartLastPoint _restartLastPoint;

        private void OnDestroy()
            => Unsubscribe();

        private void Start()
            => Initialize();

        private void Update()
            => _levelProgressView.UpdateProgressBar();

        private void OnApplicationFocus(bool focus)
        {
            if (_applicationFocus == null)
                return;

            _applicationFocus.SetFocus(focus);
        }    

        public void Read(PlayerData playerData)
        {
            _spawnPosition = playerData.SpawnPosition;

            if (_spawnPosition == Vector3.zero)
            {
                _spawnPosition = _startPosition.Position;
                return;
            }
        }

        public void Write(PlayerData playerData)
            => playerData.SpawnPosition = _spawnPosition;

        private void Initialize()
        {
            _dataReaders.Add(_playerTemplateLoader);
            _dataReaders.Add(this);
            _localSave = new(_dataReaders, _dataWriters);
            _localSave.Load();
            PlayerInitializer template = _playerTemplateLoader.Get();
            _startPlayer = _playerSpawner.Create(template, _spawnPosition);
            _dataReaders = new();
            _dataWriters = new();
            _startPlayer.Initialize();
            _restartLastPoint = new(_levelLoader, _startPlayer.GroundDetector);
            _restartLastPoint.Subscribe();
            _playerInput.Initialize(_startPlayer.Animator);
            _openableSkinViewFiller = new(_skinConfigs, _openableSkinBar);
            _dataReaders.AddRange(new IDataReader[]
            {
                _wallet,
                _openableSkinViewFiller,
            });
            _dataWriters.AddRange(new IDataWriter[]
            {
                _wallet,
                _openableSkinViewFiller,
            });
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
            _gameLossView.Initialize(_gameLoss);
            _gameLossView.Subscribe();
            _gameLossView.RetryButtonClicked += OnRetryButtonClicked;
            _gameLossView.RewardedButtonClicked += OnRewardedButtonClicked;
            _yandexAds.RewardedCallback += OnRewardedCallback;
            _gameLossView.CloseAdOfferScreenButtonClicked += OnCloseAdOfferScreenButtonClicked;
            List<IPauseHandler> pauseHandlers = new()
            {
                _playerInput,
                _gameLoss,
            };
            _pause = new Pause(pauseHandlers);
            _audio = new Audio(_soundGroup, _musicGroup);
            _applicationFocus = new ApplicationFocus(_audio, _pause);
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
            _spawnPosition = Vector3.zero;
            _dataWriters.Add(this);
            _localSave.Save();
        }

        private void OnRewardedButtonClicked()
            => _yandexAds.ShowRewardedVideo();

        private void OnRewardedCallback()
        {
            _restartLastPoint.Accept();
            _dataWriters.Add(_restartLastPoint);
            _localSave.Save();
        }

        private void OnPauseButtonClicked()
            => _pause.Enable();

        private void OnContinueButtonClicked()
            => _pause.Disable();

        private void OnCloseAdOfferScreenButtonClicked()
            => _levelLoader.Restart();

        private void OnLevelCompleted()
        {
            _wallet.AddMoney(PlayerParameter.MoneyPerLevel);
            _openableSkinViewFiller.FillPercent();
            _localSave.Save();
        }

        private void OnNextLevelButtonClicked()
            => _levelLoader.GoNext();

        private void OnExitButtonClicked()
            => _levelLoader.ExitToMainMenu();

        private void OnRestartButtonClicked()
            => _levelLoader.Restart();

        private void OnRetryButtonClicked()
            => _levelLoader.Restart();

        private void Unsubscribe()
        {
            _startPlayer.Finisher.LevelCompleted -= OnLevelCompleted;
            _startPlayer.Unsubscribe();
            _levelFinishView.Unsubscribe();
            _gameLossView.RewardedButtonClicked -= OnRewardedButtonClicked;
            _yandexAds.RewardedCallback -= OnRewardedCallback;
            _restartLastPoint.Unsubscribe();
            _gameLoss.Unsubscribe();
            _gameLossView.Unsubscribe();
            _gameLossView.RetryButtonClicked -= OnRetryButtonClicked;
            _gameLossView.CloseAdOfferScreenButtonClicked -= OnCloseAdOfferScreenButtonClicked;
            _levelProgressView.Unsubscribe();
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
