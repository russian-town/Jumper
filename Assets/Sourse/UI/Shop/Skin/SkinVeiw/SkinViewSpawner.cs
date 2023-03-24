using UnityEngine;

public class SkinViewSpawner : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private DefaultSkinView _defaultSkinView;
    [SerializeField] private PaidSkinView _paidSkinView;
    [SerializeField] private RewardedSkinView _rewardedSkinView;
    [SerializeField] private OpenableSkinView _openingSkinView;
    [SerializeField] private YandexAds _yandexAds;

    public SkinView GetSkinView(Skin skin)
    {
        switch (skin.Type)
        {
            case SkinType.Default:
                return Instantiate(_defaultSkinView, _content);
            case SkinType.Paid:
                return Instantiate(_paidSkinView, _content);
            case SkinType.Rewarded:
                return Instantiate(_rewardedSkinView, _content);
            case SkinType.Openable:
                return Instantiate(_openingSkinView, _content);
        }

        return null;
    }
}
