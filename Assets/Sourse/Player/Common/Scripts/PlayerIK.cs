using UnityEngine;

public class PlayerIK : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] private float _weight;
    [SerializeField] private HeadPosition _headPosition;

    private Animator _animator;
    private float _startWeight;

    private void OnAnimatorIK()
    {
        _animator.SetLookAtWeight(_weight);
        _animator.SetLookAtPosition(_headPosition.Current);
    }

    public void Initialize(Animator animator)
    {
        _animator = animator;
        _startWeight = _weight;
    }

    public void Enable() => _weight = _startWeight;

    public void Disable() => _weight = 0f;
}
