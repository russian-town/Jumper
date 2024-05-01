using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(PlayerAnimator), typeof(PhysicsJump), typeof(GroundDetector))]
    [RequireComponent(typeof(JumpAnimation), typeof(PhysicsMovement))]
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fallParticle;
        [SerializeField] private ParticleSystem _fallOnGroundParticle;
        [SerializeField] private AntiRoll _antiRoll;

        private JumpAnimation _jumpAnimation;
        private PhysicsJump _physicsJump;
        private PhysicsMovement _physicsMovement;

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
            _physicsMovement = GetComponent<PhysicsMovement>();
            Animator.Initialize(GroundDetector);
            _jumpAnimation.Initialize();
            _physicsJump.Initialize(_jumpAnimation, Animator, JumpLenght, DoubleJumpLenght);
            _physicsMovement.Initialize();
            _antiRoll.Initialize();
        }

        public void UpdatePhysics()
        {
            _antiRoll.UpdatePhysics();
        }
    }
}
