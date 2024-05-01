using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(Rigidbody), typeof(SurfaceSlider))]
    public class PhysicsMovement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private SurfaceSlider _surfaceSlider;

        public void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _surfaceSlider = GetComponent<SurfaceSlider>();
        }

        public void Move(Vector3 forward)
        {
            Vector3 target = _surfaceSlider.Project(forward, Vector3.zero);
        }
    }
}
