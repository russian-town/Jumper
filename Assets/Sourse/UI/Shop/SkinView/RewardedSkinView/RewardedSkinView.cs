using UnityEngine;

public class RewardedSkinView : SkinView
{
    [SerializeField] private RewardedButton _rewardedButton;

    private YandexAds _yandexAds;
    private ApplicationStatusChecker _applicationStatusChecker;

    public void SetApplicationStatusChecker(ApplicationStatusChecker applicationStatusChecker )
    {
        _applicationStatusChecker = applicationStatusChecker;
    }

    protected override void Subscribe()
    {
        _yandexAds = new YandexAds();
        _rewardedButton.ButtonClicked += OnButtonClicked;
        _yandexAds.RewardedCallback += OnRewardedCallback;
        _yandexAds.OpenCallback += OnOpenCallback;
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
        _yandexAds.OpenCallback -= OnOpenCallback;
    }

    private void OnButtonClicked()
    {
        _yandexAds.ShowRewardedVideo();
    }

    private void OnOpenCallback()
    {
        _applicationStatusChecker.OnInBackgroundChangeEvent(true);
    }

    private void OnRewardedCallback()
    {
        _applicationStatusChecker.OnInBackgroundChangeEvent(false);
        By();
    }
}
