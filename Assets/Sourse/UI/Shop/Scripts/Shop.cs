using System;
using System.Collections.Generic;
using Sourse.Balance;
using Sourse.UI.Shop.SkinConfiguration;
using Sourse.UI.Shop.View.Common;
using UnityEngine;

namespace Sourse.UI.Shop.Scripts
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopScroll _shopScroll;

        private Wallet _wallet;
        private List<Skin> _skins = new List<Skin>();
        private SkinViewSpawner _skinViewSpawner = new SkinViewSpawner();
        private List<SkinView> _spawnedSkinsView = new List<SkinView>();
        private SkinView _currentSkinView;
        private SkinConfig _currentSkin;
        private int _selectedID;

        public event Action<Skin> Bought;
        public event Action<Skin> Selected;

        public void Initialize(List<Skin> skins)
        { 
            _skins = skins;
        }

        public bool TryBuySkin(Skin skin)
        {
            if (skin.Price <= _wallet.Money)
            {
                _wallet.DicreaseMoney(skin.Price);
                skin.Buy();
                Bought?.Invoke(skin);
                return true;
            }

            return false;
        }

        public bool TrySelect(Skin skin)
        {
            if (skin.IsBought == true)
            {
                skin.Select();
                Selected?.Invoke(skin);
                return true;
            }

            return false;
        }

        private void OnSkinViewSelected(SkinConfig skin, SkinView skinView)
        {
            _currentSkinView = skinView;
            _currentSkin = skin;
            _selectedID = skin.ID;
        }

        private void OnBuyButtonClicked(Skin skin, SkinView skinView)
        {
            if (TryBuySkin(skin))
                skinView.UpdateView();
        }

        private void OnSelectButtonClicked(Skin skin, SkinView skinView)
        {
            if (TrySelect(skin))
            {
                if (_currentSkinView != null)
                    _currentSkinView.UpdateView();

                skinView.UpdateView();
                _currentSkinView = skinView;
            }
        }

        private void DeselectSkin(int id)
        {
        }
    }
}
