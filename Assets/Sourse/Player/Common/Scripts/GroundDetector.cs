using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class GroundDetector : MonoBehaviour
{
    public event UnityAction<Collision> Fell;

    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private SphereCollider _head;

    private BoxCollider _boxCollider;
    private IGroundedHandler[] _groundedHandlers;
    private bool _canDetect = true;

    public void Initialize(IGroundedHandler[] groundedHandlers)
    {
        _boxCollider = GetComponent<BoxCollider>();
        _groundedHandlers = groundedHandlers;
    }

    private void FixedUpdate()
    {
        foreach (var groundedHandler in _groundedHandlers)
        {
            groundedHandler.SetGrounded(IsGrounded());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Barrel barrel))
            return;

        _canDetect = !Physics.ComputePenetration(_head, _head.transform.position, _head.transform.rotation, collision.collider, collision.transform.position, collision.transform.rotation, out Vector3 direction, out float distance);

        if (Vector3.Dot(collision.GetContact(0).normal, Vector3.up) > 0.5f && _canDetect)
        {
            Fell?.Invoke(collision);
        }
    }

    private bool IsGrounded()
    {
        if (Physics.BoxCast(_boxCollider.bounds.center, transform.localScale / 2, -transform.up, out RaycastHit hit, transform.rotation, _distance, _groundLayerMask, QueryTriggerInteraction.Ignore))
        {
            if (Vector3.Dot(hit.normal, transform.up) > 0.5f)
            {
                return true;
            }
        }

        return false;
    }
}
