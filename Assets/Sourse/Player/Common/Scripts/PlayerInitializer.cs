using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(PlayerAnimator), typeof(PlayerJumper), typeof(GroundDetector))]
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fallParticle;
        [SerializeField] private ParticleSystem _fallOnGroundParticle;

        private PlayerJumper _jumper;

        public PlayerAnimator Animator { get; private set; }

        public GroundDetector GroundDetector { get; private set; }

        public void Unsubscribe()
            => Animator.Unsubscribe();

        public void Initialize()
        {
            Animator = GetComponent<PlayerAnimator>();
            GroundDetector = GetComponent<GroundDetector>();
            _jumper = GetComponent<PlayerJumper>();
            _jumper.Initialize(Animator);
            GroundDetector.Initialize();
            Animator.Initialize(GroundDetector);
        }
    }
}
