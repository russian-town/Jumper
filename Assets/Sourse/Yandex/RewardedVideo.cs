using System;

namespace Sourse.Yandex
{
    public class RewardedVideo
    {
        private ApplicationStatusChecker.ApplicationStatusChecker _applicationStatusChecker;
        private YandexAds _yandexAds = new YandexAds();

        public event Action RewardedVideoEnded;
        public event Action RewardedVideoOpened;

        public void Subscribe()
        {
            _yandexAds.OpenCallback += OnOpenCallBack;
            _yandexAds.RewardedCallback += OnRewardedCallback;
            _yandexAds.CloseCallback += OnCloseCallback;
            _yandexAds.ErrorCallback += OnErrorCallback;
        }

        public void Unsubscribe()
        {
            _yandexAds.OpenCallback -= OnOpenCallBack;
            _yandexAds.RewardedCallback -= OnRewardedCallback;
            _yandexAds.CloseCallback -= OnCloseCallback;
            _yandexAds.ErrorCallback -= OnErrorCallback;
        }

        public void Show()
            => _yandexAds.ShowRewardedVideo();

        private void OnOpenCallBack()
        {
            _applicationStatusChecker.SetIsPlayRewarded(true);
            _applicationStatusChecker.ChangeSoundStatus(true);
            RewardedVideoOpened?.Invoke();
        }

        private void OnRewardedCallback()
            => RewardedVideoEnded?.Invoke();

        private void OnCloseCallback()
        {
            _applicationStatusChecker.SetIsPlayRewarded(false);
            _applicationStatusChecker.ChangeSoundStatus(false);
        }

        private void OnErrorCallback(string errorText)
            => _applicationStatusChecker.OnInBackgroundChangeEvent(false);
    }
}
