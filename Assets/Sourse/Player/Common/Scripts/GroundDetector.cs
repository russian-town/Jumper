using UnityEngine;
using UnityEngine.Events;

public class GroundDetector : MonoBehaviour
{
    public event UnityAction<Collision> Fell;

    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private CheckGroundPosition _checkGroundPosition;

    public bool IsGrounded { get; private set; }

    private void FixedUpdate()
    {
        if (Physics.OverlapSphere(_checkGroundPosition.transform.position, _radius, _layerMask).Length > 0)
            IsGrounded = true;
        else
            IsGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Fell?.Invoke(collision);
    }
}
