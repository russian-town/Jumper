using System;
using System.Collections;
using Sourse.Constants;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private float _animationDelay;
        [SerializeField] private HeadPosition _headPosition;

        private Animator _animator;
        private Coroutine _startResetTriggers;
        private float _weight;
        private GroundDetector _groundDetector;

        public bool IsJumped { get; private set; }

        public bool IsdDoubleJumped { get; private set; }

        public bool IsGrounded => _groundDetector.IsGrounded();

        public event Action Jumped;

        public event Action DoubleJumped;

        private void OnAnimatorIK()
        {
            _animator.SetLookAtWeight(_weight);
            _animator.SetLookAtPosition(_headPosition.Current);
        }

        private void Update()
           => _animator.SetBool(AnimationName.IsGrounded, IsGrounded);

        public void Initialize(GroundDetector groundDetector)
        {
            _animator = GetComponent<Animator>();
            _groundDetector = groundDetector;
            _groundDetector.Fell += OnFell;
            _weight = AnimationParameter.MaxWeight;
        }

        public void Unsubscribe()
            => _groundDetector.Fell -= OnFell;

        public void CallJumpEvent()
            => Jumped?.Invoke();

        public void CallDoubleJumpEvent()
            => DoubleJumped?.Invoke();

        public void Jump()
        {
            IsJumped = true;
            _animator.SetTrigger(AnimationName.Jump);
            _startResetTriggers = StartCoroutine(ResetTriggers());
        }

        public void DoubleJump()
        {
            IsdDoubleJumped = true;
            _animator.SetTrigger(AnimationName.DoubleJump);
            _startResetTriggers = StartCoroutine(ResetTriggers());
        }

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Ground.DeadZone ground))
                Die(collision.relativeVelocity.y);
        }

        private void DisableIK()
            => _weight = AnimationParameter.MinWeight;

        private void HardFall()
           => _animator.SetTrigger(AnimationName.HardFall);

        private void Defeat()
           => _animator.SetBool(AnimationName.Defeat, true);

        private void Die(float relativeVelocity)
        {
            IsJumped = false;
            IsdDoubleJumped = false;
            DisableIK();

            if (relativeVelocity >= AnimationParameter.MaxRelativeVelocityY)
                HardFall();
            else
                Defeat();
        }

        private IEnumerator ResetTriggers()
        {
            if (_startResetTriggers != null)
                StopCoroutine(_startResetTriggers);

            yield return new WaitForSeconds(_animationDelay);
            _animator.ResetTrigger(AnimationName.Jump);
            _animator.ResetTrigger(AnimationName.DoubleJump);
            IsJumped = false;
            IsdDoubleJumped = false;
        }
    }
}
