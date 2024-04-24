using System;
using Sourse.Constants;
using Sourse.Ground;
using Sourse.Level;
using UnityEngine;

namespace Sourse.Game.Lose
{
    public class GameLoss
    {
        private readonly LevelProgress _levelProgress;
        private readonly DeadZone _deadZone;

        public GameLoss(LevelProgress levelProgress, DeadZone deadZone)
        {
            _levelProgress = levelProgress;
            _deadZone = deadZone;
        }

        public event Action<float> GameOver;

        public void Subscribe()
            => _deadZone.LevelLost += OnLevelLost;

        public void Unsubscribe()
            => _deadZone.LevelLost -= OnLevelLost;

        private void OnLevelLost()
        {
            float currentDistance = _levelProgress.GetCurrentDistance();
            float percent =
                Mathf.Ceil(currentDistance * PlayerParameter.PercentRatio);
            GameOver?.Invoke(percent);
        }
    }
}
