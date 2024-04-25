using System;
using Sourse.Enviroment.Common;
using UnityEngine;

namespace Sourse.Game.FinishContent
{
    [RequireComponent(typeof(BoxCollider))]
    public class Finish : Item
    {
        private BoxCollider _boxCollider;

        public event Action LevelCompleted;

        public void CompleteLevel()
        {
            LevelCompleted?.Invoke();
        }

        public override void Initialize()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        protected override Collider GetCollider()
        {
            return _boxCollider;
        }
    }
}
