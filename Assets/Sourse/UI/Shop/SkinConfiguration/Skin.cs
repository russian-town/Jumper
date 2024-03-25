using System;
using UnityEngine;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public class Skin
    {
        private readonly SkinView _skinView;
        private readonly SkinConfig _skinConfig;
        
        public Skin(SkinView skinView, SkinConfig skinConfig)
        {
            _skinView = skinView;
            _skinConfig = skinConfig;
            ID = _skinConfig.ID;
            Price = skinConfig.Price;
            Icon = skinConfig.Icon;
            _skinView.Initialize(this);
        }

        public bool IsSelect { get; private set; }
        public bool IsBought {  get; private set; }
        public int Price { get; private set; }
        public int ID { get; private set; }
        public Sprite Icon { get; private set; }

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
            _skinView.Subscribe();
            _skinView.BuyButtonClicked += OnBuyButtonClicked;
            _skinView.SelectButtonClicked += OnSelectButtonClicked;
        }

        public void Unsubscribe()
        {
            _skinView.Unsubscribe();
            _skinView.BuyButtonClicked -= OnBuyButtonClicked;
            _skinView.SelectButtonClicked -= OnSelectButtonClicked;
        }

        public void Select()
        {
            IsSelect = true;
            _skinView.UpdateView(this);
        }

        public void Buy()
        {
            IsBought = true;
            _skinView.UpdateView(this);
        }

        public void Deselect()
        {
            IsSelect = false;
            _skinView.UpdateView(this);
        }

        private void OnBuyButtonClicked()
            => BuyTried?.Invoke(this);

        private void OnSelectButtonClicked()
            => SelectTried?.Invoke(this);
    }
}
