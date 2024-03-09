using UnityEngine;

public class JointRotater : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint _head;
    [SerializeField] private ConfigurableJoint _pelvis;
    [SerializeField] private ConfigurableJoint _middleSpine;
    [SerializeField] private ConfigurableJoint _leftArm;
    [SerializeField] private ConfigurableJoint _leftElbow;
    [SerializeField] private ConfigurableJoint _rightArm;
    [SerializeField] private ConfigurableJoint _rightElbow;
    [SerializeField] private ConfigurableJoint _rightHips;
    [SerializeField] private ConfigurableJoint _rightKnee;
    [SerializeField] private ConfigurableJoint _leftHips;
    [SerializeField] private ConfigurableJoint _leftKnee;
    [SerializeField] private Rigidbody _pelvisBody;
    [SerializeField] private Transform _headTarget;
    [SerializeField] private Transform _pelvisTarget;
    [SerializeField] private Transform _middleSpineTarget;
    [SerializeField] private Transform _leftArmTarget;
    [SerializeField] private Transform _leftElbowTarget;
    [SerializeField] private Transform _rightArmTarget;
    [SerializeField] private Transform _rightElbowTarget;
    [SerializeField] private Transform _rightHipsTarget;
    [SerializeField] private Transform _rightKneeTarget;
    [SerializeField] private Transform _leftHipsTarget;
    [SerializeField] private Transform _leftKneeTarget;
    [SerializeField] private float _jumpForce;

    private void Start()
    {
        //_headTarget.rotation = _head.targetRotation;
        //_pelvisTarget.rotation = _pelvis.targetRotation;
        //_middleSpineTarget.rotation = _middleSpine.targetRotation;
        //_leftArmTarget.rotation = _leftArm.targetRotation;
        //_leftElbowTarget.rotation = _leftElbow.targetRotation;
        //_rightArmTarget.rotation = _rightArm.targetRotation;
        //_rightElbowTarget.rotation = _rightElbow.targetRotation;
        //_rightHipsTarget.rotation = _rightHips.targetRotation;
        //_rightKneeTarget.rotation = _rightKnee.targetRotation;
        //_leftHipsTarget.rotation = _leftHips.targetRotation;
        //_leftKneeTarget.rotation = _leftKnee.targetRotation;
        //_headTarget.position = _head.transform.position;
        //_pelvisTarget.position = _pelvis.transform.position;
        //_middleSpineTarget.position = _middleSpine.transform.position;
        //_leftArmTarget.position = _leftArm.transform.position;
        //_leftElbowTarget.position = _leftElbow.transform.position;
        //_rightArmTarget.position = _rightArm.transform.position;
        //_rightElbowTarget.position = _rightElbow.transform.position;
        //_rightHipsTarget.position = _rightHips.transform.position;
        //_rightKneeTarget.position = _rightKnee.transform.position;
        //_leftHipsTarget.position = _leftHips.transform.position;
        //_leftKneeTarget.position = _leftKnee.transform.position;
    }

    private void Update()
    {
        //_head.targetRotation = _headTarget.rotation;
        //_pelvis.targetRotation = _pelvisTarget.rotation;
        //_middleSpine.targetRotation = _middleSpineTarget.rotation;
        //_leftArm.targetRotation = _leftArmTarget.rotation;
        //_leftElbow.targetRotation = _leftElbowTarget.rotation;
        //_rightArm.targetRotation = _rightArmTarget.rotation;
        //_rightElbow.targetRotation = _rightElbowTarget.rotation;
        //_rightHips.targetRotation = _rightHipsTarget.rotation;
        //_rightKnee.targetRotation = _rightKneeTarget.rotation;
        //_leftHips.targetRotation = _leftHipsTarget.rotation;
        //_leftKnee.targetRotation = _leftKneeTarget.rotation;

        //if (Input.GetMouseButtonDown(0))
        //    _pelvisBody.AddForce(Vector3.up * _jumpForce);
    }
}
