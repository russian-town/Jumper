using Sourse.Game;
using Sourse.UI.LevelCompletePanel;
using Sourse.Yandex;

namespace Sourse.UI
{
    public class PlayerUI: IPauseHandler
    {
        private bool _isPause;
        private bool _isRewarded;

        private readonly NextLevelButton _nextLevelButton;
        private readonly RetryButton _retryButton;
        private readonly RewardedVideo _rewardedVideo;

        public PlayerUI(NextLevelButton nextLevelButton,
            RetryButton retryButton,
            RewardedVideo rewardedVideo)
        {
            _nextLevelButton = nextLevelButton;
            _retryButton = retryButton;
            _rewardedVideo = rewardedVideo;
        }

        public void Subscribe()
        {
            _rewardedVideo.RewardedVideoOpened += OnRewardedVideoOpened;
            _rewardedVideo.RewardedVideoEnded += OnRewardedVideoEnded;
        }

        public void Unsibscribe()
        {
            _rewardedVideo.RewardedVideoOpened -= OnRewardedVideoOpened;
            _rewardedVideo.RewardedVideoEnded -= OnRewardedVideoEnded;
        }

        public void SetPause(bool isPause)
        {
            _isPause = isPause;

            if (_isPause == true)
                _nextLevelButton.Hide();
            else
                _nextLevelButton.Show();

            if (_isPause == true)
                _retryButton.Hide();
            else if (_isPause == true && _isRewarded == true)
                _retryButton.Hide();
            else if (_isPause == false && _isRewarded == false)
                _retryButton.Show();
        }

        private void OnRewardedVideoOpened()
            => _isRewarded = true;

        private void OnRewardedVideoEnded()
            => _isRewarded = false;
    }
}
