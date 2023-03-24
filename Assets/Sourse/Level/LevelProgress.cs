using UnityEngine;

[RequireComponent(typeof(LevelProgressView), typeof(LevelProgressSaver))]
public class LevelProgress : MonoBehaviour
{
    private const string LevelProgressKey = "LevelProgress";

    [SerializeField] private FinishPosition _finishPosition;
    [SerializeField] private float _targetDistance;

    private LevelProgressView _levelProgressView;
    private float _distance;
    private float _maxDistance = 1f;
    private Player _player;
    private LevelProgressSaver _levelProgressSaver;

    public float CurrentDistance { get; private set; }

    public void Initialize(Player player)
    {
        _levelProgressSaver = GetComponent<LevelProgressSaver>();
        _levelProgressView = GetComponent<LevelProgressView>();
        _player = player;

        if(_levelProgressSaver.TryGetValue(LevelProgressKey, out float value))
        {
            _distance = value;
        }
        else
        {
            _distance = Vector3.Distance(player.transform.position, _finishPosition.transform.position);
            _levelProgressSaver.Save(LevelProgressKey, _distance);
        }
    }

    private void Update()
    {
        if (_player == null)
            return;

        if (CurrentDistance >= _targetDistance)
        {
            CurrentDistance = _maxDistance;
            _levelProgressView.UpdateProgressBar(CurrentDistance);
            return;
        }

        CurrentDistance = _maxDistance - Vector3.Distance(_player.transform.position, _finishPosition.transform.position) / _distance;
        _levelProgressView.UpdateProgressBar(CurrentDistance);
    }
}
