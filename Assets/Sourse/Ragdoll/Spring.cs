using UnityEngine;

namespace Sourse.Ragdoll
{
    [RequireComponent(typeof(ConfigurableJoint))]
    public class Spring : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private ConfigurableJoint _configurableJoint;
        private Quaternion _startRotation;

        private void Awake()
        {
            _configurableJoint = GetComponent<ConfigurableJoint>();
            _startRotation = transform.localRotation;
        }

        private void FixedUpdate()
            => _configurableJoint.targetRotation =
                 Quaternion.Inverse(_target.localRotation) * _startRotation;
    }
}
