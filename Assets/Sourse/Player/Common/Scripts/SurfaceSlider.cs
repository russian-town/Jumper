using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class SurfaceSlider : MonoBehaviour
    {
        public Vector3 Project(Vector3 forward, Vector3 normal)
        {
            return forward - Vector3.Dot(forward, normal) * normal;
        }
    }
}
