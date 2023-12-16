using UnityEngine;

namespace _Scripts.Shooting
{
    public class Shotgun : BaseGun
    {
        public override void Shoot()
        {
            bulletsLeft = GameManager.shotgunBulletsLeft;
            if (GameManager.shotgunBulletsLeft <= 0) return;
            var shootEffect = Instantiate(shootParticle, shootingOrigin.position, Quaternion.identity);
            shootEffect.Play();
            AudioSource.PlayClipAtPoint(shootAudio, shootingOrigin.position);
            GameManager.shotgunBulletsLeft--;
            //UIManager.uiManagerInstance.SetBullets(GameManager.shotgunBulletsLeft);
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