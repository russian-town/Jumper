using Sourse.Game;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.UI.Shop.Scripts.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class UIButton : UIElement, IPauseHandler
    {
        private Button _button;

        public event Action ButtonClicked;

        public void Unsubscribe()
            => _button.onClick.RemoveListener(OnButtonClick);

        public void Initialize()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClick);
        }

        public void SetPause(bool isPause)
        {
            if (isPause)
                Disable();
            else
                Enable();
        }

        protected void Enable()
        {
            _button.enabled = true;
            gameObject.SetActive(true);
        }

        protected void Disable() 
        {
            _button.enabled = false;
            gameObject.SetActive(false);
        }

        private void OnButtonClick()
            => ButtonClicked?.Invoke();
    }
}
