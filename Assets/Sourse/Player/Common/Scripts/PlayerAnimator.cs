using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private float _animationDelay;

    private const string JumpParametr = "Jump";
    private const string DoubleJumpParametr = "DoubleJump";
    private const string IsGroundedParametr = "IsGrounded";
    private const string HardFallParametr = "HardFall";

    private Animator _animator;

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
    }

    public void DoubleJump()
    {
        _animator.SetTrigger(DoubleJumpParametr);
        StartCoroutine(StartDoubleJump());
    }

    public void ResetJumpTrigger()
    {
        _animator.ResetTrigger(JumpParametr);
    }

    public void HardFall()
    {
        _animator.SetBool(HardFallParametr, true);
    }

    private IEnumerator StartDoubleJump()
    {
        yield return new WaitForSeconds(_animationDelay);
        _animator.ResetTrigger(DoubleJumpParametr);
    }
}