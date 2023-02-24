using UnityEngine;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour
{
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private LevelProgressView _levelProgressView;
    [SerializeField] private Menu _menu;

    private Player _player;
    private LevelProgress _levelProgress;
    private bool _gameStarted;

    private void OnDisable()
    {
        if (_player == null)
            return;

        _player.Died -= OnPlayerDied;
    }

    private void Update()
    {
        if (_gameStarted == true)
            return;

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            StartGame();
    }

    public void Initialaize(Player player, LevelProgress levelProgress)
    {
        _player = player;
        player.Died += OnPlayerDied;
        _levelProgress = levelProgress;
    }

    private void OnPlayerDied()
    {
        float percent = Mathf.Ceil(_levelProgress.CurrentDistance * 100f);
        _gameOverView.ShowPanel(percent);
        _levelProgressView.Hide();
    }

    private void StartGame()
    {
        _gameStarted = true;
        _menu.Hide();
        _levelProgressView.Show();
    }
}
