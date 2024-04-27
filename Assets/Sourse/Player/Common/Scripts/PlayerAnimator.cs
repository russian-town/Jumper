using System;
using System.Collections;
using Sourse.Constants;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private float _animationDelay;

        private Animator _animator;
        private Coroutine _startResetTriggers;
        private GroundDetector _groundDetector;

        public bool IsJumped { get; private set; }

        public bool IsdDoubleJumped { get; private set; }

        public event Action Jumped;

        public event Action DoubleJumped;

        private void Update()
           => _animator.SetBool(AnimationName.IsGrounded, _groundDetector.IsGrounded);

        public void Initialize(GroundDetector groundDetector)
        {
            _animator = GetComponent<Animator>();
            _groundDetector = groundDetector;
        }

        public void CallJumpEvent()
            => Jumped?.Invoke();

        public void CallDoubleJumpEvent()
            => DoubleJumped?.Invoke();

        public void Jump()
        {
            if (IsJumped || !_groundDetector.IsGrounded)
                return;

            IsJumped = true;
            _animator.SetTrigger(AnimationName.Jump);

            if (IsdDoubleJumped || !IsJumped || _groundDetector.IsGrounded)
                return;

            IsdDoubleJumped = true;
            _animator.SetTrigger(AnimationName.DoubleJump);
        }

        private void HardFall()
           => _animator.SetTrigger(AnimationName.HardFall);

        private void Defeat()
           => _animator.SetBool(AnimationName.Defeat, true);

        private void Die(float relativeVelocity)
        {
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
        }
    }
}
