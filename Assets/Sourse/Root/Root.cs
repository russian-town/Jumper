using System.Collections.Generic;
using Sourse.Balance;
using Sourse.Camera;
using Sourse.Enviroment.Common;
using Sourse.Game;
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
    public class Root : MonoBehaviour
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

        private List<IDataReader> _dataReaders = new();
        private List<IDataWriter> _dataWriters = new();
        private PlayerServis _playerServis;
        private PlayerInitializer _startPlayer;
        private LocalSave _localSave;
        private GameLoss _gameLoss;
        private LastPropsSaver _lastPropsSaver;
        private LevelProgress _levelProgress;
        private OpenableSkinViewFiller _openableSkinViewFiller;
        private ApplicationFocus _applicationFocus;
        private Pause _pause;
        private Audio _audio;

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

        private void Initialize()
        {
            _lastPropsSaver = new LastPropsSaver(_props, _startPosition);
            _playerServis = new PlayerServis(_lastPropsSaver);
            _playerServis.PlayerSpawned += OnPlayerSpawned;       
            _dataReaders.Add(_lastPropsSaver);
            _dataReaders.Add(_playerServis);
            _dataWriters.Add(_lastPropsSaver);
            _localSave = new(_dataReaders, _dataWriters);
            _localSave.Load();
        }

        private void OnPlayerSpawned(PlayerInitializer startPlayer)
        {
            _dataReaders = new();
            _dataWriters = new();
            _startPlayer = startPlayer;
            _startPlayer.Initialize();
            _playerInput.Initialize(_startPlayer.Animator);
            _lastPropsSaver.Initialize(_startPlayer.GroundDetector);
            _lastPropsSaver.Subscribe();
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
        }

        private void OnRewardedButtonClicked()
        {
            _lastPropsSaver.SaveIndex();
            _localSave.Save();
            _levelLoader.Restart();
        }

        private void OnPauseButtonClicked()
            => _pause.Enable();

        private void OnContinueButtonClicked()
            => _pause.Disable();

        private void OnCloseAdOfferScreenButtonClicked()
        {
            _yandexAds.ShowInterstitial();
            _applicationFocus.Disable();
            _audio.Mute();
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

        private void OnRetryButtonClicked()
        {
            _lastPropsSaver.ClearIndex();
            _levelLoader.Restart();
        }

        private void Unsubscribe()
        {
            _playerServis.PlayerSpawned -= OnPlayerSpawned;
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
