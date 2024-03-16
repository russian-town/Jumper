using System;
using System.Collections.Generic;
using Sourse.UI.Shop.SkinView.Common;
using UnityEngine;

namespace Sourse.UI.Shop.Scripts
{
    [RequireComponent(typeof(SkinViewSpawner))]
    public class Shop : SkinHandler.SkinHandler
    {
        [SerializeField] private ShopScroll _shopScroll;
        [SerializeField] private Wallet.Wallet _wallet;
        [SerializeField] private Skin.Skin _defaultSkin;

        private SkinViewSpawner _skinViewSpawner;
        private List<SkinView.Common.SkinView> _spawnedSkinsView = new List<SkinView.Common.SkinView>();
        private SkinView.Common.SkinView _currentSkinView;
        private Skin.Skin _currentSkin;
        private int _selectedID;

        public event Action<int> Selected;
        public event Action<int> Initialized;

        private void Awake()
        {
            _skinViewSpawner = GetComponent<SkinViewSpawner>();
            Initialize();

            for (int i = 0; i < Skins.Count; i++)
            {
                var spawnedSkinView = _skinViewSpawner.GetSkinView(Skins[i]);
                spawnedSkinView.Selected += OnSkinViewSelected;
                spawnedSkinView.ByButtonClicked += OnBuyButtonClicked;
                spawnedSkinView.SelectButtonClicked += OnSelectButtonClicked;
                spawnedSkinView.Initialize(Skins[i]);
                _spawnedSkinsView.Add(spawnedSkinView);
            }

            if (_currentSkin == null)
            {
                _currentSkin = _defaultSkin;
                _defaultSkin.Select();
                _currentSkinView = _skinViewSpawner.DefaultSkin;
                _currentSkinView.UpdateView();
                _selectedID = _defaultSkin.ID;
            }

            _shopScroll.Initialize(_spawnedSkinsView);
            Initialized?.Invoke(_selectedID);
        }

        private void OnDisable()
        {
            foreach (var skinView in _spawnedSkinsView)
            {
                skinView.Selected -= OnSkinViewSelected;
                skinView.ByButtonClicked -= OnBuyButtonClicked;
                skinView.SelectButtonClicked -= OnSelectButtonClicked;
            }
        }

        public void OpenSkin(int id) => TryBuySkin(id);

        public bool TryBuySkin(int id)
        {
            if (TrySearchByID(id, out Skin.Skin skin) == true)
            {
                if (skin.Price <= _wallet.Money)
                {
                    _wallet.DicreaseMoney(skin.Price);
                    skin.Buy();
                    Saver.SaveState(IsByKey, skin.ID, skin.IsBought);
                    return true;
                }
            }

            return false;
        }

        public bool TrySelect(int id)
        {
            if (TrySearchByID(id, out Skin.Skin skin))
            {
                if (skin.IsBought == true)
                {
                    DeselectSkin(_selectedID);
                    skin.Select();
                    Saver.SaveState(IsSelectKey, skin.ID, skin.IsSelect);
                    _selectedID = id;
                    Saver.SaveSelectedID(_selectedID);
                    Selected?.Invoke(id);
                    return true;
                }
            }

            return false;
        }

        private bool TrySearchByID(int id, out Skin.Skin skin)
        {
            for (int i = 0; i < Skins.Count; i++)
            {
                if (Skins[i].ID == id)
                {
                    skin = Skins[i];
                    return true;
                }
            }

            skin = null;
            return false;
        }

        private void OnSkinViewSelected(Skin.Skin skin, SkinView.Common.SkinView skinView)
        {
            _currentSkinView = skinView;
            _currentSkin = skin;
            _selectedID = skin.ID;
        }

        private void OnBuyButtonClicked(Skin.Skin skin, SkinView.Common.SkinView skinView)
        {
            if (TryBuySkin(skin.ID))
                skinView.UpdateView();
        }

        private void OnSelectButtonClicked(Skin.Skin skin, SkinView.Common.SkinView skinView)
        {
            if (TrySelect(skin.ID))
            {
                if (_currentSkinView != null)
                    _currentSkinView.UpdateView();

                skinView.UpdateView();
                _currentSkinView = skinView;
            }
        }

        private void DeselectSkin(int id)
        {
            if (TrySearchByID(id, out Skin.Skin skin))
            {
                skin.Deselect();
                Saver.SaveState(IsSelectKey, skin.ID, skin.IsSelect);
            }
        }
    }
}
