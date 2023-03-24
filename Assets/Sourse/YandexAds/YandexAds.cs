using Agava.YandexGames;
using System;

public class YandexAds
{
    public Action OpenCallback;
    public Action RewardedCallback;
    public Action CloseCallback;
    public Action<string> ErrorCallback;

    public void ShowRewardedVideo()
    {
        if (YandexGamesSdk.IsInitialized == true)
            VideoAd.Show(OpenCallback, RewardedCallback, CloseCallback, ErrorCallback);
    }

    public void ShowInterstitial()
    {
        if (YandexGamesSdk.IsInitialized == true)
            InterstitialAd.Show();
    }
}
