using UnityEngine;

public class MassPositioner : MonoBehaviour
{
    [SerializeField] private Transform _centerOfMass;
    [SerializeField] private Rigidbody _pelvis;

    private void Start() => _pelvis.centerOfMass = _centerOfMass.position;
}
