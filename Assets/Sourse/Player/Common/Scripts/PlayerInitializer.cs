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

        public GroundDetector GroundDetector { get; private set; }
        public PlayerDeath Death { get; private set; }
        public PlayerFinisher Finisher { get; private set; }

        public void Unsubscribe()
        {
            _animator.Unsubscribe();
            Death.Unsubscribe();
            _effectsPlayer.Unsubscribe();
            Finisher.Unsubscribe();
        }

        public void Initialize(PlayerInput input)
        {
            _animator = GetComponent<PlayerAnimator>();
            GroundDetector = GetComponent<GroundDetector>();
            _jumper = GetComponent<PlayerJumper>();
            Finisher = new PlayerFinisher(GroundDetector);
            Finisher.Subscribe();
            _input = input;
            _input.Initialize(_animator);
            _jumper.Initialize(_animator);
            GroundDetector.Initialize();
            _animator.Initialize(GroundDetector);
            Death = new PlayerDeath(GroundDetector);
            _effectsPlayer = new EffectsPlayer(_fallParticle,
                _fallOnGroundParticle, 
                GroundDetector);
            Death.Subscribe();
            _effectsPlayer.Subscribe();
        }
    }
}
