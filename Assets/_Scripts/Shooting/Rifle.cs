using UnityEngine;

namespace _Scripts.Shooting
{
    public class Rifle : BaseGun
    {
        public override void Shoot()
        {
           
            if (canShoot)
            {
                StartCoroutine(nameof(FireReset), fireRate);
                bulletsLeft = GameManager.rifleBulletsLeft;
                if (GameManager.rifleBulletsLeft <= 0) return;
                var shootEffect = Instantiate(shootParticle, shootingOrigin.position, Quaternion.identity);
                shootEffect.Play();
                audioSource.clip = shootAudio;
                audioSource.Play();
                GameManager.rifleBulletsLeft--;
                if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward * gunRange,
                        out var hit, gunRange)) return;
                AddBulletMark(hit);
                print(hit.transform.name);
                if (hit.transform.gameObject.CompareTag("enemy"))
                {
                    hit.transform.gameObject.GetComponent<BaseEnemy>().ApplyDamage(damage);
                }
            }
        }
    }
}