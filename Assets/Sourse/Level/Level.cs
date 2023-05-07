using UnityEngine;

[RequireComponent(typeof(Saver), typeof(PlayerSceneLoader))]
public class Level : MonoBehaviour
{
    public const string FirstLevelNumber = "1";
    public const string SavedIndexKey = "SavedIndex";

    [SerializeField] private int _currentLevelNumber;
    [SerializeField] private LevelNumberText _levelNumberText;

    private Saver _saver;
    private PlayerSceneLoader _sceneLoader;

    public int CurrentLevelNumber => _currentLevelNumber;

    public void Initialize()
    {
        _saver = GetComponent<Saver>();
        _sceneLoader = GetComponent<PlayerSceneLoader>();
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
