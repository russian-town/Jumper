using System;
using System.Collections.Generic;
using Sourse.Balance;
using Sourse.UI.Shop.SkinConfiguration;

namespace Sourse.UI.Shop.Scripts
{
    public class Shop
    {
        private Wallet _wallet;
        private List<Skin> _skins = new List<Skin>();
        private Skin _currentSelectedSkin;

        public event Action<Skin> Bought;
        public event Action<Skin> Selected;

        public void Initialize(List<Skin> skins, Wallet wallet)
        {
            _skins = skins;
            _wallet = wallet;
            SetDefaultSkin();
            SetSelectedSkin();
        }

        public void Subscribe()
        {
            foreach (var skin in _skins)
            {
                skin.BuyTried += OnBuyTried;
                skin.SelectTried += OnSelectTride;
            }
        }

        public void Unsubscribe()
        {
            foreach (var skin in _skins)
            {
                skin.BuyTried -= OnBuyTried;
                skin.SelectTried -= OnSelectTride;
            }
        }

        private void SetDefaultSkin()
            => _skins[0].Buy();

        private void SetSelectedSkin()
        {
            foreach (var skin in _skins)
            {
                if (skin.IsSelect == true)
                {
                    _currentSelectedSkin = skin;
                    return;
                }
            }

            _skins[0].Select();
            _currentSelectedSkin = _skins[0];
        }

        private void OnBuyTried(Skin skin)
        {
            if (skin.IsBought == true)
                return;

            if (skin.Price > _wallet.Money)
                return;

            _wallet.DicreaseMoney(skin.Price);
            skin.Buy();
            Bought?.Invoke(skin);
        }

        private void OnSelectTride(Skin skin)
        {
            if (skin.IsBought == false || skin.IsSelect == true)
                return;

            _currentSelectedSkin.Deselect();
            skin.Select();
            _currentSelectedSkin = skin;
            Selected?.Invoke(skin);
        }
    }
}
