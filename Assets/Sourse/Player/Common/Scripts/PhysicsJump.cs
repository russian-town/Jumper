using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class PhysicsJump : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private AnimationCurve _jumpDuration;
        [SerializeField] private AnimationCurve _doubleJumpDuration;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _doubleJumpHeight;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private ParticleSystem _smokeEffect;

        private JumpAnimation _jumpAnimation;
        private PlayerAnimator _playerAnimator;
        private SurfaceSlider _surfaceSlider;
        private Transform _transform;
        private PureAnimation _pureAnimation;
        private float _jumpLenght;
        private float _doubleJumpLenght;
        private PureAnimation _currentAnimation;
        private Vector3 _target;

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

        public void Initialize(
            JumpAnimation jumpAnimation,
            PlayerAnimator playerAnimator,
            SurfaceSlider surfaceSlider,
            float jumpLenght,
            float doubleJumpLenght)
        {
            _jumpAnimation = jumpAnimation;
            _playerAnimator = playerAnimator;
            _surfaceSlider = surfaceSlider;
            _transform = transform;
            _pureAnimation = new PureAnimation(this);
            _jumpLenght = jumpLenght;
            _doubleJumpLenght = doubleJumpLenght;
        }

        private void OnJumped()
            => Jump(_transform.forward, _jumpDuration, _jumpLenght, _jumpHeight);

        private void OnDoubleJumped()
            => Jump(_transform.forward, _doubleJumpDuration, _doubleJumpLenght, _doubleJumpHeight);

        private void Jump(Vector3 direction, AnimationCurve duration, float lenght, float height)
        {
            _pureAnimation?.Stop();
            _currentAnimation?.Stop();
            _smokeEffect.Stop();
            _smokeEffect.Play();
            Vector3 startPosition = _transform.position;
            _target = _transform.position + (direction * lenght);

            if(_surfaceSlider.TryGetTargetPosition(_target, out Vector3 target))
                _target = target;

            _currentAnimation = _jumpAnimation.Play(_transform, duration, height, _jumpCurve);

            _pureAnimation.Play(duration, (progress) =>
            {
                _transform.position = Vector3.Lerp(startPosition, _target, progress) + _currentAnimation.LastChanges.Positon;
                return null;
            });
        }
    }
}
