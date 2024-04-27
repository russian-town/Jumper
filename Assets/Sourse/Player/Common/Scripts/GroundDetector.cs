using System.Collections.Generic;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class GroundDetector : MonoBehaviour
    {
        [SerializeField] private List<Leg> _legs = new ();

        public bool IsGrounded { get; private set; }

        public void UpdatePhysics()
        {
            foreach (Leg leg in _legs)
            {
                IsGrounded = leg.CheckGround();
                break;
            }
        }
    }
}
