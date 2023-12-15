using UnityEngine;

namespace _Scripts.Shooting
{
    public class GunSystem : MonoBehaviour
    {
        [SerializeField] private BaseGun _firstWeapon;
        [SerializeField] private BaseGun _slot1Gun;
        [SerializeField] private BaseGun _slot2Gun;
        [SerializeField] private BaseGun _slot3Gun;

        [SerializeField] private Transform _gunSocket;
        public BaseGun equippedGun;

        private void Start()
        {
            if (_firstWeapon == null)
                print("You need to add gun in Inspector");
            else
            {
                EquipGun(_firstWeapon);
                UIManager.uiManagerInstance.SetBullets(equippedGun.bulletsAmount);
            }
        }

        private void Update()
        {
            if (!GameManager.gameManagerInstance.isPaused)
            {
                UIManager.uiManagerInstance.SetBullets(equippedGun.bulletsAmount);
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    EquipGun(_slot1Gun);
                    equippedGun.bulletsAmount = GameManager.pistolBulletsLeft;
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    EquipGun(_slot2Gun);
                    equippedGun.bulletsAmount = GameManager.rifleBulletsLeft;
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    EquipGun(_slot3Gun);
                    equippedGun.bulletsAmount = GameManager.shotgunBulletsLeft;
                }

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ShootGunSystem();
                    //UIManager.uiManagerInstance.SetBullets(equippedGun.bulletsAmount);
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