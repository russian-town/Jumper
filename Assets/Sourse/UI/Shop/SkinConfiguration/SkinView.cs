using System;
using Sourse.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public abstract class SkinView : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private TMP_Text _selectText;

        public event Action<Skin> SelectButtonClicked;

        public virtual void UpdateView()
        {
            if (Skin().IsSelect)
            {
                _selectButton.gameObject.SetActive(true);
                _selectButton.enabled = false;
                _selectText.text = ShopParameter.SelectedText;
            }
            else if (!Skin().IsBought && !Skin().IsSelect)
            {
                _selectButton.gameObject.SetActive(false);
            }
            else if (Skin().IsBought && !Skin().IsSelect)
            {
                _selectButton.gameObject.SetActive(true);
                _selectText.text = ShopParameter.SelectText;
                _selectButton.enabled = true;
            }
        }

        public virtual void Subscribe()
        {
            _selectButton.onClick.AddListener(()
                => SelectButtonClicked?.Invoke(Skin()));
            Skin().Bought += OnBought;
            Skin().Selected += OnSelected;
            Skin().Deselected += OnDeselected;
        }

        public virtual void Unsubscribe()
        {
            _selectButton.onClick.RemoveListener(()
                => SelectButtonClicked?.Invoke(Skin()));
            Skin().Bought -= OnBought;
            Skin().Selected -= OnSelected;
            Skin().Deselected -= OnDeselected;
        }

        protected abstract Skin Skin();

        private void OnBought()
            => UpdateView();

        private void OnSelected()
            => UpdateView();

        private void OnDeselected()
            => UpdateView();
    }
}
