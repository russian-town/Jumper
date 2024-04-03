using System;
using Sourse.Game;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sourse.UI.Shop.Scripts.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class UIButton : UIElement, IPauseHandler
    {
        private Button _button;

        public void Initialize()
            => _button = GetComponent<Button>();

        public void SetPause(bool isPause)
        {
            if (isPause)
                Disable();
            else
                Enable();
        }

        public void AddListener(UnityAction action)
            => _button.onClick.AddListener(action);

        public void RemoveListener(UnityAction action)
            => _button.onClick.RemoveListener(action);

        protected void Enable()
            => _button.enabled = true;

        protected void Disable()
            => _button.enabled = false;
    }
}
