using UnityEngine;

public class RagdollJumper : MonoBehaviour
{
    [SerializeField] private Animator _animationCharacter;
    [SerializeField] private Rigidbody _pelvis;
    [SerializeField] private float _force;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            //_animationCharacter.SetTrigger("DoubleJump");
            _pelvis.AddForce(Vector3.up * _force);
            _animationCharacter.SetTrigger("Jump");
        }
    }
}
