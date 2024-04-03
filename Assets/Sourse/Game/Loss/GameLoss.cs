using System;
using Sourse.Constants;
using Sourse.Level;
using Sourse.Pause;
using Sourse.Player.Common;
using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Game.Lose
{
    public class GameLoss : IPauseHandler
    {
        private readonly LevelProgress _levelProgress;
        private readonly PlayerPosition _playerLastPosition;
        private readonly PlayerPosition _playerStartPosition;
        private readonly PlayerDeath _playerDeath;

        public event Action<float> GameOver;

        public GameLoss(PlayerPosition playerStartPosition,
            LevelProgress levelProgress,
            PlayerDeath playerDeath)
        {
            _playerStartPosition = playerStartPosition;
            _levelProgress = levelProgress;
            _playerDeath = playerDeath;
        }

        public void Subscribe()
        {
            _playerDeath.Died += OnDied;
        }

        public void Unsubscribe()
        {
            _playerDeath.Died -= OnDied;
        }

        public void SetPause(bool isPause) { }

        private void OnDied()
        {
            float percent =
                Mathf.Ceil(_levelProgress.GetCurrentDistance() * PlayerParameter.PercentRatio);
            GameOver?.Invoke(percent);
        }

        /*private void GameOver()
        {
            

            if (_playerLastPosition == null || _playerLastPosition == _playerStartPosition)
            {
            }
            else
            {
            }
        }*/
    }
}
