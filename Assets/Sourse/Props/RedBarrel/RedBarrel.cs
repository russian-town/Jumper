using UnityEngine;

public class RedBarrel : BounceProps
{
    [SerializeField] private ParticleSystem _explosionEffect;

    protected override void Action()
    {
        MakeExplosion();

        Collider[] hitCollides = Physics.OverlapBox(transform.position, transform.localScale);

        if (hitCollides.Length > 0)
        {
            foreach (var hitCollider in hitCollides)
            {
                if(hitCollider.TryGetComponent(out RedBarrel redBarrel))
                {
                    redBarrel.MakeExplosion();
                }
            }
        }
    }

    public void MakeExplosion()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
