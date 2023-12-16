using UnityEngine;

namespace _Scripts.Shooting
{
    public class GunSystem : MonoBehaviour
    {
        [SerializeField] private BaseGun _firstWeapon;
        [SerializeField] private BaseGun _slot1Gun;
        [SerializeField] private BaseGun _slot2Gun;
        [SerializeField] private BaseGun _slot3Gun;

        [SerializeField] private AudioClip[] gun;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Transform _gunSocket;
        public BaseGun equippedGun;

        private void Start()
        {
            if (_firstWeapon == null)
                print("You need to add gun in Inspector");
            else
            {
                EquipGun(_firstWeapon);
                UIManager.uiManagerInstance.SetBullets(equippedGun.bulletsLeft);
            }
        }

        private void Update()
        {
            if (!GameManager.gameManagerInstance.isPaused)
            {

                UIManager.uiManagerInstance.SetBullets(equippedGun.bulletsLeft);
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    audioSource.clip = gun[0];
                    audioSource.Play();
                    EquipGun(_slot1Gun);
                    equippedGun.bulletsLeft = GameManager.pistolBulletsLeft;
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    audioSource.clip = gun[1];
                    audioSource.Play();
                    EquipGun(_slot2Gun);
                    equippedGun.bulletsLeft = GameManager.rifleBulletsLeft;
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    audioSource.clip = gun[2];
                    audioSource.Play();
                    EquipGun(_slot3Gun);
                    equippedGun.bulletsLeft = GameManager.shotgunBulletsLeft;
                }

                if (Input.GetKeyDown(KeyCode.Mouse0) && !equippedGun.isReloading && equippedGun.bulletsLeft > 0)
                {
                    ShootGunSystem();
                    //UIManager.uiManagerInstance.SetBullets(equippedGun.bulletsLeft);
                }
            }
        }

        public void EquipGun(BaseGun gun)
        {
            if (equippedGun != null)
            {
                Destroy(equippedGun.gameObject);
            }

            equippedGun = Instantiate(gun, _gunSocket.position, _gunSocket.rotation);
            equippedGun.transform.SetParent(_gunSocket);
        }

        public void ShootGunSystem()
        {
            equippedGun.Shoot();
        }
    }
}