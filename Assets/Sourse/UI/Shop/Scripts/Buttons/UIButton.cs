using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.UI.Shop.Scripts.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class UIButton : UIElement
    {
        private Button _button;
        private bool _isInitialized;

        public event Action ButtonClicked;

        private void OnDisable()
        {
            if (_isInitialized == false)
                return;

            _button.onClick.RemoveListener(OnButtonClick);
        }

        public void Initialize()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClick);
            _isInitialized = true;
        }

        protected void SwitchEnableState(bool isEnable) => _button.enabled = isEnable;

        private void OnButtonClick() => ButtonClicked?.Invoke();
    }
}
