using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TutorialAnimaton : MonoBehaviour
{
    private Animator _animator;

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
    }

    public void Clik(string key)
    {
        _animator.SetTrigger(key);
    }
}
