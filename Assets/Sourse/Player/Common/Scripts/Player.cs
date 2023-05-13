using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerAnimator), typeof(PlayerJumper), typeof(GroundDetector))]
public class Player : MonoBehaviour, IPauseHandler
{
    public event UnityAction Died;
    public event UnityAction LevelCompleted;

    private const float MaxRelativeVelocityY = 5f;

    [SerializeField] private ParticleSystem _fallParticle;
    [SerializeField] private int _id;

    private PlayerAnimator _animator;
    private PlayerJumper _jumper;
    private GroundDetector _groundDetector;
    private bool _doubleJump;
    private bool _jump;
    private bool _isPause;
    private bool _isStart = false;
    private bool _isGameOver = false;
    private bool _isLevelComleted = false;

    public int ID => _id;
    public bool IsStart => _isStart;

    private void Awake()
    {
        _animator = GetComponent<PlayerAnimator>();
        _groundDetector = GetComponent<GroundDetector>();
        _jumper = GetComponent<PlayerJumper>();
        _groundDetector.Initialize();
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
        _animator.SetGrounded(_groundDetector.IsGrounded());
    }

    public void SetPause(bool isPause)
    {
        _isPause = isPause;
    }

    public void SetStart(bool isStart)
    {
        _isStart = isStart;
    }

    public void Jump()
    {
        if (_isPause == true || _isGameOver == true || _isLevelComleted == true)
            return;

        if (_groundDetector.IsGrounded() == true && _jump == false)
        {
            _animator.Jump();
            _jump = true;
        }
        else if (_groundDetector.IsGrounded() == false && _doubleJump == false)
        {
            _animator.DoubleJump();
            _doubleJump = true;
        }
    }

    private void OnFell(Collision collision)
    {
        if (_isStart == false)
            return;

        _jump = false;
        _doubleJump = false;
        _jumper.ResetVelocity();
        _fallParticle.Play();

        if (collision.transform.TryGetComponent(out Ground ground))
        {
            if (_isLevelComleted == true)
                return;

            _isGameOver = true;

            if (collision.relativeVelocity.y >= MaxRelativeVelocityY)
                _animator.HardFall();
            else
                _animator.Defeat();

            Died?.Invoke();
        }
        else if (collision.transform.TryGetComponent(out Finish finish))
        {
            if (_isGameOver == true)
                return;

            if (_jumper.VelocityY == 0)
            {
                _isLevelComleted = true;
                LevelCompleted?.Invoke();
            }
        }
    }
}
