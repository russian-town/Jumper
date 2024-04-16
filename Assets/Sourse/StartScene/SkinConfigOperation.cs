using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sourse.Save;
using Sourse.UI.Shop.SkinConfiguration;

namespace Sourse.StartScene
{
    public class SkinConfigOperation : ILoadingOperation
    {
        private readonly SkinSpawner _skinSpawner = new();
        private readonly SkinSaveDataSpawner _skinSaveDataSpawner = new();

        private List<SkinSaveData> _paidSkinSaveDatas = new();
        private List<OpenableSkinSaveData> _openableSkinSaveDatas = new();
        private List<SkinConfig> _skinConfigs = new();
        private ISaveLoadService _saveLoadService;

        public SkinConfigOperation(List<SkinSaveData> paidSkinSaveDatas,
            List<OpenableSkinSaveData> openableSkinSaveDatas,
            List<SkinConfig> skinConfigs,
            ISaveLoadService saveLoadService)
        {
            _paidSkinSaveDatas = paidSkinSaveDatas;
            _openableSkinSaveDatas = openableSkinSaveDatas;
            _skinConfigs = skinConfigs;
            _saveLoadService = saveLoadService;
        }

        public Task Load(Action<float> onProgress)
        {
            onProgress?.Invoke(.3f);
            foreach (var skinConfig in _skinConfigs)
            {
                switch (skinConfig.Type)
                {
                    case SkinType.Paid:
                        AddPaidSkinSaveData(skinConfig);
                        break;
                    case SkinType.Openable:
                        AddOpenableSkinSaveData(skinConfig);
                        break;
                }

                onProgress?.Invoke(.9f);
            }

            _saveLoadService.Save();
            onProgress?.Invoke(1f);
            return Task.CompletedTask;
        }

        private void AddPaidSkinSaveData(SkinConfig skinConfig)
        {
            foreach (var data in _paidSkinSaveDatas)
            {
                if (skinConfig.ID == data.ID)
                    return;
            }

            Skin skin = _skinSpawner.CreateSkin(skinConfig);
            var skinSaveData = _skinSaveDataSpawner.CreateSkinSaveData(skin);
            _paidSkinSaveDatas.Add(skinSaveData);
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
    }
}
