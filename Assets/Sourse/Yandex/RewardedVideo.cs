using System;
using UnityEngine;

namespace Sourse.Yandex
{
    public class RewardedVideo : MonoBehaviour
    {
        [SerializeField] private ApplicationStatusChecker.ApplicationStatusChecker _applicationStatusChecker;

        private YandexAds _yandexAds;

        public event Action RewardedVideoEnded;
        public event Action RewardedVideoOpened;

        private void Awake() => _yandexAds = new YandexAds();

        private void OnEnable()
        {
            _yandexAds.OpenCallback += OnOpenCallBack;
            _yandexAds.RewardedCallback += OnRewardedCallback;
            _yandexAds.CloseCallback += OnCloseCallback;
            _yandexAds.ErrorCallback += OnErrorCallback;
        }

        private void OnDisable()
        {
            _yandexAds.OpenCallback -= OnOpenCallBack;
            _yandexAds.RewardedCallback -= OnRewardedCallback;
            _yandexAds.CloseCallback -= OnCloseCallback;
            _yandexAds.ErrorCallback -= OnErrorCallback;
        }

        public void Show() => _yandexAds.ShowRewardedVideo();

        private void OnOpenCallBack()
        {
            _applicationStatusChecker.SetIsPlayRewarded(true);
            _applicationStatusChecker.ChangeSoundStatus(true);
            RewardedVideoOpened?.Invoke();
        }

        private void OnRewardedCallback() => RewardedVideoEnded?.Invoke();

        private void OnCloseCallback()
        {
            _applicationStatusChecker.SetIsPlayRewarded(false);
            _applicationStatusChecker.ChangeSoundStatus(false);
        }

        private void OnErrorCallback(string errorText) => _applicationStatusChecker.OnInBackgroundChangeEvent(false);
    }
}