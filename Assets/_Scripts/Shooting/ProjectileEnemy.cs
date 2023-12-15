using System;
using UnityEngine;

namespace _Scripts.Shooting
{
    public class ProjectileEnemy : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy = 3f;
        public float damage = 1;
        private void Start()
        {
            Destroy(gameObject,timeToDestroy);
        }

        private void Update()
        {
            if (GameManager.gameTime / 20 > 1)
            {
                damage = GameManager.gameTime / 20;
            }
            else
            {
                damage = 1;
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}
