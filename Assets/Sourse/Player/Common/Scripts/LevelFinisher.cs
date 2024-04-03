using System;
using Sourse.Game.Finish;
using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Player.Common
{
    public class LevelFinisher
    {
        private readonly GroundDetector _groundDetector;
        private readonly Level.Level _level;

        public event Action<int> LevelCompleted;

        public LevelFinisher(GroundDetector groundDetector,
            Level.Level level)
        {
            _groundDetector = groundDetector;
            _level = level;
        }

        public void Subscribe()
            => _groundDetector.Fell += OnFell;

        public void Unsubscribe()
            => _groundDetector.Fell -= OnFell;

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out LevelCompleteSoundPlayer _))
                FinishLevel();
        }

        private void FinishLevel()
            => LevelCompleted?.Invoke(_level.CurrentLevelNumber);
    }
}
