using UnityEngine;

[RequireComponent (typeof(Animator))]
public class RagdollJumper : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float _rightFootWeight;
    [SerializeField] [Range(0f, 1f)] private float _leftFootWeight;
    [SerializeField] [Range(0f, 1f)] private float _leftKneeWeight;
    [SerializeField] [Range(0f, 1f)] private float _rightKneeWeight;
    [SerializeField] private Transform _rightFootTarget;
    [SerializeField] private Transform _letfFootTarget;
    [SerializeField] private Transform _rightKneeTarget;
    [SerializeField] private Transform _letfKneeTarget;

    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    private void OnAnimatorIK()
    {
        if( _animator == null )
            return;

        _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, _rightFootWeight);
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, _leftFootWeight);
        _animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, _rightKneeWeight);
        _animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, _leftKneeWeight);
        _animator.SetIKPosition(AvatarIKGoal.RightFoot, _rightFootTarget.position);
        _animator.SetIKPosition(AvatarIKGoal.LeftFoot, _letfFootTarget.position);
        _animator.SetIKHintPosition(AvatarIKHint.RightKnee, _rightKneeTarget.position);
        _animator.SetIKHintPosition(AvatarIKHint.LeftKnee, _letfKneeTarget.position);
    }
}
