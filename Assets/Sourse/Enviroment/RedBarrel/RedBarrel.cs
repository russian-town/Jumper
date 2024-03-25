using Sourse.Enviroment.Common;
using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Enviroment.RedBarrel
{
    public class RedBarrel : BounceProps
    {
        [SerializeField] private ParticleSystem _explosionEffect;

        public void MakeExplosion()
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }

        protected override void Action(Collision collision, PlayerAnimator playerAnimator)
        {
            MakeExplosion();
            playerAnimator.Jump();

            Collider[] hitCollides = Physics.OverlapBox(transform.position, transform.localScale);

            if (hitCollides.Length > 0)
            {
                foreach (var hitCollider in hitCollides)
                {
                    if (hitCollider.TryGetComponent(out RedBarrel redBarrel))
                    {
                        redBarrel.MakeExplosion();
                    }
                }
            }
        }
    }
}
