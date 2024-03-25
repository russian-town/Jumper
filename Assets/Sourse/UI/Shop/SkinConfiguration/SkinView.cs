using System;
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

        public event Action BuyButtonClicked;
        public event Action SelectButtonClicked;

        public void Subscribe()
        {
            _buyButton.onClick.AddListener(() => BuyButtonClicked?.Invoke());
            _selectButton.onClick.AddListener(() => SelectButtonClicked?.Invoke());
        }

        public void Unsubscribe()
        {
            _buyButton.onClick.RemoveListener(() => BuyButtonClicked?.Invoke());
            _selectButton.onClick.RemoveListener(() => SelectButtonClicked?.Invoke());
        }

        public void Initialize(Skin skin)
        {
            _icon.sprite = skin.Icon;
            _priceText.SetText(skin.Price);
            UpdateView(skin);
        }

        public void UpdateView(Skin skin)
        {
            if (skin.IsSelect)
            {
                _priceText.Hide();
                _selectButton.gameObject.SetActive(true);
                _selectButton.enabled = false;
                _selectText.text = "Selected";
            }
            else if (!skin.IsBought && !skin.IsSelect)
            {
                _selectButton.gameObject.SetActive(false);
                _priceText.Show();
            }
            else if(skin.IsBought && !skin.IsSelect)
            {
                _priceText.Hide();
                _buyButton.gameObject.SetActive(false);
                _selectButton.gameObject.SetActive(true);
                _selectText.text = "Select";
                _selectButton.enabled = true;
            }
        }
    }
}
