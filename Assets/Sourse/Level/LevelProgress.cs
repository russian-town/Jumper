using UnityEngine;

[RequireComponent(typeof(LevelProgressView))]
public class LevelProgress : MonoBehaviour
{
    [SerializeField] private FinishPosition _finishPosition;

    private LevelProgressView _levelProgressView;
    private float _distance;
    private Player _player;

    public float CurrentDistance { get; private set; }

    private void Awake()
    {
        _levelProgressView = GetComponent<LevelProgressView>();
    }

    public void Initialize(Player player)
    {
        _player = player;
        _distance = Vector3.Distance(_player.transform.position, _finishPosition.transform.position);
    }

    private void Update()
    {
        if (_player == null || CurrentDistance < 0)
            return;

        CurrentDistance = 1f - Vector3.Distance(_player.transform.position, _finishPosition.transform.position) / _distance;
        _levelProgressView.UpdateProgressBar(CurrentDistance);
    }
}
