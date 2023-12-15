using System;
using UnityEngine;

namespace _Scripts.Shooting
{
    public abstract class BaseGun : MonoBehaviour
    {
        public abstract void Shoot();

        [HideInInspector] public Camera playerCamera;
        public Transform shootingOrigin;
        public ParticleSystem shootParticle;
        public AudioClip shootAudio;
        public AudioSource audioSource;
        public float gunRange, fireRate, fireTimer, damage;
        public int bulletsAmount, bulletAmountReset;
        public GameObject bulletHole;


        private void Awake()
        {
            playerCamera = Camera.main;
        }

        private void Update()
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward * gunRange);
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * gunRange, Color.magenta);
        }

        public void AddBulletMark(RaycastHit hit)
        {
            print(hit.point.ToString());
            var bulletMark = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(bulletMark.gameObject, 5);
            bulletMark.transform.parent = hit.transform;
        }
    }
}