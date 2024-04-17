using System.Collections.Generic;
using Sourse.Balance;
using Sourse.Camera;
using Sourse.Constants;
using Sourse.Game.FinishContent;
using Sourse.Game.Lose;
using Sourse.Level;
using Sourse.PauseContent;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using Sourse.SceneConfigurator;
using Sourse.Settings.Audio;
using Sourse.UI.Shop.SkinConfiguration;
using Sourse.Yandex;
using UnityEngine;
using UnityEngine.Audio;

namespace Sourse.Root
{
    public class Root : MonoBehaviour, IDataReader, IDataWriter
    {
        private readonly Wallet _wallet = new ();
        private readonly LevelLoader _levelLoader = new ();
        private readonly YandexAds _yandexAds = new ();
        private readonly PlayerSpawner _playerSpawner = new ();
        private readonly PlayerTemplateLoader _playerTemplateLoader = new ();

        [SerializeField] private AudioMixerGroup _soundGroup;
        [SerializeField] private AudioMixerGroup _musicGroup;
        [SerializeField] private List<SceneConfig> _sceneConfigs = new ();
        [SerializeField] private List<SkinConfig> _skinConfigs = new ();

        private FollowCamera _followCamera;
        private PlayerPosition _startPosition;
        private FinishPosition _finishPosition;
        private HUD _hud;

        private List<IDataReader> _dataReaders = new ();
        private List<IDataWriter> _dataWriters = new ();
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
            => _hud.UpdateView();

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
            _localSave = new (_dataReaders, _dataWriters);
            _localSave.Load();
            PlayerInitializer template = _playerTemplateLoader.Get();
            _startPlayer = _playerSpawner.Create(template, _spawnPosition);
            _dataReaders = new ();
            _dataWriters = new ();
            _startPlayer.Initialize();
            _restartLastPoint = new (_levelLoader, _startPlayer.GroundDetector);
            _restartLastPoint.Subscribe();    
            _openableSkinViewFiller = new (_skinConfigs);
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
            _localSave = new (_dataReaders, _dataWriters);
            _localSave.Load();
            _followCamera.SetTarget(_startPlayer); 
            _yandexAds.RewardedCallback += OnRewardedCallback;  
            List<IPauseHandler> pauseHandlers = new () { _hud };
            _pause = new Pause(pauseHandlers);
            _audio = new Audio(_soundGroup, _musicGroup);
            _applicationFocus = new ApplicationFocus(_audio, _pause);
            _hud.Initialize(_levelLoader.GetCurrentNumber(),
                _startPlayer,
                _levelProgress,
                _gameLoss,
                _pause);
            _hud.Subscribe();
            _hud.PauseButtonClicked += OnPauseButtonClicked;
            _startPlayer.Finisher.LevelCompleted += OnLevelCompleted;
            _spawnPosition = Vector3.zero;
            _dataWriters.Add(this);
            _localSave.Save();
        }

        private void OnRewardedCallback()
        {
            _restartLastPoint.Accept();
            _dataWriters.Add(_restartLastPoint);
            _localSave.Save();
        }

        private void OnPauseButtonClicked()
            => _pause.Enable();

        private void OnLevelCompleted()
        {
            _wallet.AddMoney(PlayerParameter.MoneyPerLevel);
            _openableSkinViewFiller.CalculatePercent();
            _localSave.Save();
        }

        private void Unsubscribe()
        {
            _startPlayer.Finisher.LevelCompleted -= OnLevelCompleted;
            _startPlayer.Unsubscribe();
            _yandexAds.RewardedCallback -= OnRewardedCallback;
            _restartLastPoint.Unsubscribe();
            _gameLoss.Unsubscribe();
            _hud.Unsubscribe();
            _hud.PauseButtonClicked -= OnPauseButtonClicked;
        }
    }
}
