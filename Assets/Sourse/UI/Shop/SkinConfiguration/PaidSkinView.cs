using System;
using Sourse.UI.Shop.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public class PaidSkinView : SkinView
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private PriceText _priceText;
        [SerializeField] private Image _icon;

        private Skin _skin;

        public event Action<Skin> BuyButtonClicked;

        public override void Subscribe()
        {
            base.Subscribe();
            _buyButton.onClick.AddListener(()
                => BuyButtonClicked?.Invoke(_skin));
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            _buyButton.onClick.RemoveListener(()
                => BuyButtonClicked?.Invoke(_skin));
        }

        public void Initialize(Skin skin)
        {
            _skin = skin;
            _icon.sprite = skin.Icon;
            _priceText.SetText(skin.Price);
            UpdateView();
        }

        public override void UpdateView()
        {
            base.UpdateView();

            if (!_skin.IsBought)
            {
                _buyButton.gameObject.SetActive(true);
                _priceText.Show();
                return;
            }

            _priceText.Hide();
        }

        protected override Skin Skin()
        {
            return _skin;
        }
    }
}
