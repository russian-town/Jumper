using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Barrel : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void AddPhysics() => _rigidbody.isKinematic = false;
}
