using Sourse.UI.Shop.Skin;
using UnityEngine;

namespace Sourse.UI.Shop.SkinView.Common
{
    public class SkinViewSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private DefaultSkinView.DefaultSkinView _defaultSkinView;
        [SerializeField] private PaidSkinView.PaidSkinView _paidSkinView;
        [SerializeField] private RewardedSkinView.RewardedSkinView _rewardedSkinView;
        [SerializeField] private OpenableSkinView.OpenableSkinView _openableSkinView;
        [SerializeField] private ApplicationStatusChecker.ApplicationStatusChecker _applicationStatusChecker;

        public SkinView DefaultSkin { get; private set; }

        public SkinView GetSkinView(Skin.Skin skin)
        {
            switch (skin.Type)
            {
                case SkinType.Default:
                    return DefaultSkin = Instantiate(_defaultSkinView, _content);
                case SkinType.Paid:
                    return Instantiate(_paidSkinView, _content);
                case SkinType.Rewarded:
                    RewardedSkinView.RewardedSkinView rewardedSkinView = Instantiate(_rewardedSkinView, _content);
                    rewardedSkinView.SetApplicationStatusChecker(_applicationStatusChecker);
                    return rewardedSkinView;
                case SkinType.Openable:
                    return Instantiate(_openableSkinView, _content);
            }

            return null;
        }
    }
}
