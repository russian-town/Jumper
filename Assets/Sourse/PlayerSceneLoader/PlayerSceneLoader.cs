using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sourse.PlayerSceneLoader
{
    [RequireComponent(typeof(Saver.Saver))]
    public class PlayerSceneLoader : MonoBehaviour
    {
        private const string StartSceneName = "StartScene";
        private const string SettingsSceneName = "SettingsScene";
        private const string ShopSceneName = "Shop";
        private const int FirstLevel = 1;

        private Saver.Saver _saver;

        public string CurrentSceneName => SceneManager.GetActiveScene().name;

        private void Awake()
        {
            _saver = GetComponent<Saver.Saver>();
        }

        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadSceneByName(int levelNumber)
        {
            if (SceneUtility.GetBuildIndexByScenePath(levelNumber.ToString()) < 0)
                SceneManager.LoadScene(Level.Level.FirstLevelNumber);
            else
                SceneManager.LoadScene(levelNumber.ToString());
        }

        public void LoadLastScene()
        {
            if (_saver.TryGetValue(Level.Level.SavedIndexKey, out int value))
                LoadSceneByName(value);
            else
                LoadSceneByName(FirstLevel);
        }

        public void LoadStartScene()
        {
            SceneManager.LoadScene(StartSceneName);
        }

        public void LoadShopScene()
        {
            SceneManager.LoadScene(ShopSceneName);
        }

        public void LoadSettingsScene()
        {
            SceneManager.LoadScene(SettingsSceneName);
        }
    }
}
