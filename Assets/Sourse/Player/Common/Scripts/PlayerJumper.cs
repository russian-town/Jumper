using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpLength;
    [SerializeField] private float _doubleJumpForce;
    [SerializeField] private float _doubleJumpLength;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector3(_jumpLength, _jumpForce, 0f);
    }

    public void DoubleJump()
    {
        _rigidbody.velocity += new Vector3(_doubleJumpLength, _doubleJumpForce, 0f);
    }
}
