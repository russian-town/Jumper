using UnityEngine;

public class RedBarrel : BounceProps
{
    [SerializeField] private ParticleSystem _explosionEffect;

    protected override void Action()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
