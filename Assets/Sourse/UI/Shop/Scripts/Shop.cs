using System;
using System.Collections.Generic;
using Sourse.Balance;
using Sourse.UI.Shop.SkinConfiguration;
using Sourse.Yandex;

namespace Sourse.UI.Shop.Scripts
{
    public class Shop
    {
        private Wallet _wallet;
        private List<Skin> _skins = new List<Skin>();
        private List<PaidSkinView> _paidSkinViews = new();
        private List<OpenableSkinView> _openableSkinViews = new();
        private List<RewardedSkinView> _rewardedSkinViews = new();
        private Skin _currentSelectedSkin;
        private YandexAds _yandexAds;
        private SkinRewarded _skinRewarded;

        public event Action<Skin> Bought;
        public event Action<Skin> Selected;

        public void Initialize(List<Skin> skins,
            List<PaidSkinView> paidSkinViews,
            List<OpenableSkinView> openableSkinViews,
            List<RewardedSkinView> rewardedSkinViews,
            Wallet wallet)
        {
            _skins = skins;
            _paidSkinViews = paidSkinViews;
            _openableSkinViews = openableSkinViews;
            _rewardedSkinViews = rewardedSkinViews;
            _wallet = wallet;
            SetDefaultSkin();
            SetSelectedSkin();

            foreach (var paidSkinView in _paidSkinViews)
                paidSkinView.UpdateView();

            foreach (var openableSkinView in _openableSkinViews)
                openableSkinView.UpdateView();

            foreach (var rewardedSkinView in _rewardedSkinViews)
                rewardedSkinView.UpdateView();
        }

        public void Subscribe()
        {
            foreach (var paidSkinView in _paidSkinViews)
            {
                paidSkinView.BuyButtonClicked += OnBuyButtonClicked;
                paidSkinView.SelectButtonClicked += OnSelectButtonClicked;
                paidSkinView.Subscribe();
            }

            foreach (var openableSkinView in _openableSkinViews)
            {
                openableSkinView.SelectButtonClicked += OnSelectButtonClicked;
                openableSkinView.Subscribe();
            }

            foreach (var rewardedSkinView in _rewardedSkinViews)
            {
                rewardedSkinView.RewardedButtonClicked += OnRewardedButtonClicked;
                rewardedSkinView.SelectButtonClicked += OnSelectButtonClicked;
                rewardedSkinView.Subscribe();
            }
        }

        public void Unsubscribe()
        {
            foreach (var paidSkinView in _paidSkinViews)
            {
                paidSkinView.BuyButtonClicked -= OnBuyButtonClicked;
                paidSkinView.SelectButtonClicked -= OnSelectButtonClicked;
                paidSkinView.Unsubscribe();
            }

            foreach (var openableSkinView in _openableSkinViews)
            {
                openableSkinView.SelectButtonClicked -= OnSelectButtonClicked;
                openableSkinView.Unsubscribe();
            }

            foreach (var rewardedSkinView in _rewardedSkinViews)
            {
                rewardedSkinView.RewardedButtonClicked -= OnRewardedButtonClicked;
                rewardedSkinView.SelectButtonClicked -= OnSelectButtonClicked;
                rewardedSkinView.Unsubscribe();
            }
        }

        private void SetDefaultSkin()
            => _skins[0].Buy();

        private void SetSelectedSkin()
        {
            foreach (var skin in _skins)
            {
                if (skin.IsSelect == true)
                {
                    _currentSelectedSkin = skin;
                    return;
                }
            }

            _skins[0].Select();
            _currentSelectedSkin = _skins[0];
        }

        private void OnBuyButtonClicked(Skin skin)
        {
            if (skin.IsBought == true)
                return;

            if (skin.Price > _wallet.Money)
                return;

            _wallet.DicreaseMoney(skin.Price);
            skin.Buy();
            Bought?.Invoke(skin);
        }

        private void OnSelectButtonClicked(Skin skin)
        {
            if (skin.IsBought == false || skin.IsSelect == true)
                return;

            _currentSelectedSkin.Deselect();
            skin.Select();
            _currentSelectedSkin = skin;
            Selected?.Invoke(skin);
        }

        private void OnRewardedButtonClicked(Skin skin)
        {
            _skinRewarded = new SkinRewarded(skin);
            _yandexAds.ShowRewardedVideo();
            _yandexAds.RewardedCallback += OnRewardedCallback;
        }

        private void OnRewardedCallback()
        {
            _yandexAds.RewardedCallback -= OnRewardedCallback;
            _skinRewarded.Accept();
            Bought?.Invoke(_skinRewarded.Skin);
        }
    }
}
