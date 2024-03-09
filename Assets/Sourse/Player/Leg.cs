using UnityEngine;

public class Leg : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _constantForce;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Rigidbody _pelvis;
    [SerializeField] private Transform _origin;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = new Ray(_origin.position, _origin.up);
            Debug.Log("Click");

            if (Physics.SphereCast(ray, _radius, out hit, Mathf.Infinity, _layerMask))
            {
                _pelvis.AddForce(hit.normal * _jumpForce);
                _pelvis.AddForce(_pelvis.transform.right * _constantForce);
                Debug.Log(hit.transform.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_origin.position, _origin.up * 10f);
    }
}
