using System;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator), typeof(PlayerJumper), typeof(GroundDetector))]
public class Player : MonoBehaviour, IPauseHandler, IGroundedHandler
{
    private const float MaxRelativeVelocityY = 5f;

    [SerializeField] private ParticleSystem _fallParticle;
    [SerializeField] private ParticleSystem _fallOnGroundParticle;
    [SerializeField] private int _id;

    private PlayerAnimator _animator;
    private PlayerJumper _jumper;
    private GroundDetector _groundDetector;
    private PlayerPositionHandler _positionHandler;
    private bool _doubleJump;
    private bool _jump;
    private bool _isPause;
    private bool _isStart = false;
    private bool _isGameOver = false;
    private bool _isLevelComleted = false;
    private bool _isGrounded;

    public int ID => _id;
    public bool IsStart => _isStart;

    public event Action Died;
    public event Action LevelCompleted;

    private void OnDisable() => _groundDetector.Fell -= OnFell;

    public void Initialize(PlayerPositionHandler positionHandler)
    {
        _animator = GetComponent<PlayerAnimator>();
        _groundDetector = GetComponent<GroundDetector>();
        _groundDetector.Fell += OnFell;
        _jumper = GetComponent<PlayerJumper>();
        _animator.Initialize();
        _jumper.Initialize();
        _groundDetector.Initialize(new IGroundedHandler[] { _animator, this });
        _positionHandler = positionHandler;
    }

    public void SetPause(bool isPause) => _isPause = isPause;

    public void SetStart(bool isStart) => _isStart = isStart;

    public void Jump()
    {
        if (_isPause == true || _isGameOver == true || _isLevelComleted == true)
            return;

        if (_jump == false && _isGrounded)
        {
            _animator.Jump();
            _jump = true;
        }
        else if (_doubleJump == false && _jump == true || _doubleJump == false && _isGrounded == false)
        {
            _animator.DoubleJump();
            _doubleJump = true;
        }
    }

    public void Bounce() => _jumper.JumpUp();

    public void SetGrounded(bool isGrounded) => _isGrounded = isGrounded;

    private void OnFell(Collision collision)
    {
        if (_isStart == false || _isGameOver == true)
            return;

        if (collision.transform.TryGetComponent(out Props props))
        {
            _positionHandler.OnPlayerFell(props.PlayerPosition, props);
            _jumper.ResetVelocity();
            _fallParticle.Play();
            _jump = false;
            _doubleJump = false;
        }
        else if (collision.transform.TryGetComponent(out Ground ground))
        {
            _animator.DisableIK();

            if (_isLevelComleted == true || _isGameOver == true)
                return;

            _fallOnGroundParticle.Play();

            if (collision.relativeVelocity.y >= MaxRelativeVelocityY)
                _animator.HardFall();
            else
                _animator.Defeat();

            Died?.Invoke();
            _isGameOver = true;
        }
        else if (collision.transform.TryGetComponent(out Finish finish))
        {
            if (_isLevelComleted == true || _isGameOver == true)
                return;

            _fallParticle.Play();
            LevelCompleted?.Invoke();
            _isLevelComleted = true;
        }
    }
}
