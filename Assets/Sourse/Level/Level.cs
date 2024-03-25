using Sourse.YandexAds;
using UnityEngine;

namespace Sourse.Level
{
    [RequireComponent(typeof(PlayerSceneLoader.PlayerSceneLoader))]
    public class Level : MonoBehaviour
    {
        public const string FirstLevelNumber = "1";
        public const string SavedIndexKey = "SavedIndex";

        [SerializeField] private LevelNumberText _levelNumberText;
        [SerializeField] private RewardedVideo _rewardedVideo;

        private int _currentLevelNumber;
        private PlayerSceneLoader.PlayerSceneLoader _sceneLoader;
        public int CurrentLevelNumber => _currentLevelNumber;

        public void Unsubscribe()
        {
            _rewardedVideo.RewardedVideoEnded -= RestartLevelOnLastPosition;
        }

        public void Initialize(LevelProgress levelProgress)
        {
            _sceneLoader = GetComponent<PlayerSceneLoader.PlayerSceneLoader>();
            _rewardedVideo.RewardedVideoEnded += RestartLevelOnLastPosition;

            if (int.TryParse(_sceneLoader.CurrentSceneName, out int level))
                _currentLevelNumber = level;

            _levelNumberText.Initialize(_currentLevelNumber);
        }

        public void RestartLevel()
        {
            _sceneLoader.ReloadCurrentScene();
        }

        public void LoadNextLevel()
        {
            _currentLevelNumber++;
            _sceneLoader.LoadSceneByName(_currentLevelNumber);
        }

        private void RestartLevelOnLastPosition()
        {
            RestartLevel();
        }
    }
}
