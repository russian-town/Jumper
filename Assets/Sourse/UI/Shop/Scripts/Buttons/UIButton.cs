using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sourse.UI.Shop.Scripts.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class UIButton : UIElement
    {
        private Button _button;

        public void Initialize()
            => _button = GetComponent<Button>();

        public void AddListener(UnityAction action)
            => _button.onClick.AddListener(action);

        public void RemoveListener(UnityAction action)
            => _button.onClick.RemoveListener(action);
    }
}
