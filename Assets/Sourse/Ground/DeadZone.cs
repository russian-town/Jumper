using System;
using Sourse.Enviroment.Common;
using UnityEngine;

namespace Sourse.Ground
{
    [RequireComponent(typeof(BoxCollider))]
    public class DeadZone : Item
    {
        private BoxCollider _boxCollider;

        public event Action LevelLost;

        public override void Initialize()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        public void GameOver()
        {
            LevelLost?.Invoke();
        }

        protected override Collider GetCollider()
        {
            return _boxCollider;
        }
    }
}
