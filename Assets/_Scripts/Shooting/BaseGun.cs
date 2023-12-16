using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Scripts.Shooting
{
    public abstract class BaseGun : MonoBehaviour
    {
        public abstract void Shoot();

        [HideInInspector] public Camera playerCamera;
        public Transform shootingOrigin;
        public ParticleSystem shootParticle;
        public AudioClip shootAudio,recharging;
        public AudioClip[] outOfAmmo;
        public AudioSource audioSource;
        public float gunRange, fireRate, fireTimer, damage, reloadTime;
        public int bulletsLeft, magazineCapacity;
        public GameObject bulletHole;
        public bool isReloading;


        private void Awake()
        {
            playerCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineCapacity && !isReloading)
            {
                StartCoroutine(Reload(reloadTime));
                audioSource.clip = recharging;
                audioSource.Play();
            }

            if (bulletsLeft <= 0 && !isReloading)
            {
                audioSource.clip = outOfAmmo[Random.Range(0, outOfAmmo.Length)];
                audioSource.Play();
                StartCoroutine(Reload(reloadTime));
            }
        }

        public void AddBulletMark(RaycastHit hit)
        {
            print(hit.point.ToString());
            var bulletMark = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(bulletMark.gameObject, 5);
            bulletMark.transform.parent = hit.transform;
        }

        public IEnumerator Reload(float time)
        {
            isReloading = true;
            float timer = time;
            while ((timer -= Time.deltaTime) >= 0)
            {
                yield return null;
            }

            bulletsLeft = magazineCapacity;
            isReloading = false;
            if (CompareTag("Pistol"))
            {
                GameManager.pistolBulletsLeft = bulletsLeft;
            }
            else if (CompareTag("Rifle"))
            {
                GameManager.rifleBulletsLeft = bulletsLeft;
            }
            else if (CompareTag("Shotgun"))
            {
                GameManager.shotgunBulletsLeft = bulletsLeft;
            }
        }
    }
}