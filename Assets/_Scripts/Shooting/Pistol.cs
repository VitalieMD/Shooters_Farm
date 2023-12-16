using UnityEngine;

namespace _Scripts.Shooting
{
    public class Pistol : BaseGun
    {
        public override void Shoot()
        {
            bulletsLeft = GameManager.pistolBulletsLeft;
            if (GameManager.pistolBulletsLeft <= 0) return;
            var shootEffect = Instantiate(shootParticle, shootingOrigin.position, Quaternion.identity);
            shootEffect.Play();
            audioSource.clip = shootAudio;
            audioSource.Play();
            GameManager.pistolBulletsLeft--;
            //UIManager.uiManagerInstance.SetBullets(GameManager.pistolBulletsLeft);
            if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward * gunRange,
                    out var hit, gunRange)) return;
            AddBulletMark(hit);
            if (hit.transform.gameObject.CompareTag("enemy"))
            {
                hit.transform.gameObject.GetComponent<BaseEnemy>().ApplyDamage(damage);
            }
        }
    }
}