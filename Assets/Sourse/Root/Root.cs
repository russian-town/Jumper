using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSpawner), typeof(Saver))]
public class Root : MonoBehaviour
{
    [SerializeField] private FollowCamera _followCamera;
    [SerializeField] private List<Props> _props = new List<Props>();
    [SerializeField] private PlayerPosition _playerStartPosition;
    [SerializeField] private Game _game;
    [SerializeField] private OpenableSkinHandler _openableSkinHandler;
    [SerializeField] private LevelProgress _levelProgress;
    [SerializeField] private RewardedVideo _rewardedVideo;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Level _level;

    private Vector3 _targetRotation = new Vector3(0f, 90f, 0f);
    private PlayerSpawner _playerSpawner;
    private Saver _saver;
    private PlayerPosition _lastPlayerPosition;
    private Player _startPlayer;

    private void Awake()
    {
        _level.Initialize();
        _playerSpawner = GetComponent<PlayerSpawner>();
        _saver = GetComponent<Saver>();
    }

    private void OnEnable()
    {
        foreach (var props in _props)
            props.PlayerFell += OnPlayerFell;

        _rewardedVideo.RewardedVideoEnded += RestartLevelOnLastPosition;
    }

    private void OnDisable()
    {
        foreach (var props in _props)
            props.PlayerFell -= OnPlayerFell;

        _rewardedVideo.RewardedVideoEnded -= RestartLevelOnLastPosition;
    }

    private void Start()
    {
        Initialize(_saver.GetSelectedID(), _playerStartPosition);
    }

    private void RestartLevelOnLastPosition()
    {
        _game.Deinitialize();
        RemovePlayer();
        Initialize(_saver.GetSelectedID(), _lastPlayerPosition);
    }

    private void Initialize(int id, PlayerPosition playerPosition)
    {
        _startPlayer = _playerSpawner.GetPlayer(id, playerPosition);
        _startPlayer.transform.localRotation = Quaternion.Euler(_targetRotation);
        _playerInput.Initialize(_startPlayer);
        _game.Initialaize(_startPlayer, _levelProgress, _playerInput, _playerStartPosition);
        _levelProgress.Initialize(_startPlayer);
        _followCamera.SetTarget(_startPlayer);
    }

    private void RemovePlayer()
    {
        Destroy(_startPlayer.gameObject);
    }

    private void OnPlayerFell(PlayerPosition playerPosition)
    {
        _game.SetLastPosition(playerPosition);
        _lastPlayerPosition = playerPosition;
    }
}
