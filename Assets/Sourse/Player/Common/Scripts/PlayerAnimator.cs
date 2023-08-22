using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour, IGroundedHandler
{
    private const float MaxWeight = 1f;
    private const float MinWeight = 0f;
    private const string JumpParametr = "Jump";
    private const string DoubleJumpParametr = "DoubleJump";
    private const string IsGroundedParametr = "IsGrounded";
    private const string HardFallParametr = "HardFall";
    private const string DefeatParametr = "Defeat";

    [SerializeField] private float _animationDelay;
    [SerializeField] private HeadPosition _headPosition;

    private Animator _animator;
    private Coroutine _startResetTriggers;
    private float _weight;

    public Animator Current => _animator;

    private void OnAnimatorIK()
    {
        _animator.SetLookAtWeight(_weight);
        _animator.SetLookAtPosition(_headPosition.Current);
    }

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        _weight = MaxWeight;
    }

    public void DisableIK()
    {
        _weight = MinWeight;
    }

    public void SetGrounded(bool isGrounded)
    {
        _animator.SetBool(IsGroundedParametr, isGrounded);
    }

    public void Jump()
    {
        _animator.SetTrigger(JumpParametr);
        _startResetTriggers = StartCoroutine(ResetTriggers());
    }

    public void Defeat()
    {
        _animator.SetBool(DefeatParametr, true);
    }

    public void DoubleJump()
    {
        _animator.SetTrigger(DoubleJumpParametr);
        _startResetTriggers = StartCoroutine(ResetTriggers());
    }

    public void HardFall()
    {
        _animator.SetTrigger(HardFallParametr);
    }

    private IEnumerator ResetTriggers()
    {
        if (_startResetTriggers != null)
            StopCoroutine(_startResetTriggers);

        yield return new WaitForSeconds(_animationDelay);
        _animator.ResetTrigger(JumpParametr);
        _animator.ResetTrigger(DoubleJumpParametr);
    }
}
