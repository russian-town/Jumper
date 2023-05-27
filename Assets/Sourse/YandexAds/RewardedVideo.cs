using UnityEngine;
using UnityEngine.Events;

public class RewardedVideo : MonoBehaviour
{
    public event UnityAction RewardedVideoEnded;
    public event UnityAction RewardedVideoOpened;

    [SerializeField] private ApplicationStatusChecker _applicationStatusChecker;

    private YandexAds _yandexAds;

    private void Awake()
    {
        _yandexAds = new YandexAds();
    }

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

    public void Show()
    {
        _yandexAds.ShowRewardedVideo();
    }

    private void OnOpenCallBack()
    {
        _applicationStatusChecker.OnInBackgroundChangeEvent(true);
        RewardedVideoOpened?.Invoke();
    }

    private void OnRewardedCallback()
    {
        RewardedVideoEnded?.Invoke();
    }

    private void OnCloseCallback()
    {
        _applicationStatusChecker.OnInBackgroundChangeEvent(false);
    }

    private void OnErrorCallback(string errorText)
    {
        _applicationStatusChecker.OnInBackgroundChangeEvent(false);
    }
}
