using UnityEngine;

[RequireComponent(typeof(PlayerSpawner))]
public class Root : MonoBehaviour
{
    [SerializeField] private FollowCamera _followCamera;
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private PlayerPosition _playerPosition;
    [SerializeField] private Game _game;
    [SerializeField] private LevelProgress _levelProgress;

    private PlayerSpawner _playerSpawner;
    private Vector3 _targetRotation = new Vector3(0f, 90f, 0f);

    private void Awake()
    {
        _playerSpawner = GetComponent<PlayerSpawner>();
    }

    private void Start()
    {
        Player player = _playerSpawner.GetPlayer(_playerTemplate, _playerPosition);
        player.transform.localRotation = Quaternion.Euler(_targetRotation);
        _followCamera.SetTarget(player);
        _game.Initialaize(player, _levelProgress);
        _levelProgress.Initialize(player);
    }
}
