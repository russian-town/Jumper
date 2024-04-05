using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(PlayerAnimator), typeof(PlayerJumper), typeof(GroundDetector))]
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fallParticle;
        [SerializeField] private ParticleSystem _fallOnGroundParticle;

        private PlayerJumper _jumper;
        private EffectsPlayer _effectsPlayer;

        public PlayerAnimator Animator { get; private set; }
        public GroundDetector GroundDetector { get; private set; }
        public PlayerDeath Death { get; private set; }
        public PlayerFinisher Finisher { get; private set; }

        public void Unsubscribe()
        {
            Animator.Unsubscribe();
            Death.Unsubscribe();
            _effectsPlayer.Unsubscribe();
            Finisher.Unsubscribe();
        }

        public void Initialize()
        {
            Animator = GetComponent<PlayerAnimator>();
            GroundDetector = GetComponent<GroundDetector>();
            _jumper = GetComponent<PlayerJumper>();
            Finisher = new PlayerFinisher(GroundDetector);
            Finisher.Subscribe();
            _jumper.Initialize(Animator);
            GroundDetector.Initialize();
            Animator.Initialize(GroundDetector);
            Death = new PlayerDeath(GroundDetector);
            _effectsPlayer = new EffectsPlayer(_fallParticle,
                _fallOnGroundParticle,
                GroundDetector);
            Death.Subscribe();
            _effectsPlayer.Subscribe();
        }
    }
}
