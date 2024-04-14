using System.Collections.Generic;
using Sourse.Balance;
using Sourse.Level;
using Sourse.Save;
using Sourse.UI.Shop.Scripts;
using Sourse.UI.Shop.SkinConfiguration;
using UnityEngine;

namespace Sourse.Root
{
    public class CompositionRoot : MonoBehaviour, IDataReader, IDataWriter
    {
        private readonly Shop _shop = new ();
        private readonly SkinViewSpawner _skinViewSpawner = new ();
        private readonly List<PaidSkinView> _paidSkinViews = new ();
        private readonly List<OpenableSkinView> _openableSkinViews = new ();
        private readonly List<RewardedSkinView> _rewardedSkinViews = new ();
        private readonly List<Skin> _skins = new ();
        private readonly List<OpenableSkin> _openableSkins = new ();
        private readonly SkinSpawner _skinSpawner = new ();
        private readonly Wallet _wallet = new ();
        private readonly BubbleSort _bubbleSort = new ();
        private readonly LevelLoader _levelLoader = new ();

        [SerializeField] private ShopScroll _shopScroll;
        [SerializeField] private List<SkinConfig> _skinConfigs = new ();
        [SerializeField] private PaidSkinView _paidSkinViewTemplate;
        [SerializeField] private OpenableSkinView _openableSkinViewTemplate;
        [SerializeField] private RewardedSkinView _rewardedSkinViewTemplate;
        [SerializeField] private Transform _parent;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private ShopView _shopView;

        private List<SkinSaveData> _skinSaveDatas = new ();
        private List<OpenableSkinSaveData> _openableSkinSaveDatas = new ();
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
            _shopView.CloseButtonClicked -= OnCloseButtonClicked;
        }

        private void Initialize()
        {
            List<IDataWriter> dataWriters = new ()
            {
                _wallet,
                this,
            };
            List<IDataReader> dataReaders = new ()
            {
                _wallet,
                this,
            };
            _localSave = new LocalSave(dataReaders, dataWriters);
            _saveLoadService = _localSave;
            _saveLoadService.Load();
            _saveDataInjector = new SaveDataInjector(_skinSaveDatas, _openableSkinSaveDatas);
            _bubbleSort.SortingSkins(ref _skinConfigs);

            foreach (var skinConfig in _skinConfigs)
            {
                switch (skinConfig.Type)
                {
                    case SkinType.Paid:
                        var paidSkinView = _skinViewSpawner.Get(
                            _paidSkinViewTemplate,
                            _parent);
                        var skin = _skinSpawner.CreateSkin(skinConfig);
                        _saveDataInjector.Update(skin);
                        paidSkinView.Initialize(skin);
                        _skins.Add(skin);
                        _paidSkinViews.Add(paidSkinView);
                        break;
                    case SkinType.Openable:
                        var openableSkinView = _skinViewSpawner.Get(
                            _openableSkinViewTemplate,
                            _parent);
                        var openableSkin = _skinSpawner.CreateOpenableSkin(skinConfig);
                        _saveDataInjector.Update(openableSkin);
                        openableSkinView.Initialize(openableSkin);
                        _openableSkins.Add(openableSkin);
                        _openableSkinViews.Add(openableSkinView);
                        break;
                    case SkinType.Rewarded:
                        var rewardedSkinView = _skinViewSpawner.Get(
                            _rewardedSkinViewTemplate,
                            _parent);
                        var skin1 = _skinSpawner.CreateSkin(skinConfig);
                        _saveDataInjector.Update(skin1);
                        rewardedSkinView.Initialize(skin1);
                        _skins.Add(skin1);
                        _rewardedSkinViews.Add(rewardedSkinView);
                        break;
                }
            }

            _walletView.Initialize(_wallet);
            _walletView.Subscribe();
            List<Skin> skins = new ();
            skins.AddRange(_skins);
            skins.AddRange(_openableSkins);
            _shop.Initialize(skins,
                _paidSkinViews,
                _openableSkinViews,
                _rewardedSkinViews,
                _wallet);
            _shop.Subscribe();
            _shop.Bought += OnBought;
            _shop.Selected += OnSelected;
            _shopView.Initialize();
            _shopView.Subscribe();
            _shopView.CloseButtonClicked += OnCloseButtonClicked;
            List<SkinView> skinViews = new ();
            skinViews.AddRange(_paidSkinViews);
            skinViews.AddRange(_openableSkinViews);
            skinViews.AddRange(_rewardedSkinViews);
            _shopScroll.Initialize(skinViews);
        }

        private void OnSelected(Skin skin)
           => _saveLoadService.Save();

        private void OnBought(Skin skin)
            => _saveLoadService.Save();

        private void OnCloseButtonClicked()
            => _levelLoader.ExitToMainMenu();
    }
}
