using UnityEngine;

[RequireComponent(typeof(Saver), typeof(PlayerSceneLoader))]
public class Level : MonoBehaviour
{
    public const string FirstLevelNumber = "1";
    public const string SavedIndexKey = "SavedIndex";

    [SerializeField] private LevelNumberText _levelNumberText;

    private int _currentLevelNumber;
    private Saver _saver;
    private PlayerSceneLoader _sceneLoader;

    public int CurrentLevelNumber => _currentLevelNumber;

    public void Initialize()
    {
        _saver = GetComponent<Saver>();
        _sceneLoader = GetComponent<PlayerSceneLoader>();

        if (int.TryParse(_sceneLoader.CurrentSceneName, out int level))
            _currentLevelNumber = level;

        _saver.Save(SavedIndexKey, _currentLevelNumber);
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
}
