using System.Collections;
using System.Collections.Generic;
using Lean.Localization;
using Sourse.Balance;
using Sourse.Level;
using Sourse.Menu;
using Sourse.Save;
using Sourse.Settings.Audio;
using Sourse.Settings.Language;
using Sourse.UI.Shop.SkinConfiguration;
using Sourse.Yandex;
using UnityEngine;
using UnityEngine.Audio;

namespace Sourse.Root
{
    public class EntryPoint : MonoBehaviour, IDataReader, IDataWriter
    {
        [SerializeField] private List<SkinConfig> _skinConfigs = new();
        [SerializeField] private AudioView _audioView;
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private LeanLocalization _leanLocalization;
        [SerializeField] private AudioMixerGroup _soundGroup;
        [SerializeField] private AudioMixerGroup _musicGroup;

        private readonly SkinSpawner _skinSpawner = new();
        private readonly SkinSaveDataSpawner _skinSaveDataSpawner = new();
        private readonly Wallet _wallet = new();
        private readonly LevelLoader _levelLoader = new();

        private YandexInitializer _yandexInitializer;
        private LanguageSettings _languageSettings;
        private Audio _audio;
        private ISaveLoadService _saveLoadService;
        private LocalSave _localSave;
        private List<SkinSaveData> _skinSaveDatas = new();
        private List<OpenableSkinSaveData> _openableSkinSaveDatas = new();

        private void OnDestroy()
            => Unsubscribe();

        private IEnumerator Start()
        {
            _languageSettings = new LanguageSettings(_leanLocalization);
            _yandexInitializer = new YandexInitializer(_languageSettings);
            yield return _yandexInitializer.Initialize(this);
            Initialize();
        }

        public void Read(PlayerData playerData)
        {
            _skinSaveDatas = playerData.SkinSaveDatas;
            _openableSkinSaveDatas = playerData.OpenableSkinSaveDatas;
        }

        public void Write(PlayerData playerData)
        {
            playerData.SkinSaveDatas = _skinSaveDatas;
            playerData.OpenableSkinSaveDatas = _openableSkinSaveDatas;
        }

        private void Initialize()
        {
            _audio = new Audio(_soundGroup, _musicGroup);
            _audioView.Initialize(_audio);
            _audioView.Subscribe();
            _audioView.Closed += OnClosed;
            _mainMenuView.Initialize();
            _mainMenuView.Subscribe();
            _mainMenuView.ShopButtonClicked += OnShopButtonClicked;
            _mainMenuView.PlayButtonClicked += OnPlayButtonClicked;
            _localSave = new LocalSave(new List<IDataReader> { this, _wallet, _audio },
                new List<IDataWriter> { this, _wallet, _audio});
            _saveLoadService = _localSave;
            _saveLoadService.Load();
            LoadSkinProgress();
        }

        private void OnShopButtonClicked()
            => _levelLoader.OpenShop();

        private void OnPlayButtonClicked()
            => _levelLoader.GoNext();

        private void Unsubscribe()
        {
            _audioView.Unsubscribe();
            _audioView.Closed -= OnClosed;
            _mainMenuView.Unsubscribe();
            _mainMenuView.ShopButtonClicked -= OnShopButtonClicked;
            _mainMenuView.PlayButtonClicked -= OnPlayButtonClicked;
        }

        private void LoadSkinProgress()
        {
            foreach (var skinConfig in _skinConfigs)
            {
                switch (skinConfig.Type)
                {
                    case SkinType.Paid:
                        AddSkinSaveData(skinConfig);
                        break;
                    case SkinType.Openable:
                        AddOpenableSkinSaveData(skinConfig);
                        break;
                }
            }

            _saveLoadService.Save();
        }

        private void AddSkinSaveData(SkinConfig skinConfig)
        {
            foreach (var data in _skinSaveDatas)
            {
                if (skinConfig.ID == data.ID)
                    return;
            }

            Skin skin = _skinSpawner.CreateSkin(skinConfig);
            var skinSaveData = _skinSaveDataSpawner.CreateSkinSaveData(skin);
            _skinSaveDatas.Add(skinSaveData);
        }

        private void AddOpenableSkinSaveData(SkinConfig skinConfig)
        {
            foreach (var data in _openableSkinSaveDatas)
            {
                if (skinConfig.ID == data.ID)
                    return;
            }

            OpenableSkin openableSkin =
                _skinSpawner.CreateOpenableSkin(skinConfig);
            var openableSkinSaveData =
                _skinSaveDataSpawner.CreateOpenableSkinSaveData(openableSkin);
            _openableSkinSaveDatas.Add(openableSkinSaveData);
        }

        private void OnClosed()
            => _saveLoadService.Save();
    }
}
