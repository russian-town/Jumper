using Sourse.Constants;
using Sourse.Game.FinishContent;
using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Level
{
    public class LevelProgress
    {
        private readonly FinishPosition _finishPosition;
        private readonly PlayerPosition _startPosition;
        private readonly float _startDistancePlayerToFinish;
        private readonly PlayerInitializer _playerInitializer;
        
        public LevelProgress(
            PlayerInitializer playerInitializer,
            FinishPosition finishPosition,
            PlayerPosition startPosition)
        {
            _playerInitializer = playerInitializer;
            _finishPosition = finishPosition;
            _startPosition = startPosition;
            _startDistancePlayerToFinish =
                GetDistancePlayerToFinish() + GetDistanceStartToPlayer();
        }

        public float GetCurrentDistance()
        {
            if (_playerInitializer == null)
                return PlayerParameter.MaxDistance;

            float normalizeDistancePlayerToFinish =
                GetDistancePlayerToFinish() / _startDistancePlayerToFinish;

            return PlayerParameter.MaxDistance - normalizeDistancePlayerToFinish;
        }

        private float GetDistanceStartToPlayer()
        {
            Vector3 finishPositionXZ = new (
                _playerInitializer.transform.position.x,
                0f, _playerInitializer.transform.position.z);
            Vector3 startPositionXZ = new (
                _startPosition.Position.x,
                0f, _startPosition.Position.z);

            return Vector3.Distance(startPositionXZ, finishPositionXZ);
        }

        private float GetDistancePlayerToFinish()
        {
            Vector3 playerPositionXZ = new (
                _playerInitializer.transform.position.x,
                0f, _playerInitializer.transform.position.z);
            Vector3 finishPositionXZ = new (
                _finishPosition.transform.position.x,
                0f, _finishPosition.transform.position.z);

            return Vector3.Distance(playerPositionXZ, finishPositionXZ);
        }
    }
}