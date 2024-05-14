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
        private SurfaceSlider _surfaceSlider;
        private float _lastDot;
        private GroundDetector _groundDetector;

        public void Initialize(SurfaceSlider surfaceSlider, GroundDetector groundDetector)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
            _surfaceSlider = surfaceSlider;
            _groundDetector = groundDetector;
        }

        public void UpdatePhysics()
        {
            Vector3 up = _transform.up;
            float surfaceDot = _surfaceSlider.Dot(_transform.forward);
            float dot = Vector3.Dot(up, Vector3.up);
            float targetDot = surfaceDot + dot;
            Vector3 axis = Vector3.Cross(up, Vector3.up);
            Stabilize(dot, targetDot, axis);
            Damping(dot, targetDot, axis);
            ClampRotation(up, dot);
        }

        private void Stabilize(float dot, float targetDot, Vector3 axis)
        {
            if (_groundDetector.IsGrounded)
            {
                if (dot < targetDot)
                {
                    _rigidbody.AddTorque(new Vector3(0f, 0f, axis.z) * (1 - dot) * _stabilizerForce);
                }
            }
            else 
            {
                if (dot > 0)
                {
                    _rigidbody.AddTorque(new Vector3(0f, 0f, axis.z) * (1 - dot) * _stabilizerForce);
                }
            }
        }

        private void Damping(float dot, float targetDot, Vector3 axis)
        {
            float difference = (_groundDetector.IsGrounded ? (_lastDot - targetDot) : (_lastDot - dot)) * Time.fixedDeltaTime;

            if (difference > 0)
                _rigidbody.AddTorque(-new Vector3(0f, 0f, axis.z) * difference * _damping);

            _lastDot = dot;
        }

        private void ClampRotation(Vector3 up, float dot)
        {
            if (dot <= 0)
            {
                Vector3 targetVector = Vector3.ProjectOnPlane(up, Vector3.up).normalized;
                Quaternion targetRotaion = Quaternion.FromToRotation(up, targetVector);
                _transform.rotation *= targetRotaion;
            }
        }
    }
}
