using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(PlayerAnimator), typeof(PlayerJumper), typeof(GroundDetector))]
    [RequireComponent (typeof(AntiRoll))]
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fallParticle;
        [SerializeField] private ParticleSystem _fallOnGroundParticle;

        private PlayerJumper _jumper;
        private AntiRoll _antiRoll;

        public PlayerAnimator Animator { get; private set; }

        public GroundDetector GroundDetector { get; private set; }

        public void Unsubscribe()
            => Animator.Unsubscribe();

        public void Initialize()
        {
            Animator = GetComponent<PlayerAnimator>();
            GroundDetector = GetComponent<GroundDetector>();
            _jumper = GetComponent<PlayerJumper>();
            _antiRoll = GetComponent<AntiRoll>();
            Animator.Initialize(GroundDetector);
            GroundDetector.Initialize();
            _jumper.Initialize(Animator);
            _antiRoll.Initialize();
        }

        public void UpdatePhysics()
        {
            _antiRoll.UpdatePhysics();
        }
    }
}
