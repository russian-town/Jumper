using System.Collections.Generic;
using Sourse.Save;
using Sourse.UI.Shop.SkinConfiguration;
using UnityEngine;

namespace Sourse.Root
{
    public class EntryPoint : MonoBehaviour, IDataReader, IDataWriter
    {
        [SerializeField] private List<SkinConfig> _skinConfigs = new();

        private readonly SkinSpawner _skinSpawner = new();

        private ISaveLoadService _saveLoadService;
        private LocalSave _localSave;
        private List<SkinSaveData> _skinSaveDatas = new();
        private List<OpenableSkinSaveData> _openableSkinSaveDatas = new();

        private void Start()
            => Initialize();

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
            _localSave = new LocalSave(new List<IDataReader> { this },
                new List<IDataWriter> { this});
            _saveLoadService = _localSave;
            _saveLoadService.Load();
            TakeSkinProgress();
        }

        private void TakeSkinProgress()
        {
            foreach (var skinConfig in _skinConfigs)
            {
                switch (skinConfig.Type)
                {
                    case SkinType.Paid:
                        TakeSkinSaveData(skinConfig);
                        break;
                    case SkinType.Openable:
                        TakeOpenableSkinSaveData(skinConfig);
                        break;
                }
            }

            _saveLoadService.Save();
        }

        private void TakeSkinSaveData(SkinConfig skinConfig)
        {
            foreach (var data in _skinSaveDatas)
            {
                if (skinConfig.ID == data.ID)
                    return;
            }

            Skin skin = _skinSpawner.CreateSkin(skinConfig);
            SkinSaveData skinSaveData = new();
            skinSaveData.ID = skinConfig.ID;
            skinSaveData.IsBought = skin.IsBought;
            skinSaveData.IsSelect = skin.IsSelect;
            _skinSaveDatas.Add(skinSaveData);
        }

        private void TakeOpenableSkinSaveData(SkinConfig skinConfig)
        {
            foreach (var data in _openableSkinSaveDatas)
            {
                if (skinConfig.ID == data.ID)
                    return;
            }

            OpenableSkin openableSkin = _skinSpawner.CreateOpenableSkin(skinConfig);
            OpenableSkinSaveData openableSkinSaveData = new();
            openableSkinSaveData.ID = openableSkin.ID;
            openableSkinSaveData.IsBought = openableSkin.IsBought;
            openableSkinSaveData.IsSelect = openableSkin.IsSelect;
            openableSkinSaveData.Persent = openableSkin.Percent;
            _openableSkinSaveDatas.Add(openableSkinSaveData);
        }
    }
}
