using System;
using Agava.YandexGames;

public class YandexAds
{
    public event Action OpenCallback;
    public event Action RewardedCallback;
    public event Action CloseCallback;
    public event Action<string> ErrorCallback;
    public event Action OpenInterstitialCallback;
    public event Action<bool> CloseInterstitialCallback;
    public event Action<string> ErrorInterstitialCallback;
    public event Action OfflineCallback;

    public void ShowRewardedVideo()
    {
        if (YandexGamesSdk.IsInitialized == true)
            VideoAd.Show(OpenCallback, RewardedCallback, CloseCallback, ErrorCallback);
    }

    public void ShowInterstitial()
    {
        if (YandexGamesSdk.IsInitialized == true)
            InterstitialAd.Show(OpenInterstitialCallback, CloseInterstitialCallback, ErrorInterstitialCallback, OfflineCallback);
    }
}
