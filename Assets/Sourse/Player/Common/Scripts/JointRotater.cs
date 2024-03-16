using UnityEngine;

public class JointRotater : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private ConfigurableJoint _joint;
    private Quaternion _startRotation;

    private void Awake()
        => _joint = GetComponent<ConfigurableJoint>();

    private void Start()
        => _startRotation = transform.localRotation;

    private void Update()
       => _joint.targetRotation = Quaternion.Inverse(_target.localRotation) * _startRotation;
}
