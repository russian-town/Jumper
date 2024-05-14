using System;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class GroundDetector : MonoBehaviour
    {
        public event Action PlayerFell;

        public event Action PlayerStay;

        public event Action PlayerJumped;

        public bool IsGrounded { get; private set; }

        private void OnCollisionEnter(Collision collision)
        {
            IsGrounded = true;
            PlayerFell?.Invoke();
        }

        private void OnCollisionStay(Collision collision)
            => PlayerStay?.Invoke();

        private void OnCollisionExit(Collision collision)
        {
            IsGrounded = false;
            PlayerJumped?.Invoke();
        }
    }
}
