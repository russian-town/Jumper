using Sourse.UI;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(PlayerAnimator), typeof(PlayerJumper), typeof(GroundDetector))]
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fallParticle;
        [SerializeField] private ParticleSystem _fallOnGroundParticle;

        private PlayerAnimator _animator;
        private PlayerJumper _jumper;
        private PlayerInput _input;
        private EffectsPlayer _effectsPlayer;
        private GroundDetector _groundDetector;

        public PlayerDeath Death { get; private set; }

        public void Unsubscribe()
        {
            _animator.Unsubscribe();
            Death.Unsubscribe();
            _effectsPlayer.Unsubscribe();
        }

        public void Initialize(PlayerInput input)
        {
            _animator = GetComponent<PlayerAnimator>();
            _groundDetector = GetComponent<GroundDetector>();
            _jumper = GetComponent<PlayerJumper>();
            _input = input;
            _input.Initialize(_animator);
            _jumper.Initialize(_animator);
            _groundDetector.Initialize();
            _animator.Initialize(_groundDetector);
            Death = new PlayerDeath(_groundDetector);
            _effectsPlayer = new EffectsPlayer(_fallParticle,
                _fallOnGroundParticle, 
                _groundDetector);
            Death.Subscribe();
            _effectsPlayer.Subscribe();
        }
    }
}
