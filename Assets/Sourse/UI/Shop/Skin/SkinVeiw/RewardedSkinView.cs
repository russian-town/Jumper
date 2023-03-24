using UnityEngine;

public class RewardedSkinView : SkinView
{
    [SerializeField] private RewardedButton _rewardedButton;

    private YandexAds _yandexAds;

    private void Start()
    {
        if (Skin.IsBy == true)
        {
            _rewardedButton.Hide();
            SelectButton.Show();
        }
    }

    private void OnEnable()
    {
        _yandexAds = new YandexAds();
        _rewardedButton.ButtonClicked += OnButtonClicked;
        _yandexAds.RewardedCallback += OnRewardedCallback;
    }

    private void OnDisable()
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
        base.By();
        Shop.TryBySkin(Skin.ID);
    }
}
