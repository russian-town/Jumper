using System;
using Sourse.Enviroment.Barrel;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class GroundDetector : MonoBehaviour
    {
        private const float Denominator = 2f;
        private const float Threshold = .5f;

        [SerializeField] private float _distance;
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private SphereCollider _head;

        private BoxCollider _boxCollider;
        private bool _canDetect = true;

        public event Action<Collision> Fell;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Barrel barrel))
                return;

            _canDetect = !Physics.ComputePenetration(
                _head,
                _head.transform.position,
                _head.transform.rotation,
                collision.collider,
                collision.transform.position,
                collision.transform.rotation,
                out Vector3 direction,
                out float distance
            );

            if (Vector3.Dot(collision.GetContact(0).normal, Vector3.up) > Threshold && _canDetect)
                Fell?.Invoke(collision);
        }

        public void Initialize()
           => _boxCollider = GetComponent<BoxCollider>();

        public bool IsGrounded()
        {
            Vector3 halfScale = transform.localScale / Denominator;

            if (Physics.BoxCast(
                _boxCollider.bounds.center,
                halfScale,
                -transform.up,
                out RaycastHit hit,
                transform.rotation,
                _distance,
                _groundLayerMask,
                QueryTriggerInteraction.Ignore))
            {
                if (Vector3.Dot(hit.normal, transform.up) > Threshold)
                    return true;
            }

            return false;
        }
    }
}
