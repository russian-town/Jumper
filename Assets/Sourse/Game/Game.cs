using UnityEngine;
using UnityEngine.EventSystems;
using Agava.YandexGames;

[RequireComponent(typeof(OpenableSkinHandler))]
public class Game : MonoBehaviour, IPauseHandler
{
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private LevelProgressView _levelProgressView;
    [SerializeField] private Menu _menu;
    [SerializeField] private Shop _shop;
    [SerializeField] private LevelCompletePanel _levelCompletePanel;
    [SerializeField] private float _percentOpeningSkin;
    [SerializeField] private Pause _pause;
    [SerializeField] private RetryButton _retryButton;
    [SerializeField] private RewardedPanel _rewardedPanel;

    private OpenableSkinHandler _openableSkinHandler;
    private Player _player;
    private LevelProgress _levelProgress;
    private bool _isPause;
    private PlayerPosition _playerStartPosition;
    private PlayerPosition _playerLastPosition;

    public bool IsStart { get; private set; }

    private void OnDisable()
    {
        if (_player == null)
            return;

        _player.Died -= OnPlayerDied;
    }

    private void Update()
    {
        if (IsStart == true)
            return;

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            StartGame();
    }

    public void Initialaize(Player player, LevelProgress levelProgress, PlayerPosition playerStartPosition)
    {
        _openableSkinHandler = GetComponent<OpenableSkinHandler>();
        _openableSkinHandler.Initialize(_shop, _levelCompletePanel);
        _player = player;
        player.Died += OnPlayerDied;
        player.LevelCompleted += OnLevelCompleted;
        _playerStartPosition = playerStartPosition;
        _levelProgress = levelProgress;
        _pause.Initialize(new IPauseHandler[] { player, this });
    }

    public void StartGame()
    {
        if (_isPause)
            return;

        IsStart = true;
        _menu.Hide();
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
        IsStart = false;
        float percent = Mathf.Ceil(_levelProgress.CurrentDistance * 100f);
        _gameOverView.Show();

#if !UNITY_EDITOR
        if (YandexGamesSdk.IsInitialized == false || _playerStartPosition == _playerLastPosition)
        {
            _rewardedPanel.Hide();
            _retryButton.Show();
        }
        else
        {
            _rewardedPanel.Show();
            _retryButton.Hide();
        }
#endif

        _rewardedPanel.Show();
        _retryButton.Hide();
        _gameOverView.ShowProgress(percent);
        _levelProgressView.Hide();
    }

    private void OnLevelCompleted()
    {
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
    }
}
