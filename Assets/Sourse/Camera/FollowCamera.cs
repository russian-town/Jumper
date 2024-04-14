using UnityEngine;

namespace Sourse.Camera
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Vector3 _offSet;

        private Transform _target;

        private void Update()
        {
            if (_target == null)
                return;

            Vector3 targetPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            Vector3 offSet = new Vector3(_offSet.x, _offSet.y, 0f);
            Vector3 followPosition = targetPosition + offSet;
            transform.position = Vector3.Lerp(transform.position, followPosition, _speed * Time.deltaTime);
        }

        public void SetTarget(Player.Common.Scripts.PlayerInitializer player)
        {
            _target = player.transform;
        }
    }
}
