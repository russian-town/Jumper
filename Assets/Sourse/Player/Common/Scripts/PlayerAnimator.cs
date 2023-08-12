using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour, IGroundedHandler
{
    [SerializeField] private float _animationDelay;

    private const string JumpParametr = "Jump";
    private const string DoubleJumpParametr = "DoubleJump";
    private const string IsGroundedParametr = "IsGrounded";
    private const string HardFallParametr = "HardFall";
    private const string DefeatParametr = "Defeat";

    private Animator _animator;
    private Coroutine StartResetTriggers;

    public Animator Current => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetGrounded(bool isGrounded)
    {
        _animator.SetBool(IsGroundedParametr, isGrounded);
    }

    public void Jump()
    {
        _animator.SetTrigger(JumpParametr);
        StartResetTriggers = StartCoroutine(ResetTriggers());
    }

    public void Defeat()
    {
        _animator.SetBool(DefeatParametr, true);
    }

    public void DoubleJump()
    {
        _animator.SetTrigger(DoubleJumpParametr);
        StartResetTriggers = StartCoroutine(ResetTriggers());
    }

    public void HardFall()
    {
        _animator.SetTrigger(HardFallParametr);
    }

    private IEnumerator ResetTriggers()
    {
        if (StartResetTriggers != null)
            StopCoroutine(StartResetTriggers);

        yield return new WaitForSeconds(_animationDelay);
        _animator.ResetTrigger(JumpParametr);
        _animator.ResetTrigger(DoubleJumpParametr);
    }
}
