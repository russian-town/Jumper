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

        public event Action<Skin> Bought;
        public event Action<Skin> Selected;

        public void Initialize(List<Skin> skins, Wallet wallet)
        {
            _skins = skins;
            _wallet = wallet;
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

        private void OnBuyTried(Skin skin)
        {
            if (skin.Price > _wallet.Money)
                return;

            _wallet.DicreaseMoney(skin.Price);
            skin.Buy();
            Bought?.Invoke(skin);
        }

        private void OnSelectTride(Skin skin)
        {
            if (skin.IsBought == false)
                return;

            skin.Select();
            Selected?.Invoke(skin);
        }
    }
}
