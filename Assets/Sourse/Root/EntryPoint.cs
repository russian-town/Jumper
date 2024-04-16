using System.Collections;
using System.Collections.Generic;
using Lean.Localization;
using Sourse.Balance;
using Sourse.Constants;
using Sourse.Level;
using Sourse.Save;
using Sourse.Settings.Audio;
using Sourse.Settings.Language;
using Sourse.StartScene;
using Sourse.UI.Shop.SkinConfiguration;
using Sourse.Yandex;
using UnityEngine;
using UnityEngine.Audio;

namespace Sourse.Root
{
    public class EntryPoint : MonoBehaviour, IDataReader, IDataWriter
    {
        private readonly Wallet _wallet = new ();
        private readonly LevelLoader _levelLoader = new ();

        [SerializeField] private List<SkinConfig> _skinConfigs = new ();
        [SerializeField] private LeanLocalization _leanLocalization;
        [SerializeField] private AudioMixerGroup _soundGroup;
        [SerializeField] private AudioMixerGroup _musicGroup;
        [SerializeField] private LoadingBar _loadingBar;

        private YandexInitializer _yandexInitializer;
        private LanguageSettings _languageSettings;
        private Audio _audio;
        private ISaveLoadService _saveLoadService;
        private LocalSave _localSave;
        private List<SkinSaveData> _skinSaveData = new ();
        private List<SkinSaveData> _paidSkinSaveDatas = new ();
        private List<OpenableSkinSaveData> _openableSkinSaveDatas = new ();
        private SkinConfigOperation _skinConfigOperation;
        private Queue<ILoadingOperation> _loadingOperations = new ();
        private LoadingScreen _loadingScreen;

        private int _currentSelectedSkinID;

        private IEnumerator Start()
        {
            _languageSettings = new LanguageSettings(_leanLocalization);
            _yandexInitializer = new YandexInitializer(_languageSettings);
            yield return _yandexInitializer.Initialize(this);
            Initialize();
        }

        private void OnDestroy()
        {
            _loadingBar.Unsubscribe();
        }

        public void Read(PlayerData playerData)
        {
            _paidSkinSaveDatas = playerData.SkinSaveDatas;
            _openableSkinSaveDatas = playerData.OpenableSkinSaveDatas;
        }

        public void Write(PlayerData playerData)
        {
            playerData.SkinSaveDatas = _paidSkinSaveDatas;
            playerData.OpenableSkinSaveDatas = _openableSkinSaveDatas;
            playerData.CurrentSelectedSkinID = _currentSelectedSkinID;
        }

        private void Initialize()
        {
            _audio = new Audio(_soundGroup, _musicGroup);
            _localSave = new LocalSave(
                new List<IDataReader> { this, _wallet, _audio },
                new List<IDataWriter> { this, _wallet, _audio });
            _saveLoadService = _localSave;
            _saveLoadService.Load();
            _skinSaveData.AddRange(_paidSkinSaveDatas);
            _skinSaveData.AddRange(_openableSkinSaveDatas);
            LoadCurrentSelectedSkinId();
            _saveLoadService.Save();

            _skinConfigOperation = new (_paidSkinSaveDatas,
                _openableSkinSaveDatas,
                _skinConfigs,
                _saveLoadService);
            _loadingOperations.Enqueue(_skinConfigOperation);
            _loadingScreen = new (_loadingOperations, this, _levelLoader);
            _loadingBar.Initialize(_loadingScreen);
            _loadingScreen.StartLoad();
        }

        private void LoadCurrentSelectedSkinId()
        {
            foreach (var skinSaveData in _skinSaveData)
            {
                if (skinSaveData.IsSelect)
                {
                    _currentSelectedSkinID = skinSaveData.ID;
                    return;
                }
            }

            _currentSelectedSkinID = PlayerParameter.DefaultID;
        }
    }
}
