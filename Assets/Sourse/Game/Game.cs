using UnityEngine;

[RequireComponent(typeof(OpenableSkinHandler))]
public class Game : MonoBehaviour, IPauseHandler
{
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private LevelProgressView _levelProgressView;
    [SerializeField] private Level _level;
    [SerializeField] private LevelCompletePanel _levelCompletePanel;
    [SerializeField] private float _percentOpeningSkin;
    [SerializeField] private Pause _pause;
    [SerializeField] private RetryButton _retryButton;
    [SerializeField] private RewardedPanel _rewardedPanel;

    private OpenableSkinHandler _openableSkinHandler;
    private Player _player;
    private LevelProgress _levelProgress;
    private PlayerInput _playerInput;
    private bool _isPause;
    private PlayerPosition _playerLastPosition;
    private PlayerPosition _playerStartPosition;

    private bool _isStart;

    private void OnDisable()
    {
        if (_player == null)
            return;

        Deinitialize();
    }

    public void Initialaize(Player player, LevelProgress levelProgress, PlayerInput playerInput, PlayerPosition playerStartPosition)
    {
        _openableSkinHandler = GetComponent<OpenableSkinHandler>();
        _openableSkinHandler.Initialize(_levelCompletePanel);
        _player = player;
        _playerInput = playerInput;
        _playerStartPosition = playerStartPosition;
        player.Died += OnPlayerDied;
        player.LevelCompleted += OnLevelCompleted;
        _playerInput.Tap += StartGame;
        _levelProgress = levelProgress;
        _pause.Initialize(new IPauseHandler[] { player, this });
    }

    public void Deinitialize()
    {
        _isStart = false;
        _player.Died -= OnPlayerDied;
        _playerInput.Tap -= StartGame;
        _gameOverView.Hide();
    }

    public void StartGame()
    {
        if (_isStart == true || _isPause == true)
            return;

        _isStart = true;
        _player.SetStart(_isStart);
        _levelProgressView.Show();
        _gameOverView.Hide();
    }

    public void SetPause(bool isPause)
    {
        _isPause = isPause;
    }

    public void SetLastPosition(PlayerPosition playerLastPosition)
    {
        _playerLastPosition = playerLastPosition;
    }

    private void OnPlayerDied()
    {
        _isStart = false;
        _levelProgress.DeleteSavedDistance();
        float percent = Mathf.Ceil(_levelProgress.CurrentDistance * 100f);
        _gameOverView.Show();

        if (_playerLastPosition == null || _playerLastPosition == _playerStartPosition)
        {
            _rewardedPanel.Hide();
            _retryButton.Show();
        }
        else
        {
            _rewardedPanel.Show();
            _retryButton.Hide();
        }

        _gameOverView.ShowProgress(percent);
        _levelProgressView.Hide();
    }

    private void OnLevelCompleted()
    {
        _isStart = false;
        _levelProgress.DeleteSavedDistance();
        _pause.Enable();
        _levelProgressView.Hide();
        _levelCompletePanel.Show();

        if (_openableSkinHandler.GetOpenableSkin() != null)
        {
            _levelCompletePanel.Initialize(_openableSkinHandler.GetOpenableSkin());
            _levelCompletePanel.StartFillSkinBar(_percentOpeningSkin);
        }
        else
        {
            _levelCompletePanel.HideOpeningSkinBar();
        }

        _levelCompletePanel.SetText(_level.CurrentLevelNumber);
    }
}
