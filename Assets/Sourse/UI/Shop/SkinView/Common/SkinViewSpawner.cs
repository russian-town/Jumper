using UnityEngine;

public class SkinViewSpawner : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private DefaultSkinView _defaultSkinView;
    [SerializeField] private PaidSkinView _paidSkinView;
    [SerializeField] private RewardedSkinView _rewardedSkinView;
    [SerializeField] private OpenableSkinView _openableSkinView;
    [SerializeField] private ApplicationStatusChecker _applicationStatusChecker;

    public SkinView DefaultSkin { get; private set; }

    public SkinView GetSkinView(Skin skin)
    {
        switch (skin.Type)
        {
            case SkinType.Default:
                return DefaultSkin = Instantiate(_defaultSkinView, _content);
            case SkinType.Paid:
                return Instantiate(_paidSkinView, _content);
            case SkinType.Rewarded:
                RewardedSkinView rewardedSkinView = Instantiate(_rewardedSkinView, _content);
                rewardedSkinView.SetApplicationStatusChecker(_applicationStatusChecker);
                return rewardedSkinView;
            case SkinType.Openable:
                return Instantiate(_openableSkinView, _content);
        }

        return null;
    }
}
