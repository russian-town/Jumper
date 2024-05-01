using System;
using Sourse.Enviroment.Common;
using UnityEngine;

namespace Sourse.Ground
{
    public class DeadZone : Item
    {
        [SerializeField] private ParticleSystem _fallParticleTemplate;

        public event Action LevelLost;

        public void GameOver(Vector3 point)
        {
            ParticleSystem fallParticle = Instantiate(_fallParticleTemplate, point, Quaternion.identity);
            fallParticle.Play();
            LevelLost?.Invoke();
        }
    }
}
