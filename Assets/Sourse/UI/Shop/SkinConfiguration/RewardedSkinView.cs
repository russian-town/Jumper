using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public class RewardedSkinView : SkinView
    {
        [SerializeField] private Button _rewardedButton;
        [SerializeField] private Image _icon;

        private Skin _skin;

        public event Action<Skin> RewardedButtonClicked;

        public override void Subscribe()
        {
            base.Subscribe();
            _rewardedButton.onClick.AddListener(()
                => RewardedButtonClicked?.Invoke(_skin));
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            _rewardedButton.onClick.RemoveListener(()
                => RewardedButtonClicked?.Invoke(_skin));
        }

        public void Initialize(Skin skin)
        {
            _skin = skin;
            _icon.sprite = skin.Icon;
            UpdateView();
        }

        public override void UpdateView()
        {
            base.UpdateView();

            if (!_skin.IsBought && !_skin.IsSelect)
            {
                _rewardedButton.gameObject.SetActive(true);
            }
            else if (_skin.IsBought && !_skin.IsSelect)
            {
                _rewardedButton.gameObject.SetActive(false);
            }
        }

        protected override Skin Skin()
        {
            return _skin;
        }
    }
}
