using Sourse.Root;
using Sourse.UI;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(PlayerAnimator), typeof(PlayerJumper), typeof(GroundDetector))]
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fallParticle;
        [SerializeField] private ParticleSystem _fallOnGroundParticle;

        private PlayerAnimator _animator;
        private PlayerJumper _jumper;
        private PlayerInput _input;
        private LevelFinisher _levelFinisher;
        private PlayerDeath _death;
        private EffectsPlayer _effectsPlayer;
        private GroundDetector _groundDetector;
        private PlayerPositionHandler _positionHandler;
        private bool _isStart = false;

        public bool IsStart => _isStart;

        public void Unsubscribe()
        {
            _animator.Unsubscribe();
            _positionHandler.Unsubscribe();
            _levelFinisher.Unsubscribe();
            _death.Unsubscribe();
            _effectsPlayer.Unsubscribe();
        }

        public void Initialize(PlayerPositionHandler positionHandler, PlayerInput input)
        {
            _animator = GetComponent<PlayerAnimator>();
            _groundDetector = GetComponent<GroundDetector>();
            _jumper = GetComponent<PlayerJumper>();
            _input = input;
            _input.Initialize(_animator);
            _jumper.Initialize(_animator);
            _groundDetector.Initialize();
            _animator.Initialize(_groundDetector);
            _levelFinisher = new LevelFinisher(_groundDetector);
            _death = new PlayerDeath(_groundDetector);
            _effectsPlayer = new EffectsPlayer(_fallParticle, _fallOnGroundParticle, _groundDetector);
            _positionHandler = positionHandler;
            _positionHandler.Subscribe(_groundDetector);
            _levelFinisher.Subscribe();
            _death.Subscribe();
            _effectsPlayer.Subscribe();
        }
    }
}
