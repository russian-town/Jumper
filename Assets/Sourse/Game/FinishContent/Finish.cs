using System;
using Sourse.Enviroment.Common;
using UnityEngine;

namespace Sourse.Game.FinishContent
{
    public class Finish : Item
    {
        [SerializeField] private ParticleSystem _finishEffects;

        public event Action LevelCompleted;

        public void CompleteLevel(Vector3 point)
        {
            ParticleSystem finishEffects = Instantiate(_finishEffects, point, Quaternion.identity);
            finishEffects.Play();
            LevelCompleted?.Invoke();
        }
    }
}
