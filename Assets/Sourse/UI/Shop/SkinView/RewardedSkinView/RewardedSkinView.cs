using UnityEngine;

public class RewardedSkinView : SkinView
{
    [SerializeField] private RewardedButton _rewardedButton;

    private YandexAds _yandexAds;

    protected override void Subscribe()
    {
        _yandexAds = new YandexAds();
        _rewardedButton.ButtonClicked += OnButtonClicked;
        _yandexAds.RewardedCallback += OnRewardedCallback;
    }

    protected override void UpdateChildView()
    {
        if (Skin.IsBy == true)
        {
            _rewardedButton.Hide();
            SelectButton.Show();
        }
        else if(Skin.IsBy == false)
        {
            _rewardedButton.Show();
            SelectButton.Hide();
        }
    }

    protected override void Deinitialize()
    {
        _rewardedButton.ButtonClicked -= OnButtonClicked;
        _yandexAds.RewardedCallback -= OnRewardedCallback;
    }

    private void OnButtonClicked()
    {
        _yandexAds.ShowRewardedVideo();
    }

    private void OnRewardedCallback()
    {
        By();
    }
}
