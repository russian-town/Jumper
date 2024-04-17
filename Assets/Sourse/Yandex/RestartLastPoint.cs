using Sourse.Enviroment.Common;
using Sourse.Level;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using UnityEngine;

namespace Sourse.Yandex
{
    public class RestartLastPoint : Rewarded, IDataWriter
    {
        private readonly LevelLoader _levelLoader;
        private readonly GroundDetector _groundDetector;

        private PlayerPosition _lastPosition;

        public RestartLastPoint(
            LevelLoader levelLoader,
            GroundDetector groundDetector)
        {
            _levelLoader = levelLoader;
            _groundDetector = groundDetector;
        }

        public void Subscribe()
            => _groundDetector.Fell += OnFell;

        public void Unsubscribe()
            => _groundDetector.Fell -= OnFell;

        public override void Accept()
            => _levelLoader.Restart();

        public void Write(PlayerData playerData)
            => playerData.SpawnPosition = _lastPosition.Position;

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Item props))
                _lastPosition = null;
        }
    }
}
