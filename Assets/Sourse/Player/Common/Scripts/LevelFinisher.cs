using System;
using Sourse.Finish;
using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Player.Common
{
    public class LevelFinisher
    {
        private GroundDetector _groundDetector;

        public event Action LevelCompleted;

        public LevelFinisher(GroundDetector groundDetector)
            => _groundDetector = groundDetector;

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
            => LevelCompleted?.Invoke();
    }
}
