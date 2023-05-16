using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class GroundDetector : MonoBehaviour
{
    public event UnityAction<Collision> Fell;

    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layerMask;

    private BoxCollider _boxCollider;

    public void Initialize()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        IsGrounded();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Barrel barrel))
            return;

        Fell?.Invoke(collision);
    }

    public bool IsGrounded()
    {
        return Physics.BoxCast(_boxCollider.bounds.center, transform.localScale / 2, -transform.up, transform.rotation, _distance, _layerMask);
    }
}
