using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerAnimator), typeof(PlayerJumper), typeof(GroundDetector))]

public class Player : MonoBehaviour
{
    public event UnityAction Died;
    public event UnityAction LevelCompleted;

    [SerializeField] private ParticleSystem _fallParticle;

    private PlayerAnimator _animator;
    private GroundDetector _groundDetector;
    private bool _doubleJump;

    private void Awake()
    {
        _animator = GetComponent<PlayerAnimator>();
        _groundDetector = GetComponent<GroundDetector>();
    }

    private void OnEnable()
    {
        _groundDetector.Fell += OnFell;
    }

    private void OnDisable()
    {
        _groundDetector.Fell -= OnFell;
    }

    private void Update()
    {
        _animator.SetGrounded(_groundDetector.IsGrounded);

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (_groundDetector.IsGrounded == true || _groundDetector.IsGrounded == false && _doubleJump == false)
            {
                _animator.Jump(); 
            }

            if (_groundDetector.IsGrounded == false && _doubleJump == false)
            {
                _animator.DoubleJump();
                _doubleJump = true;
            }
        }
    }

    private void OnFell(Collision collision)
    {
        _doubleJump = false;
        _animator.ResetJumpTrigger();
        _fallParticle.Play();

        if (collision.transform.TryGetComponent(out Ground ground))
        {
            _animator.HardFall();
            Die();
        }
        else if (collision.transform.TryGetComponent(out Finish finish))
        {
            LevelCompleted?.Invoke();
        }
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
