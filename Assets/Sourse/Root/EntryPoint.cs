using System.Collections.Generic;
using Sourse.Balance;
using Sourse.Save;
using Sourse.UI.Shop.SkinConfiguration;
using UnityEngine;

namespace Sourse.Root
{
    public class EntryPoint : MonoBehaviour, IDataReader, IDataWriter
    {
        [SerializeField] private List<SkinConfig> _skinConfigs = new();

        private readonly SkinSpawner _skinSpawner = new();
        private readonly SkinSaveDataSpawner _skinSaveDataSpawner = new();
        private readonly Wallet _wallet = new();

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
            _wallet.AddMoney(1000);
            _localSave = new LocalSave(new List<IDataReader> { this },
                new List<IDataWriter> { this, _wallet});
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

            OpenableSkin openableSkin = _skinSpawner.CreateOpenableSkin(skinConfig);
            var openableSkinSaveData = _skinSaveDataSpawner.CreateOpenableSkinSaveData(openableSkin);
            _openableSkinSaveDatas.Add(openableSkinSaveData);
        }
    }
}
