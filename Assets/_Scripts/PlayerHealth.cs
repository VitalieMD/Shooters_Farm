using System;
using _Scripts.Shooting;
using UnityEngine;

namespace _Scripts
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;
        [SerializeField] private AudioClip[] _clips;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        private void Start()
        {
            UIManager.uiManagerInstance.SetHealth(_currentHealth);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("bullet"))
            {
                TakeDamage(other.GetComponent<ProjectileEnemy>().damage);
                
            }
        }

        private void TakeDamage(float damageAmount)
        {
            _currentHealth -= damageAmount;

            if (_currentHealth <= 0)
            {
                UIManager.uiManagerInstance.GameLost();
            }
            else
            {
                UIManager.uiManagerInstance.SetHealth(_currentHealth);
            }
        }
    }
}