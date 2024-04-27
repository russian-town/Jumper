using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(PlayerAnimator), typeof(PhysicsJump), typeof(GroundDetector))]
    [RequireComponent (typeof(AntiRoll), typeof(JumpAnimation))]
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fallParticle;
        [SerializeField] private ParticleSystem _fallOnGroundParticle;

        //private PlayerJumper _jumper;
        private AntiRoll _antiRoll;
        private JumpAnimation _jumpAnimation;
        private PhysicsJump _physicsJump;

        public GroundDetector GroundDetector { get; private set; }
        public PlayerAnimator Animator { get; private set; }

        public void Unsubscribe()
        {
            _physicsJump.Unsubscribe();
        }

        public void Initialize()
        {
            GroundDetector = GetComponent<GroundDetector>();
            Animator = GetComponent<PlayerAnimator>();
            _antiRoll = GetComponent<AntiRoll>();
            _jumpAnimation = GetComponent<JumpAnimation>();
            _physicsJump = GetComponent<PhysicsJump>();
            Animator.Initialize(GroundDetector);
            _antiRoll.Initialize();
            _jumpAnimation.Initialize();
            _physicsJump.Initialize(_jumpAnimation, Animator);
            _physicsJump.Subsctibe();
        }

        public void UpdatePhysics()
        {
            //_antiRoll.UpdatePhysics();
            GroundDetector.UpdatePhysics();
        }
    }
}
