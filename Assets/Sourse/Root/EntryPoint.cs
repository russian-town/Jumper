using System.Collections.Generic;
using Sourse.Balance;
using Sourse.Save;
using Sourse.UI.Shop.Scripts;
using Sourse.UI.Shop.SkinConfiguration;
using UnityEngine;

namespace Sourse.Root
{
    public class EntryPoint : MonoBehaviour, IDataReader, IDataWriter
    {
        [SerializeField] private ShopScroll _shopScroll;
        [SerializeField] private List<SkinConfig> _skinConfigs = new List<SkinConfig>();
        [SerializeField] private SkinView _temeplate;
        [SerializeField] private Transform _parent;
        [SerializeField] private WalletView _walletView;

        private Shop _shop = new Shop();
        private SkinViewSpawner _skinViewSpawner = new SkinViewSpawner();
        private List<SkinView> _skinViews = new List<SkinView>();
        private List<Skin> _skins = new List<Skin>();
        private SkinSpawner _skinSpawner = new SkinSpawner();
        private List<SkinSaveData> _skinSaveDatas = new List<SkinSaveData>();
        private SaveDataInjector _saveDataInjector;
        private ISaveLoadService _saveLoadService;
        private PlayerData _playerData = new PlayerData();
        private LocalSave _localSave;
        private Wallet _wallet = new Wallet();
        private BubbleSort _bubbleSort = new BubbleSort();

        private void OnDestroy()
            => Unsubscribe();

        private void Start()
            => Initialize();

        private void Unsubscribe()
        {
            _walletView.Unsubscribe();
            _shop.Unsubscribe();

            foreach (var skin in _skins)
                skin.Unsubscribe();

            _shop.Bought -= OnBought;
            _shop.Selected -= OnSelected;
        }

        private void Initialize()
        {
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
                var skin = _skinSpawner.Get(skinView, skinConfig);
                _saveDataInjector.Update(skin);
                skin.Subscribe();
                _skinViews.Add(skinView);
                _skins.Add(skin);
            }

            _walletView.Initialize(_wallet);
            _walletView.Subscribe();
            _shop.Initialize(_skins, _wallet);
            _shop.Subscribe();
            _shop.Bought += OnBought;
            _shop.Selected += OnSelected;
            _shopScroll.Initialize(_skinViews);
        }

        public void Read(PlayerData playerData)
            => _skinSaveDatas = playerData.SkinSaveDatas;

        public void Write(PlayerData playerData)
        {
            if (playerData.SkinSaveDatas == null)
                playerData.SkinSaveDatas = new List<SkinSaveData>();

            if (playerData.SkinSaveDatas.Count == 0)
            {
                foreach (var skin in _skins)
                {
                    SkinSaveData skinSaveData = new SkinSaveData();
                    FillSkinSaveData(skinSaveData, skin);
                    playerData.SkinSaveDatas.Add(skinSaveData);
                }

                return;
            }

            foreach (var skin in _skins)
            {
                SkinSaveData skinSaveData = SearchSkinSaveData(playerData, skin);

                if (skinSaveData != null)
                {
                    FillSkinSaveData(skinSaveData, skin);
                }
                else
                {
                    FillSkinSaveData(skinSaveData, skin);
                    playerData.SkinSaveDatas.Add(skinSaveData);
                }
            }
        }

        private void FillSkinSaveData(SkinSaveData skinSaveData, Skin skin)
        {
            skinSaveData.ID = skin.ID;
            skinSaveData.IsBought = skin.IsBought;
            skinSaveData.IsSelect = skin.IsSelect;
        }

        private SkinSaveData SearchSkinSaveData(PlayerData playerData, Skin skin)
        {
            foreach (var skinSaveData in playerData.SkinSaveDatas)
                if (skinSaveData.ID == skin.ID)
                    return skinSaveData;

            return null;
        }

        private void OnSelected(Skin skin)
           => _saveLoadService.Save(_playerData);

        private void OnBought(Skin skin)
            => _saveLoadService.Save(_playerData);
    }
}
