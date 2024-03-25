using Sourse.Finish;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using UnityEngine;

namespace Sourse.Level
{
    public class LevelProgress : IDataReader, IDataWriter
    {
        private readonly FinishPosition _finishPosition;
        private readonly float _maxDistance = 1f;
        private readonly PlayerInitializer _playerInitializer;
        
        private float _distance;

        public LevelProgress(PlayerInitializer playerInitializer,
            FinishPosition finishPosition)
        {
            _playerInitializer = playerInitializer;
            _finishPosition = finishPosition;
        }

        public float GetDistance()
        {
            if (_playerInitializer == null)
                return _maxDistance;

            float distanceToFinish = Vector3.Distance(_playerInitializer.transform.position,
                _finishPosition.transform.position);
            return _maxDistance - distanceToFinish / _distance;
        }

        public void Read(PlayerData playerData)
            => _distance = playerData.Distance;

        public void Write(PlayerData playerData)
            => playerData.Distance = _distance;
    }
}