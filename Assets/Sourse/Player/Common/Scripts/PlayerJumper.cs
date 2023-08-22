using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpLength;
    [SerializeField] private float _doubleJumpForce;
    [SerializeField] private float _doubleJumpLength;

    private Rigidbody _rigidbody;

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ResetVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public void JumpUp()
    {
        _rigidbody.AddForce(new Vector3(_jumpLength, _jumpForce, 0f), ForceMode.Impulse);
    }

    public void DoubleJumpUp()
    {
        _rigidbody.AddForce(new Vector3(_doubleJumpLength, _doubleJumpForce, 0f), ForceMode.Impulse);
    }
}
