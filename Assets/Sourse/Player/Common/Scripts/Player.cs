using System;
using Sourse.Finish;
using Sourse.Root;
using Sourse.UI;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(PlayerAnimator), typeof(PlayerJumper), typeof(GroundDetector))]
    public class Player : MonoBehaviour
    {
        private const float MaxRelativeVelocityY = 5f;

        [SerializeField] private ParticleSystem _fallParticle;
        [SerializeField] private ParticleSystem _fallOnGroundParticle;
        [SerializeField] private int _id;

        private PlayerAnimator _animator;
        private PlayerJumper _jumper;
        private PlayerInput _input;
        private GroundDetector _groundDetector;
        private PlayerPositionHandler _positionHandler;
        private bool _isStart = false;
        private bool _isGameOver = false;
        private bool _isLevelComleted = false;

        public int ID => _id;
        public bool IsStart => _isStart;

        public event Action Died;
        public event Action LevelCompleted;

        public void Unsubscribe()
        {
            _groundDetector.Fell -= OnFell;
            _animator.Unsubscribe();
            _positionHandler.Unsubscribe();
        }

        public void Initialize(PlayerPositionHandler positionHandler, PlayerInput input)
        {
            _animator = GetComponent<PlayerAnimator>();
            _groundDetector = GetComponent<GroundDetector>();
            _groundDetector.Fell += OnFell;
            _jumper = GetComponent<PlayerJumper>();
            _input = input;
            _input.Initialize(_animator);
            _jumper.Initialize(_animator);
            _groundDetector.Initialize();
            _animator.Initialize(_groundDetector);
            _positionHandler = positionHandler;
            _positionHandler.Subscribe(_groundDetector);
        }

        public void SetStart(bool isStart) => _isStart = isStart;

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Props.Common.Props props))
                LandOnProps();
            else if (collision.transform.TryGetComponent(out Ground.Ground ground))
                Die(collision);
            else if (collision.transform.TryGetComponent(out LevelCompleteSoundPlayer finish))
                FinishLevel();
        }

        private void LandOnProps()
           => _fallParticle.Play();

        private void FinishLevel()
        {
            if (_isLevelComleted == true || _isGameOver == true)
                return;

            _fallParticle.Play();
            LevelCompleted?.Invoke();
            _isLevelComleted = true;
        }

        private void Die(Collision collision)
        {
            _animator.DisableIK();
            _isGameOver = true;
            _fallOnGroundParticle.Play();

            if (collision.relativeVelocity.y >= MaxRelativeVelocityY)
                _animator.HardFall();
            else
                _animator.Defeat();

            Died?.Invoke();
        }
    }
}
