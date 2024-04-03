using Sourse.UI;
using Sourse.UI.Shop.Scripts.Buttons;
using System;
using UnityEngine;

namespace Sourse.Game
{
    public class RewardedPanel : UIElement
    {
        [SerializeField] private RewardedButton _rewardedButton;
        [SerializeField] private CloseAdOfferScreenButton _closeAdOfferScreenButton;

        public event Action RewardedButtonClicked;
        public event Action CloseAdOfferScreenButtonClicked;

        public void Subscribe()
        {
            _rewardedButton.AddListener(()
                => RewardedButtonClicked?.Invoke());
            _closeAdOfferScreenButton.AddListener(()
                => CloseAdOfferScreenButtonClicked?.Invoke());
        }

        public void Unsubscribe()
        {
            _rewardedButton.RemoveListener(()
                => RewardedButtonClicked?.Invoke());
            _closeAdOfferScreenButton.RemoveListener(()
                => CloseAdOfferScreenButtonClicked?.Invoke());
        }

        public void Initialize()
        {
            _rewardedButton.Initialize();
            _closeAdOfferScreenButton.Initialize();
        }
    }
}
