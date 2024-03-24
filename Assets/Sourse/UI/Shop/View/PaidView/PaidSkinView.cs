using Sourse.UI.Shop.Scripts;
using Sourse.UI.Shop.Scripts.Buttons;
using UnityEngine;

namespace Sourse.UI.Shop.View.Common.PaidView
{
    public class PaidSkinView : SkinView
    {
        [SerializeField] private BuyButton _byButton;
        [SerializeField] private PriceText _priceText;

        protected override void UpdateChildView()
        {
            //if (Skin.IsBought == true)
            //{
            //    _byButton.Hide();
            //    SelectButton.Show();
            //}
            //else if (Skin.IsBought == false)
            //{
            //    _priceText.SetText(Skin.Price);
            //    _byButton.Show();
            //}
        }

        protected override void Initialize()
        {
            _byButton.Initialize();
            _byButton.ButtonClicked += OnButtonClicked;
        }

        protected override void Deinitialize()
        {
            _byButton.ButtonClicked -= OnButtonClicked;
        }

        private void OnButtonClicked()
        {
            By();
        }
    }
}
