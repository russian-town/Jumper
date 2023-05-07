using UnityEngine;

[RequireComponent(typeof(LevelProgressView), typeof(Saver))]
public class LevelProgress : MonoBehaviour
{
    private const string LevelProgressKey = "LevelProgress";

    [SerializeField] private FinishPosition _finishPosition;

    private LevelProgressView _levelProgressView;
    private float _distance;
    private float _maxDistance = 1f;
    private Player _player;
    private Saver _saver;

    public float CurrentDistance { get; private set; }

    public void Initialize(Player player)
    {
        _saver = GetComponent<Saver>();
        _levelProgressView = GetComponent<LevelProgressView>();
        _player = player;

        if(_saver.TryGetValue(LevelProgressKey, out float value))
        {
            _distance = value;
        }
        else
        {
            _distance = Vector3.Distance(player.transform.position, _finishPosition.transform.position);
            _saver.Save(LevelProgressKey, _distance);
        }
    }

    private void Update()
    {
        if (_player == null)
            return;

        CurrentDistance = _maxDistance - Vector3.Distance(_player.transform.position, _finishPosition.transform.position) / _distance;
        _levelProgressView.UpdateProgressBar(CurrentDistance);
    }
}
