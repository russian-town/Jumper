using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offSet;
    [SerializeField] private Transform _targetPlayer;

    private Player _target;

    private void FixedUpdate()
    {
        //if (_targetPlayer == null)
        //    return;

        Vector3 targetPosition = new Vector3(_targetPlayer.position.x, _targetPlayer.position.y, transform.position.z) + new Vector3(_offSet.x, _offSet.y, 0f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
    }

    public void SetTarget(Player target)
    {
        _target = target;
    }
}
