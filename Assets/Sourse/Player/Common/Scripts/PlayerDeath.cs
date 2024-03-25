using System;
using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Player.Common
{
    public class PlayerDeath
    {
        private readonly GroundDetector _groundDetector;

        public PlayerDeath(GroundDetector groundDetector)
            => _groundDetector = groundDetector;

        public event Action Died;

        public void Subscribe()
            => _groundDetector.Fell += OnFell;

        public void Unsubscribe()
            => _groundDetector.Fell -= OnFell;

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Ground.Ground _))
                Die();
        }

        private void Die()
            => Died?.Invoke();
    }
}
