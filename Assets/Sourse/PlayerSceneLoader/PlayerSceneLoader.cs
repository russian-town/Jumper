using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Saver))]
public class PlayerSceneLoader : MonoBehaviour
{
    private const string StartSceneName = "StartScene";
    private const string SettingsSceneName = "SettingsScene";
    private const string ShopSceneName = "Shop";
    private const int FirstLevel = 1;

    private Saver _saver;

    public string CurrentSceneName => SceneManager.GetActiveScene().name;

    private void Awake()
    {
        _saver = GetComponent<Saver>();
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadSceneByName(int levelNumber)
    {
        if (SceneUtility.GetBuildIndexByScenePath(levelNumber.ToString()) < 0)
            SceneManager.LoadScene(Level.FirstLevelNumber);
        else
            SceneManager.LoadScene(levelNumber.ToString());
    }

    public void LoadLastScene()
    {
        if (_saver.TryGetValue(Level.SavedIndexKey, out int value))
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
