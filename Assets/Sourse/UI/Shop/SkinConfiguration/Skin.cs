using System;
using Sourse.UI.Shop.View.Common;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public class Skin
    {
        private readonly SkinView _skinView;
        private readonly SkinConfig _skinConfig;
        
        private bool _isSelect;

        public Skin(SkinView skinView, SkinConfig skinConfig)
        {
            _skinView = skinView;
            _skinConfig = skinConfig;
            ID = _skinConfig.ID;
            Price = skinConfig.Price;
        }

        public bool IsBought {  get; private set; }
        public int Price { get; private set; }
        public int ID { get; private set; }

        public event Action<Skin> BuyTried;
        public event Action<Skin> SelectTried;

        public void ApplySaveData(SkinSaveData skinSaveData)
        {
            if (skinSaveData.IsSelect)
                Select();

            if (skinSaveData.IsBought)
                Buy();
        }

        public void Subscribe()
        {
            _skinView.BuyButtonClicked += OnBuyButtonClicked;
            _skinView.SelectButtonClicked += OnSelectButtonClicked;
        }

        public void Unsubscribe()
        {
            _skinView.BuyButtonClicked -= OnBuyButtonClicked;
            _skinView.SelectButtonClicked -= OnSelectButtonClicked;
        }

        public void Select()
        {
            if (IsBought == false)
                return;

            _isSelect = true;
            _skinView.UpdateView();
        }

        public void Buy()
        {
            IsBought = true;
            _skinView.UpdateView();
        }

        public void Deselect()
        {
            _isSelect = false;
            _skinView.UpdateView();
        }

        private void OnBuyButtonClicked()
            => BuyTried?.Invoke(this);

        private void OnSelectButtonClicked()
            => SelectTried?.Invoke(this);
    }
}
