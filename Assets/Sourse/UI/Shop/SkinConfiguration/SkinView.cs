using System;
using Sourse.Constants;
using Sourse.UI.Shop.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public class SkinView : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TMP_Text _selectText;
        [SerializeField] private PriceText _priceText;
        [SerializeField] private Image _icon;

        private Skin _skin;

        public event Action<Skin, SkinView> BuyButtonClicked;
        public event Action<Skin, SkinView> SelectButtonClicked;

        public void Subscribe()
        {
            _buyButton.onClick.AddListener(() => BuyButtonClicked?.Invoke(_skin, this));
            _selectButton.onClick.AddListener(() => SelectButtonClicked?.Invoke(_skin, this));
        }

        public void Unsubscribe()
        {
            _buyButton.onClick.RemoveListener(() => BuyButtonClicked?.Invoke(_skin, this));
            _selectButton.onClick.RemoveListener(() => SelectButtonClicked?.Invoke(_skin, this));
        }

        public void Initialize(Skin skin)
        {
            _skin = skin;
            _icon.sprite = skin.Icon;
            _priceText.SetText(skin.Price);
            UpdateView();
        }

        public void UpdateView()
        {
            if (_skin.IsSelect)
            {
                _priceText.Hide();
                _selectButton.gameObject.SetActive(true);
                _selectButton.enabled = false;
                _selectText.text = ShopParameter.SelectedText;
            }
            else if (!_skin.IsBought && !_skin.IsSelect)
            {
                _selectButton.gameObject.SetActive(false);
                _priceText.Show();
            }
            else if(_skin.IsBought && !_skin.IsSelect)
            {
                _priceText.Hide();
                _buyButton.gameObject.SetActive(false);
                _selectButton.gameObject.SetActive(true);
                _selectText.text = ShopParameter.SelectText;
                _selectButton.enabled = true;
            }
        }
    }
}
