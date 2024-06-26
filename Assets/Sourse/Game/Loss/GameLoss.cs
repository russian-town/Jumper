using System;
using Sourse.Constants;
using Sourse.Level;
using Sourse.Player.Common;
using UnityEngine;

namespace Sourse.Game.Lose
{
    public class GameLoss
    {
        private readonly LevelProgress _levelProgress;
        private readonly PlayerDeath _playerDeath;

        public GameLoss(LevelProgress levelProgress, PlayerDeath playerDeath)
        {
            _levelProgress = levelProgress;
            _playerDeath = playerDeath;
        }

        public event Action<float> GameOver;

        public void Subscribe()
            => _playerDeath.Died += OnDied;

        public void Unsubscribe()
            => _playerDeath.Died -= OnDied;

        private void OnDied()
        {
            float currentDistance = _levelProgress.GetCurrentDistance();
            float percent =
                Mathf.Ceil(currentDistance * PlayerParameter.PercentRatio);
            GameOver?.Invoke(percent);
        }
    }
}
