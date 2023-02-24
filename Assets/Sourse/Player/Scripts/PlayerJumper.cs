using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpAudio;
    [SerializeField] private AudioClip _doubleJumpAudio;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpLength;
    [SerializeField] private float _doubleJumpForce;
    [SerializeField] private float _doubleJumpLength;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector3(_jumpLength, _jumpForce, 0f);
        _audioSource.PlayOneShot(_jumpAudio);
    }

    public void DoubleJump()
    {
        _rigidbody.velocity += new Vector3(_doubleJumpLength, _doubleJumpForce, 0f);
        _audioSource.PlayOneShot(_doubleJumpAudio);
    }
}
