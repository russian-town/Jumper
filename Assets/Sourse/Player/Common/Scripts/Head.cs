using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Head : MonoBehaviour
{
    [SerializeField] HeadPosition _headPosition;
    [SerializeField] private float _maxDistance = 1f;
    [SerializeField] private LayerMask _headMask;
    [SerializeField] private LayerMask _wallMask;

    private PlayerIK _playerIK;
    private SphereCollider _sphereCollider;
    private Vector3 _normal;

    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
    public SphereCollider Collider => _sphereCollider;

    public void Initialize(PlayerIK playerIK)
    {
        _playerIK = playerIK;
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void FixedUpdate()
    {
        if (_sphereCollider == null || _playerIK == null)
            return;

        if(Physics.SphereCast(transform.position + _sphereCollider.center, _sphereCollider.radius, transform.right, out RaycastHit raycastHit, _maxDistance))
        {
            if(raycastHit.collider)
            {
                _normal = raycastHit.normal;
                Vector3 direction = Vector3.Reflect(transform.right, _normal);
                Vector3 point = new Vector3(0f, transform.position.y, 0f) + new Vector3(0f, direction.y, 0f);
                _headPosition.Set(point);
                _playerIK.Enable();
            }
            else
            {
                _playerIK.Disable();
            }
        }
    }

    public void Disable() => enabled = false;
}
