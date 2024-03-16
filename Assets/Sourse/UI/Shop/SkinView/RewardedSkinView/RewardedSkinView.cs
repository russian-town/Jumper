using Sourse.ApplicationStatusChecker;
using UnityEngine;

public class RewardedSkinView : SkinView
{
    [SerializeField] private RewardedButton _rewardedButton;

    private YandexAds _yandexAds;
    private ApplicationStatusChecker _applicationStatusChecker;

    public void SetApplicationStatusChecker(ApplicationStatusChecker applicationStatusChecker)
    {
        _applicationStatusChecker = applicationStatusChecker;
    }

    protected override void Initialize()
    {
        _yandexAds = new YandexAds();
        _rewardedButton.Initialize();
        _rewardedButton.ButtonClicked += OnButtonClicked;
        _yandexAds.RewardedCallback += OnRewardedCallback;
        _yandexAds.OpenCallback += OnOpenCallback;
        _yandexAds.CloseCallback += OnCloseCallback;
    }

    protected override void UpdateChildView()
    {
        if (Skin.IsBought == true)
        {
            _rewardedButton.Hide();
            SelectButton.Show();
        }
        else if (Skin.IsBought == false)
        {
            _rewardedButton.Show();
            SelectButton.Hide();
        }
    }

    protected override void Deinitialize()
    {
        _rewardedButton.ButtonClicked -= OnButtonClicked;
        _yandexAds.RewardedCallback -= OnRewardedCallback;
        _yandexAds.OpenCallback -= OnOpenCallback;
        _yandexAds.CloseCallback -= OnCloseCallback;
    }

    private void OnButtonClicked()
    {
        _yandexAds.ShowRewardedVideo();
    }

    private void OnOpenCallback()
    {
        _applicationStatusChecker.SetIsPlayRewarded(true);
        _applicationStatusChecker.ChangeSoundStatus(true);
    }

    private void OnRewardedCallback()
    {
        By();
    }

    private void OnCloseCallback()
    {
        _applicationStatusChecker.SetIsPlayRewarded(false);
        _applicationStatusChecker.ChangeSoundStatus(false);
    }
}
