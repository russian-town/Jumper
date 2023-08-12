using UnityEngine;

public class HeadPosition : MonoBehaviour
{
    public Vector3 Current => transform.position;

    public void Set(Vector3 position)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, position.y, -position.z);
    }
}
