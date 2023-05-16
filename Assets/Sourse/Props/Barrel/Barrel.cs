using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Barrel : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private CapsuleCollider _capsuleCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _capsuleCollider.enabled = false;
        _rigidbody.isKinematic = true;

    }

    public void AddPhysics()
    {
        _capsuleCollider.enabled = true;
        _rigidbody.isKinematic = false;
    }
}
