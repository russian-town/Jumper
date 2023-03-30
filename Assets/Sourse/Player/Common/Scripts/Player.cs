using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerAnimator), typeof(PlayerJumper), typeof(GroundDetector))]
public class Player : MonoBehaviour, IPauseHandler
{
    public event UnityAction Died;
    public event UnityAction LevelCompleted;

    [SerializeField] private ParticleSystem _fallParticle;
    [SerializeField] private int _id;

    private PlayerAnimator _animator;
    private GroundDetector _groundDetector;
    private Game _game;
    private bool _doubleJump;
    private bool _isPause;
    private bool _isStart = false;

    public int ID => _id;
    public bool IsStart => _isStart;

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

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !_isPause)
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

    public void Initialize(Game game)
    {
        _game = game;
    }

    public void SetPause(bool isPause)
    {
        _isPause = isPause;
    }

    public void SetStart(bool isStart)
    {
        _isStart = isStart;
    }

    private void OnFell(Collision collision)
    {
        if (_isStart == false)
            return;

        _doubleJump = false;
        _animator.ResetJumpTrigger();

        if (collision.transform.TryGetComponent(out Props props))
        {
            _fallParticle.Play();
        }
        else if (collision.transform.TryGetComponent(out Ground ground))
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
