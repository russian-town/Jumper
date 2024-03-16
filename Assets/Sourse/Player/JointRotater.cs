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
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _pelvisBody;
    [SerializeField] private Transform _centerOfMass;


    private Quaternion _startHeadRotation;
    private Quaternion _startPelvisRotation;
    private Quaternion _startMiddleSpineRotation;
    private Quaternion _startLeftArmRotation;
    private Quaternion _startLeftElbowRotation;
    private Quaternion _startRightArmRotation;
    private Quaternion _startRightElboweRotation;
    private Quaternion _startRightHipsRotation;
    private Quaternion _startRightKneeRotation;
    private Quaternion _startLeftHipsRotation;
    private Quaternion _startLeftKneeRotation;

    private void Start()
    {
        _pelvisBody.centerOfMass = _centerOfMass.localPosition;
        _startHeadRotation = _head.transform.localRotation;
        _startPelvisRotation = _pelvis.transform.localRotation;
        _startMiddleSpineRotation = _middleSpine.transform.localRotation;
        _startLeftArmRotation = _leftArm.transform.localRotation;
        _startLeftElbowRotation = _leftElbow.transform.localRotation;
        _startRightArmRotation = _rightArm.transform.localRotation;
        _startRightElboweRotation = _rightElbow.transform.localRotation;
        _startRightHipsRotation = _rightHips.transform.localRotation;
        _startRightKneeRotation = _rightKnee.transform.localRotation;
        _startLeftHipsRotation = _leftHips.transform.localRotation;
        _startLeftKneeRotation = _leftKnee.transform.localRotation;
    }

    private void Update()
    {
        _head.targetRotation = Quaternion.Inverse(_headTarget.localRotation) * _startHeadRotation;
        _pelvis.targetRotation = Quaternion.Inverse(_pelvisTarget.localRotation) * _startPelvisRotation;
        _middleSpine.targetRotation = Quaternion.Inverse(_middleSpineTarget.localRotation) * _startMiddleSpineRotation;
        _leftArm.targetRotation = Quaternion.Inverse(_leftArmTarget.localRotation) * _startLeftArmRotation;
        _leftElbow.targetRotation = Quaternion.Inverse(_leftElbowTarget.localRotation) * _startLeftElbowRotation;
        _rightArm.targetRotation = Quaternion.Inverse(_rightArmTarget.localRotation) * _startRightArmRotation;
        _rightElbow.targetRotation = Quaternion.Inverse(_rightElbowTarget.localRotation) * _startRightElboweRotation;
        _rightHips.targetRotation = Quaternion.Inverse(_rightHipsTarget.localRotation) * _startRightHipsRotation;
        _rightKnee.targetRotation = Quaternion.Inverse(_rightKneeTarget.localRotation) * _startRightKneeRotation;
        _leftHips.targetRotation = Quaternion.Inverse(_leftHipsTarget.localRotation) * _startLeftHipsRotation;
        _leftKnee.targetRotation = Quaternion.Inverse(_leftKneeTarget.localRotation) * _startLeftKneeRotation;

        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Jump");
            _pelvisBody.AddForce(Vector3.up * _jumpForce);
        }
    }
}
