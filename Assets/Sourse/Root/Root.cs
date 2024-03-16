using Sourse.ApplicationStatusChecker;
using UnityEngine;

[RequireComponent(typeof(PlayerSpawner), typeof(Saver))]
public class Root : MonoBehaviour
{
    [SerializeField] private FollowCamera _followCamera;
    [SerializeField] private Game _game;
    [SerializeField] private OpenableSkinHandler _openableSkinHandler;
    [SerializeField] private LevelProgress _levelProgress;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Level _level;
    [SerializeField] private ApplicationStatusChecker _applicationStatusChecker;
    [SerializeField] private PlayerPositionHandler _playerPositionHandler;
    [SerializeField] private PlayerPosition _startPlayerPosition;

    private Vector3 _targetRotation = new Vector3(0f, 90f, 0f);
    private PlayerSpawner _playerSpawner;
    private Saver _saver;
    private Player _startPlayer;

    private void Awake()
    {
        _playerSpawner = GetComponent<PlayerSpawner>();
        _saver = GetComponent<Saver>();
    }

    private void Start()
    {
        _level.Initialize(_levelProgress, _playerPositionHandler);
        _playerPositionHandler.Initialize();
        PlayerPosition startPositon = _playerPositionHandler.GetLastPosition();

        if (startPositon == null)
            startPositon = _startPlayerPosition;

        _playerPositionHandler.RemoveCurrentPropsID();
        Initialize(_saver.GetSelectedID(), startPositon);
    }

    private void Initialize(int id, PlayerPosition playerPosition)
    {
        _startPlayer = _playerSpawner.GetPlayer(id, playerPosition);
        _startPlayer.transform.localRotation = Quaternion.Euler(_targetRotation);
        _startPlayer.Initialize(_playerPositionHandler);
        _playerInput.Initialize(_startPlayer);
        _game.Initialaize(_startPlayer, _levelProgress, _playerInput, _startPlayerPosition, _applicationStatusChecker);
        _levelProgress.Initialize(_startPlayer);
        _followCamera.SetTarget(_startPlayer);
    }
}
