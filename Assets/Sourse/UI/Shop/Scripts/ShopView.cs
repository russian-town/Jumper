using System;
using Sourse.UI.Shop.Scripts.Buttons;
using UnityEngine;

namespace Sourse.UI.Shop.Scripts
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private CloseButton _closeButton;

        public event Action CloseButtonClicked;

        public void Initialize()
            => _closeButton.Initialize();

        public void Subscribe()
            => _closeButton.AddListener(() => CloseButtonClicked?.Invoke());

        public void Unsubscribe()
            => _closeButton.RemoveListener(() => CloseButtonClicked?.Invoke());
    }
}
