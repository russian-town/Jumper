using System.Collections.Generic;
using Sourse.Balance;
using Sourse.Save;
using Sourse.UI.Shop.Scripts;
using Sourse.UI.Shop.SkinConfiguration;
using UnityEngine;

namespace Sourse.Root
{
    public class CompositionRoot : MonoBehaviour, IDataReader, IDataWriter
    {
        [SerializeField] private ShopScroll _shopScroll;
        [SerializeField] private List<SkinConfig> _skinConfigs = new();
        [SerializeField] private PaidSkinView _temeplate;
        [SerializeField] private Transform _parent;
        [SerializeField] private WalletView _walletView;

        private readonly Shop _shop = new();
        private readonly SkinViewSpawner _skinViewSpawner = new();
        private readonly List<PaidSkinView> _skinViews = new();
        private readonly List<Skin> _skins = new();
        private readonly List<OpenableSkin> _openableSkins = new();
        private readonly SkinSpawner _skinSpawner = new();
        private readonly Wallet _wallet = new();
        private readonly BubbleSort _bubbleSort = new();

        private List<SkinSaveData> _skinSaveDatas = new();
        private List<OpenableSkinSaveData> _openableSkinSaveDatas = new();
        private SaveDataInjector _saveDataInjector;
        private ISaveLoadService _saveLoadService;
        private LocalSave _localSave;

        private void OnDestroy()
            => Unsubscribe();

        private void Start()
            => Initialize();

        public void Read(PlayerData playerData)
        {
            _skinSaveDatas = playerData.SkinSaveDatas;
            _openableSkinSaveDatas = playerData.OpenableSkinSaveDatas;
        }

        public void Write(PlayerData playerData)
        {
            foreach (var skin in _skins)
                _saveDataInjector.Write(skin);

            foreach (var openableSkin in _openableSkins)
                _saveDataInjector.Write(openableSkin);
        }

        private void Unsubscribe()
        {
            _walletView.Unsubscribe();
            _shop.Unsubscribe();
            _shop.Bought -= OnBought;
            _shop.Selected -= OnSelected;
        }

        private void Initialize()
        {
            _wallet.AddMoney(1000);
            List<IDataWriter> dataWriters = new()
            {
                _wallet,
                this
            };
            List<IDataReader> dataReaders = new()
            {
                _wallet,
                this
            };
            _localSave = new LocalSave(dataReaders, dataWriters);
            _saveLoadService = _localSave;
            _saveLoadService.Load();
            _saveDataInjector = new SaveDataInjector(_skinSaveDatas, _openableSkinSaveDatas);
            _bubbleSort.SortingSkins(ref _skinConfigs);

            foreach (var skinConfig in _skinConfigs)
            {
                var skinView = _skinViewSpawner.Get(_temeplate, _parent);

                switch (skinConfig.Type)
                {
                    case SkinType.Paid:
                        var skin = _skinSpawner.CreateSkin(skinConfig);
                        _saveDataInjector.Update(skin);
                        skinView.Initialize(skin);
                        _skins.Add(skin);
                        break;
                    case SkinType.Openable:
                        var openableSkin = _skinSpawner.CreateOpenableSkin(skinConfig);
                        _saveDataInjector.Update(openableSkin);
                        skinView.Initialize(openableSkin);
                        _openableSkins.Add(openableSkin);
                        break;
                }

                _skinViews.Add(skinView);
            }

            _walletView.Initialize(_wallet);
            _walletView.Subscribe();
            List<Skin> skins = new();
            skins.AddRange(_skins);
            skins.AddRange(_openableSkins);
            _shop.Initialize(skins, _skinViews, _wallet);
            _shop.Subscribe();
            _shop.Bought += OnBought;
            _shop.Selected += OnSelected;
            _shopScroll.Initialize(_skinViews);
        }

        private void OnSelected(Skin skin)
           => _saveLoadService.Save();

        private void OnBought(Skin skin)
            => _saveLoadService.Save();
    }
}
