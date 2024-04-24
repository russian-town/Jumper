using System.Collections.Generic;
using Sourse.Balance;
using Sourse.Camera;
using Sourse.Constants;
using Sourse.Enviroment.Common;
using Sourse.Game.FinishContent;
using Sourse.Game.Lose;
using Sourse.Ground;
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
    public class Root : MonoBehaviour
    {
        private readonly Wallet _wallet = new ();
        private readonly LevelLoader _levelLoader = new ();
        private readonly YandexAds _yandexAds = new ();
        private readonly PlayerSpawner _playerSpawner = new ();
        private readonly PlayerTemplateLoader _playerTemplateLoader = new ();
        private readonly SceneBuilder _sceneBuilder = new ();

        [SerializeField] private AudioMixerGroup _soundGroup;
        [SerializeField] private AudioMixerGroup _musicGroup;
        [SerializeField] private List<SceneConfig> _sceneConfigs = new ();
        [SerializeField] private List<SkinConfig> _skinConfigs = new ();
        [SerializeField] private Ground.DeadZone _groundTemplate;

        [SerializeField] private FollowCamera _followCamera; // Сделать через фабрику
        [SerializeField] private HUD _hud; // adresable

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
        private RestartLastPoint _restartLastPoint;
        private Finish _finish;
        private DeadZone _deadZone;

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

        private void Initialize()
        {
            _dataReaders.Add(_playerTemplateLoader);
            _localSave = new (_dataReaders, _dataWriters);
            _localSave.Load();
            List<Item> items = _sceneBuilder.Create(_sceneConfigs[0], _groundTemplate);
            PlayerInitializer template = _playerTemplateLoader.Get();
            _startPlayer = _playerSpawner.Create(template, items[0].Position);
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
            _levelProgress = new LevelProgress(_startPlayer,
                items[items.Count - 1].Position,
                items[0].Position);
            _gameLoss = new GameLoss(_levelProgress, _deadZone);
            _gameLoss.Subscribe();
            _localSave = new (_dataReaders, _dataWriters);
            _localSave.Load();
            _followCamera.SetTarget(_startPlayer); 
            _yandexAds.RewardedCallback += OnRewardedCallback;  
            List<IPauseHandler> pauseHandlers = new () { _hud };
            _pause = new Pause(pauseHandlers);
            _audio = new Audio(_soundGroup, _musicGroup);
            _applicationFocus = new ApplicationFocus(_audio, _pause);
            _hud.Initialize(
                _levelLoader.GetCurrentNumber(),
                _startPlayer,
                _levelProgress,
                _gameLoss,
                _pause,
                _openableSkinViewFiller,
                _finish,
                _deadZone);
            _hud.Subscribe();
            _finish.LevelCompleted += OnLevelCompleted;
            _localSave.Save();
        }

        private void OnRewardedCallback()
        {
            _restartLastPoint.Accept();
            _dataWriters.Add(_restartLastPoint);
            _localSave.Save();
        }

        private void OnLevelCompleted()
        {
            _wallet.AddMoney(PlayerParameter.MoneyPerLevel);
            _openableSkinViewFiller.CalculatePercent();
            _localSave.Save();
        }

        private void Unsubscribe()
        {
            _finish.LevelCompleted -= OnLevelCompleted;
            _startPlayer.Unsubscribe();
            _yandexAds.RewardedCallback -= OnRewardedCallback;
            _restartLastPoint.Unsubscribe();
            _gameLoss.Unsubscribe();
            _hud.Unsubscribe();
        }
    }
}
