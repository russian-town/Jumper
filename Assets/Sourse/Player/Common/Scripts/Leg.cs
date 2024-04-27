using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class Leg : MonoBehaviour
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private float _radius;
        [SerializeField] private float _maxDistance;
        [SerializeField] private LayerMask _propsLayer;

        public bool CheckGround()
        {
            if (Physics.SphereCast(
                _origin.position,
                _radius,
                _origin.right,
                out RaycastHit hitInfo,
                _maxDistance,
                _propsLayer,
                QueryTriggerInteraction.Ignore))
            {
                return true;
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Vector3 startPoint = _origin.position;
            Vector3 endPoint = _origin.position + _origin.right * _maxDistance;
            Gizmos.DrawWireCube(startPoint, Vector3.one * 0.2f);
            Gizmos.DrawLine(startPoint, endPoint);
            Gizmos.DrawSphere(endPoint, _radius);
        }
    }
}
