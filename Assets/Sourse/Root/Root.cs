using Sourse.Camera;
using Sourse.Level;
using Sourse.Player.Common.Scripts;
using Sourse.UI;
using UnityEngine;

namespace Sourse.Root
{
    [RequireComponent(typeof(PlayerSpawner), typeof(Saver.Saver))]
    public class Root : MonoBehaviour
    {
        [SerializeField] private FollowCamera _followCamera;
        [SerializeField] private Game.Game _game;
        [SerializeField] private LevelProgress _levelProgress;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Level.Level _level;
        [SerializeField] private ApplicationStatusChecker.ApplicationStatusChecker _applicationStatusChecker;
        [SerializeField] private PlayerPositionHandler _playerPositionHandler;
        [SerializeField] private PlayerPosition _startPlayerPosition;

        private Vector3 _targetRotation = new Vector3(0f, 90f, 0f);
        private PlayerSpawner _playerSpawner;
        private Saver.Saver _saver;
        private Player.Common.Scripts.Player _startPlayer;

        private void Awake()
        {
            _playerSpawner = GetComponent<PlayerSpawner>();
            _saver = GetComponent<Saver.Saver>();
        }

        private void OnDestroy()
        {
            _game.Unsubscribe();
            _startPlayer.Unsubscribe();
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
            _startPlayer.Initialize(_playerPositionHandler, _playerInput);
            _game.Initialaize(_startPlayer, _levelProgress, _playerInput, _startPlayerPosition, _applicationStatusChecker);
            _game.Enable();
            _levelProgress.Initialize(_startPlayer);
            _followCamera.SetTarget(_startPlayer);
        }
    }
}
