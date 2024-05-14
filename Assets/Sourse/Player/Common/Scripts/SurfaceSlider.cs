using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class SurfaceSlider : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundMask;

        private Vector3 _normal;
        private Transform _transform;
        private BoxCollider _boxCollider;
        private Vector3 _halfSize;
        private Vector3 _target;
        private RaycastHit _hitInfo;

        public void Initialize()
        {
            _transform = transform;
            _boxCollider = GetComponent<BoxCollider>();
            _halfSize = _boxCollider.size / 2;
        }

        public float Dot(Vector3 localAxis)
        {
            if (_normal == Vector3.zero)
                return 1f;

            return Vector3.Dot(_normal, localAxis);
        }

        public bool TryGetTargetPosition(Vector3 target, out Vector3 targetPosition)
        {
            Quaternion orientation = _transform.localRotation;
            _target = target;

            if (Physics.BoxCast(
                target,
                _halfSize,
                Vector3.down,
                out _hitInfo,
                orientation,
                Mathf.Infinity,
                _groundMask))
            {
                float targetY = _hitInfo.point.y + _halfSize.y;

                if (targetY <= 0)
                {
                    targetPosition = Vector3.zero;
                    return false;
                }

                targetPosition = new Vector3(target.x, targetY, target.z);
                _target = targetPosition;
                return true;
            }

            targetPosition = Vector3.zero;
            return false;
        }

        private void OnCollisionStay(Collision collision)
            => _normal = collision.contacts[0].normal;

        private void OnCollisionExit(Collision collision)
            => _normal = Vector3.zero;

        private void OnDrawGizmos()
        {
            if (_boxCollider == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_target, _boxCollider.size);
        }
    }
}
