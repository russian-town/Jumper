using UnityEngine;

public class Balancer : MonoBehaviour
{
    [SerializeField] private Transform _pelvis;

    private void Update()
    {
        transform.position = _pelvis.position;
    }
}
