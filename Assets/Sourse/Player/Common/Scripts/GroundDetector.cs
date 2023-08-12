using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(BoxCollider))]
public class GroundDetector : MonoBehaviour
{
    public event UnityAction<Collision> Fell;

    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Head _head;

    private BoxCollider _boxCollider;
    private IGroundedHandler[] _groundedHandlers;

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

        Vector3 colliderPosition = collision.gameObject.transform.position;
        Quaternion colliderRotation = collision.gameObject.transform.rotation;
        float distance;
        Vector3 direction;

        bool canDetect = !Physics.ComputePenetration(collision.collider, colliderPosition, colliderRotation, _head.Collider, _head.Position, _head.Rotation, out direction, out distance);

        if (canDetect == false)
            return;

        if (collision.contacts.Length > 0f)
        {
            if (Vector3.Dot(collision.GetContact(0).normal, Vector3.up) > 0.5f)
            {
                Fell?.Invoke(collision);
            }
        }
    }

    private bool IsGrounded()
    {
        if (Physics.BoxCast(_boxCollider.bounds.center, transform.localScale / 2, -transform.up, out RaycastHit hit, transform.rotation, _distance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
