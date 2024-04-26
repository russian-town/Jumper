using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class AntiRoll : MonoBehaviour
    {
        [SerializeField] private float _stabilizerForce;
        [SerializeField] private float _damping;
        [SerializeField] private float _maxDot;

        private Rigidbody _rigidbody;
        private Transform _transform;
        private float _lastDot;

        public void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
        }

        public void UpdatePhysics()
        {
            Vector3 up = _transform.up;
            float dot = Vector3.Dot(up, Vector3.up);
            Vector3 axis = Vector3.Cross(up, Vector3.up);
            Stabilize(dot, axis);
            Damping(dot, axis);
            ClampRotation(up, dot);
        }

        private void Stabilize(float dot, Vector3 axis)
        {
            if (dot > 0)
                _rigidbody.AddTorque(new Vector3(0f, 0f, axis.z) * (1 - dot) * _stabilizerForce);
        }

        private void Damping(float dot, Vector3 axis)
        {
            float difference = (_lastDot - dot) * Time.fixedDeltaTime;

            if (difference > 0)
                _rigidbody.AddTorque(-new Vector3(0f, 0f, axis.z) * difference * _damping);

            _lastDot = dot;
        }

        private void ClampRotation(Vector3 up, float dot)
        {
            if (dot <= _maxDot)
            {
                Vector3 targetVector = Vector3.ProjectOnPlane(up, Vector3.up).normalized;
                Quaternion targetRotaion = Quaternion.FromToRotation(up, targetVector);
                _transform.rotation *= new Quaternion(1f, 1f, targetRotaion.z, 1f);
            }
        }
    }
}
