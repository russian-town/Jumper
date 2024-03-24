using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Player.Common
{
    public class EffectsPlayer
    {
        private ParticleSystem _fallParticle;
        private ParticleSystem _fallOnGroundParticle;
        private GroundDetector _groundDetector;

        public EffectsPlayer(ParticleSystem fallParticle,
            ParticleSystem fallOnGroundParticle,
            GroundDetector groundDetector)
        {
            _fallParticle = fallParticle;
            _fallOnGroundParticle = fallOnGroundParticle;
            _groundDetector = groundDetector;
        }

        public void Subscribe()
            => _groundDetector.Fell += OnFell;

        public void Unsubscribe()
            => _groundDetector.Fell -= OnFell;

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Ground.Ground _))
                _fallOnGroundParticle.Play();
            else
                _fallParticle.Play();
        }
    }
}
