using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private SphereCollider _head;

    private readonly float _threshold = .5f;

    private BoxCollider _boxCollider;
    private IGroundedHandler[] _groundedHandlers;
    private bool _canDetect = true;

    public event Action<Collision> Fell;

    private void FixedUpdate()
    {
        foreach (var groundedHandler in _groundedHandlers)
            groundedHandler.SetGrounded(IsGrounded());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Barrel barrel))
            return;

        _canDetect = !Physics.ComputePenetration
            (
            _head,
            _head.transform.position,
            _head.transform.rotation,
            collision.collider,
            collision.transform.position,
            collision.transform.rotation,
            out Vector3 direction,
            out float distance
            );

        if (Vector3.Dot(collision.GetContact(0).normal, Vector3.up) > _threshold && _canDetect)
            Fell?.Invoke(collision);
    }

    public void Initialize(IGroundedHandler[] groundedHandlers)
    {
        _boxCollider = GetComponent<BoxCollider>();
        _groundedHandlers = groundedHandlers;
    }

    private bool IsGrounded()
    {
        Vector3 halfScale = transform.localScale / 2f;

        if (Physics.BoxCast(_boxCollider.bounds.center, halfScale, -transform.up, out RaycastHit hit, transform.rotation, _distance, _groundLayerMask, QueryTriggerInteraction.Ignore))
        {
            if (Vector3.Dot(hit.normal, transform.up) > _threshold)
            {
                return true;
            }
        }

        return false;
    }
}
