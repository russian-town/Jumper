using UnityEngine;

public class PlayerIK : MonoBehaviour
{
    [SerializeField] private SphereCollider _head;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layerMask;

    private PlayerAnimator _playerAnimator;

    public void Initialize(PlayerAnimator playerAnimator)
    {
        _playerAnimator = playerAnimator;
    }
}
