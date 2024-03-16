using Sourse.Finish;
using UnityEngine;

namespace Sourse.Level
{
    [RequireComponent(typeof(LevelProgressView), typeof(Saver.Saver))]
    public class LevelProgress : MonoBehaviour
    {
        private const string LevelProgressKey = "LevelProgress";

        [SerializeField] private FinishPosition _finishPosition;

        private LevelProgressView _levelProgressView;
        private float _distance;
        private float _maxDistance = 1f;
        private Player.Common.Scripts.Player _player;
        private Saver.Saver _saver;

        public float CurrentDistance { get; private set; }

        private void Update()
        {
            if (_player == null)
                return;

            CurrentDistance = _maxDistance - Vector3.Distance(_player.transform.position, _finishPosition.transform.position) / _distance;
            _levelProgressView.UpdateProgressBar(CurrentDistance);
        }

        public void Initialize(Player.Common.Scripts.Player player)
        {
            _saver = GetComponent<Saver.Saver>();
            _levelProgressView = GetComponent<LevelProgressView>();
            _player = player;

            if (_saver.TryGetValue(LevelProgressKey, out float value))
                _distance = value;
            else
                _distance = Vector3.Distance(player.transform.position, _finishPosition.transform.position);
        }

        public void SaveDistance() => _saver.Save(LevelProgressKey, _distance);

        public void DeleteSavedDistance() => _saver.TryDeleteSaveData(LevelProgressKey);
    }
}