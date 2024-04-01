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
        [SerializeField] private List<SkinConfig> _skinConfigs = new List<SkinConfig>();
        [SerializeField] private SkinView _temeplate;
        [SerializeField] private Transform _parent;
        [SerializeField] private WalletView _walletView;

        private readonly Shop _shop = new Shop();
        private readonly SkinViewSpawner _skinViewSpawner = new SkinViewSpawner();
        private readonly List<SkinView> _skinViews = new List<SkinView>();
        private readonly List<Skin> _skins = new List<Skin>();
        private readonly SkinSpawner _skinSpawner = new SkinSpawner();
        private readonly Wallet _wallet = new Wallet();
        private readonly BubbleSort _bubbleSort = new BubbleSort();

        private List<SkinSaveData> _skinSaveDatas = new List<SkinSaveData>();
        private SaveDataInjector _saveDataInjector;
        private ISaveLoadService _saveLoadService;
        private LocalSave _localSave;

        private void OnDestroy()
            => Unsubscribe();

        private void Start()
            => Initialize();

        public void Read(PlayerData playerData)
            => _skinSaveDatas = playerData.SkinSaveDatas;

        public void Write(PlayerData playerData)
        {
            foreach (var skin in _skins)
                _saveDataInjector.Write(skin);
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
            List<IDataWriter> dataWriters = new List<IDataWriter>
            {
                _wallet,
                this
            };
            List<IDataReader> dataReaders = new List<IDataReader>
            {
                _wallet,
                this
            };
            _localSave = new LocalSave(dataReaders, dataWriters);
            _saveLoadService = _localSave;
            _saveLoadService.Load();
            _saveDataInjector = new SaveDataInjector(_skinSaveDatas);
            _bubbleSort.SortingSkins(ref _skinConfigs);

            foreach (var skinConfig in _skinConfigs)
            {
                var skinView = _skinViewSpawner.Get(_temeplate, _parent);
                var skin = _skinSpawner.CreateSkin(skinConfig);
                _saveDataInjector.Update(skin);
                skinView.Initialize(skin);
                _skinViews.Add(skinView);
                _skins.Add(skin);
            }

            _walletView.Initialize(_wallet);
            _walletView.Subscribe();
            _shop.Initialize(_skins, _skinViews, _wallet);
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
