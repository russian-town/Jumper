using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Player _target;
    private Vector3 _offSet;

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        Vector3 targetPosition = new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z) + new Vector3(_offSet.x, _offSet.y, 0f); ;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
    }

    public void SetTarget(Player target)
    {
        _target = target;
        _offSet = transform.position - new Vector3(_target.transform.position.x, _target.transform.position.y, 0f);
    }
}
