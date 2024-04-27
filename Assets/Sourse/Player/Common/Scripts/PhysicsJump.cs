using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class PhysicsJump : MonoBehaviour
    {
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _jumpLenght;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private float _doubleJumpDuration;
        [SerializeField] private float _doubleJumpLenght;
        [SerializeField] private float _doubleJumpHeight;
        [SerializeField] private AnimationCurve _doubleJumpCurve;

        private JumpAnimation _jumpAnimation;
        private PureAnimation _pureAnimation;
        private PlayerAnimator _playerAnimator;

        public void Subsctibe()
        {
            _playerAnimator.Jumped += OnJumped;
            _playerAnimator.DoubleJumped += OnDoubleJumped;
        }

        public void Unsubscribe()
        {
            _playerAnimator.Jumped -= OnJumped;
            _playerAnimator.DoubleJumped -= OnDoubleJumped;
        }

        public void Initialize(JumpAnimation jumpAnimation, PlayerAnimator playerAnimator)
        {
            _jumpAnimation = jumpAnimation;
            _playerAnimator = playerAnimator;
            _pureAnimation = new PureAnimation(this);
        }

        private void OnJumped()
            => Jump(transform.forward, _jumpDuration, _jumpLenght, _jumpHeight, _jumpCurve);

        private void OnDoubleJumped()
            => Jump(transform.forward, _doubleJumpDuration, _jumpLenght, _doubleJumpHeight, _doubleJumpCurve);

        private void Jump(Vector3 direction, float duration, float lenght, float height, AnimationCurve animationCurve)
        {
            Vector3 target = transform.position + (direction * lenght);
            Vector3 startPosition = transform.position;
            PureAnimation fxPlayTime = _jumpAnimation.Play(transform, duration, height, animationCurve);

            _pureAnimation.Play(duration, (progress) =>
            {
                transform.position = Vector3.Lerp(startPosition, target, progress) + fxPlayTime.LastChanges.Positon;
                return null;
            });
        }
    }
}
