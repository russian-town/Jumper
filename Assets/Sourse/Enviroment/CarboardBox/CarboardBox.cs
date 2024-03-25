using System.Collections.Generic;
using UnityEngine;

namespace Sourse.Enviroment.CarboardBox
{
    [RequireComponent(typeof(BoxCollider))]
    public class CarboardBox : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _smokeEffects = new List<ParticleSystem>();
        [SerializeField] private List<GameObject> _boxes = new List<GameObject>();

        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        public void RemoveBoxes()
        {
            for (int i = 0; i < _boxes.Count; i++)
            {
                _boxes[i].SetActive(false);
                _smokeEffects[i].Play();
            }

            _boxCollider.enabled = false;
        }
    }
}
