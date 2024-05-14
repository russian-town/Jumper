using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(PlayerAnimator), typeof(PhysicsJump), typeof(GroundDetector))]
    [RequireComponent(typeof(JumpAnimation))]
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fallParticle;
        [SerializeField] private ParticleSystem _fallOnGroundParticle;
        [SerializeField] private AntiRoll _antiRoll;
        [SerializeField] private SurfaceSlider _surfaceSlider;

        private JumpAnimation _jumpAnimation;
        private PhysicsJump _physicsJump;

        public GroundDetector GroundDetector { get; private set; }

        public PlayerAnimator Animator { get; private set; }

        [field: SerializeField] public float JumpLenght { get; private set; }

        [field: SerializeField] public float DoubleJumpLenght { get; private set; }

        public void Subscribe()
        {
            Animator.Subscribe();
            _physicsJump.Subsctibe();
        }

        public void Unsubscribe()
        {
            _physicsJump.Unsubscribe();
            Animator.Unsubscribe();
        }

        public void Initialize()
        {
            GroundDetector = GetComponent<GroundDetector>();
            Animator = GetComponent<PlayerAnimator>();
            _jumpAnimation = GetComponent<JumpAnimation>();
            _physicsJump = GetComponent<PhysicsJump>();
            Animator.Initialize(GroundDetector);
            _jumpAnimation.Initialize();
            _surfaceSlider.Initialize();
            _physicsJump.Initialize(_jumpAnimation, Animator, _surfaceSlider, JumpLenght, DoubleJumpLenght);
            _antiRoll.Initialize(_surfaceSlider, GroundDetector);
        }

        public void UpdatePhysics()
        {
            _antiRoll.UpdatePhysics();
        }
    }
}
