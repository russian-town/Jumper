using System;
using Agava.YandexGames;

public class YandexAds
{
    public Action OpenCallback;
    public Action RewardedCallback;
    public Action CloseCallback;
    public Action<string> ErrorCallback;
    public Action OpenInterstitialCallback;
    public Action<bool> CloseInterstitialCallback;
    public Action<string> ErrorInterstitialCallback;
    public Action OfflineCallback;

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
