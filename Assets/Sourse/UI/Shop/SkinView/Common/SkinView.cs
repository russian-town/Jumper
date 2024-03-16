using System;
using Sourse.UI.Shop.Scripts.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.UI.Shop.SkinView.Common
{
    public abstract class SkinView : MonoBehaviour
    {
        [SerializeField] private SelectButton _selectButton;
        [SerializeField] private TMP_Text _selectText;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _selectIcon;
        [SerializeField] private Image _selectButtonImage;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Color _defaultColor;

        private Skin.Skin _skin;

        protected Skin.Skin Skin => _skin;
        protected SelectButton SelectButton => _selectButton;

        public event Action<Skin.Skin, SkinView> ByButtonClicked;
        public event Action<Skin.Skin, SkinView> SelectButtonClicked;
        public event Action<Skin.Skin, SkinView> Selected;

        private void OnDisable()
        {
            _selectButton.ButtonClicked -= OnButtonClicked;
            Deinitialize();
        }

        public void Initialize(Skin.Skin skin)
        {
            _selectButton.Initialize();
            _selectButton.ButtonClicked += OnButtonClicked;
            _icon.sprite = skin.Icon;
            _skin = skin;
            UpdateView();
            Initialize();
            UpdateChildView();

            if (_skin.IsBought && _skin.IsSelect)
                Selected?.Invoke(_skin, this);
        }

        public void UpdateView()
        {
            UpdateChildView();

            if (_skin.IsBought && _skin.IsSelect)
                SwitchViewState(false, true, _selectColor);
            else if (_skin.IsBought && !_skin.IsSelect)
                SwitchViewState(true, false, _defaultColor);
        }

        protected void By() => ByButtonClicked?.Invoke(_skin, this);

        protected abstract void Initialize();

        protected abstract void UpdateChildView();

        protected abstract void Deinitialize();

        protected void SetIconColor(Color color) => _icon.color = color;

        private void OnButtonClicked() => SelectButtonClicked?.Invoke(_skin, this);

        private void SwitchViewState(bool enableText, bool enableIcon, Color buttonColor)
        {
            _selectText.enabled = enableText;
            _selectIcon.enabled = enableIcon;
            _selectButtonImage.color = buttonColor;
        }
    }
}
