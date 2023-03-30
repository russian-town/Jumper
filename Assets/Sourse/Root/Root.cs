using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerSpawner))]
public class Root : MonoBehaviour
{
    [SerializeField] private FollowCamera _followCamera;
    [SerializeField] private Audio _audio;
    [SerializeField] private List<Player> _players = new List<Player>();
    [SerializeField] private List<Props> _props = new List<Props>();
    [SerializeField] private Player _defaultTemplate;
    [SerializeField] private Skin _defaultSkin;
    [SerializeField] private PlayerPosition _playerStartPosition;
    [SerializeField] private Game _game;
    [SerializeField] private OpenableSkinHandler _openableSkinHandler;
    [SerializeField] private LevelProgress _levelProgress;
    [SerializeField] private Shop _shop;
    [SerializeField] private RewardedVideo _rewardedVideo;

    private Vector3 _targetRotation = new Vector3(0f, 90f, 0f);
    private PlayerSpawner _playerSpawner;
    private PlayerPosition _lastPlayerPosition;
    private Player _startPlayer;

    private void Awake()
    {
        _playerSpawner = GetComponent<PlayerSpawner>();
    }

    private void OnEnable()
    {
        foreach (var props in _props)
            props.PlayerFell += OnPlayerFell;

        _shop.Initialized += OnInitialized;
        _shop.Selected += OnSelected;
        _rewardedVideo.RewardedVideoEnded += RestartLevelOnLastPosition;
    }

    private void OnDisable()
    {
        foreach (var props in _props)
            props.PlayerFell -= OnPlayerFell;

        _shop.Initialized -= OnInitialized;
        _shop.Selected -= OnSelected;
        _rewardedVideo.RewardedVideoEnded -= RestartLevelOnLastPosition;
    }

    private void Start()
    {
        _shop.Initialize(_openableSkinHandler, _defaultSkin);
    }

    private void OnInitialized(int id)
    {
        StartGame(id, _playerStartPosition);
    }

    private void RestartLevelOnLastPosition()
    {
        _game.StartGame();
        RemovePlayer();
        StartGame(_shop.SelectedID, _lastPlayerPosition);
    }

    private void StartGame(int id, PlayerPosition playerPosition)
    {
        _audio.Initialize();
        _startPlayer = _playerSpawner.GetPlayer(TryGetPlayer(id), playerPosition);
        _startPlayer.transform.localRotation = Quaternion.Euler(_targetRotation);
        _startPlayer.Initialize(_game);
        _game.Initialaize(_startPlayer, _levelProgress, _playerStartPosition);
        _levelProgress.Initialize(_startPlayer);
        _followCamera.SetTarget(_startPlayer);
    }

    private void RemovePlayer()
    {
        Destroy(_startPlayer.gameObject);
    }

    private Player TryGetPlayer(int id)
    {
        foreach (var player in _players)
        {
            if (player.ID == id)
            {
                return player;
            }
        }

        return _defaultTemplate;
    }

    private void OnSelected(int id)
    {
        RemovePlayer();
        StartGame(id, _playerStartPosition);
    }

    private void OnPlayerFell(PlayerPosition playerPosition)
    {
        _game.SetLastPosition(playerPosition);
        _lastPlayerPosition = playerPosition;
    }
}
