using UnityEngine;

public class Pelvis : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private ConfigurableJoint _configurableJoint;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Awake()
    {
        _configurableJoint = GetComponent<ConfigurableJoint>();
        _startPosition = -transform.localPosition;
        _startRotation = _target.rotation;
    }

    private void FixedUpdate()
    {
      /*  _configurableJoint.targetPosition =
        _startPosition + -_target.localPosition;*/
        _configurableJoint.targetRotation =
                 Quaternion.Inverse(_target.rotation) * _startRotation;
    }
}
