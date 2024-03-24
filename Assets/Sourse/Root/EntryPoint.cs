using System.Collections.Generic;
using Sourse.Save;
using Sourse.UI.Shop.SkinConfiguration;
using Sourse.UI.Shop.View.Common;
using UnityEngine;

namespace Sourse.Root
{
    public class EntryPoint : MonoBehaviour, IDataReader, IDataWriter
    {
        [SerializeField] private List<SkinConfig> _skinConfigs = new List<SkinConfig>();
        [SerializeField] private SkinView _temeplate;
        [SerializeField] private Transform _parent;

        private SkinViewSpawner _skinViewSpawner = new SkinViewSpawner();
        private List<SkinView> _skinViews = new List<SkinView>();
        private List<Skin> _skins = new List<Skin>();
        private SkinSpawner _skinSpawner = new SkinSpawner();
        private List<SkinSaveData> _skinSaveDatas;
        private SaveDataInjector _saveDataInjector;
        private ISaveLoadService _saveLoadService;
        private LocalSave _localSave;

        public void Initialize()
        {
            List<IDataWriter> dataWriters = new List<IDataWriter>
            {
                this
            };
            List<IDataReader> dataReaders = new List<IDataReader>
            {
                this
            };
            _localSave = new LocalSave(dataReaders, dataWriters);
            _saveLoadService = _localSave;
            _saveLoadService.Load();
            _saveDataInjector = new SaveDataInjector(_skinSaveDatas);

            foreach (var skinConfig in _skinConfigs)
            {
                var skinView = _skinViewSpawner.Get(_temeplate, _parent);
                var skin = _skinSpawner.Get(skinView, skinConfig);
                _skinViews.Add(skinView);
                _skins.Add(skin);
            }
        }

        public void Read(PlayerData playerData)
            => _skinSaveDatas = playerData.SkinSaveDatas;

        public void Write(PlayerData playerData)
            => playerData.SkinSaveDatas = _skinSaveDatas;
    }
}
