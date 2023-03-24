using UnityEngine;
using UnityEngine.Events;

public class RewardedVideo : MonoBehaviour
{
    public event UnityAction RewardedVideoEnded;

    private YandexAds _yandexAds;

    private void Awake()
    {
        _yandexAds = new YandexAds();
    }

    private void OnEnable()
    {
        _yandexAds.RewardedCallback += OnRewardedCallback;
        _yandexAds.CloseCallback += OnCloseCallback;
        _yandexAds.ErrorCallback += OnErrorCallback;
    }

    private void OnDisable()
    {
        _yandexAds.RewardedCallback -= OnRewardedCallback;
        _yandexAds.CloseCallback -= OnCloseCallback;
        _yandexAds.ErrorCallback -= OnErrorCallback;
    }

    public void Show()
    {
        _yandexAds.ShowRewardedVideo();
    }

    private void OnRewardedCallback()
    {
        RewardedVideoEnded?.Invoke();
    }

    private void OnCloseCallback()
    {
    }

    private void OnErrorCallback(string errorText)
    {
    }
}
