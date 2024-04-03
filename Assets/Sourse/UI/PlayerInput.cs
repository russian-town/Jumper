using System;
using Sourse.Pause;
using Sourse.Player.Common.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sourse.UI
{
    public class PlayerInput : MonoBehaviour, IPointerDownHandler, IPauseHandler
    {
        private PlayerAnimator _animator;
        private bool _isPause;
        private bool _isGameOver;
        private bool _isLevelComleted;

        public event Action Pressed;

        public void Initialize(PlayerAnimator animator)
            => _animator = animator;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_animator == null)
                return;

            if (_isPause == true || _isGameOver == true || _isLevelComleted == true)
                return;

            if (_animator.IsJumped == false && _animator.IsGrounded == true)
                _animator.Jump();
            else if (_animator.IsdDoubleJumped == false && _animator.IsJumped == true
                || _animator.IsdDoubleJumped == false && _animator.IsGrounded == false)
                _animator.DoubleJump();

            Pressed?.Invoke();
        }

        public void SetPause(bool isPause)
            => _isPause = isPause;
    }
}
