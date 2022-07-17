using Core;
using UnityEngine;
using WeaponNamespace;

namespace Player
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private Transform weaponAnchor;
        [SerializeField] private WeaponCatalogueSO weaponCatalogue;

        [SerializeField] private int weaponEquipIndex; //Weapon to use
        [SerializeField] private int bulletEquipIndex; //Bullet to use
        public Weapon weaponScript;

        private GameObject weaponObject;

        private void Start()
        {
            EquipWeapon(weaponEquipIndex, bulletEquipIndex);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0)) weaponScript.Shoot();
        }

        public void EquipWeapon(int weaponID, int bulletID)
        {
            for (var i = 0; i < weaponAnchor.childCount; i++) Destroy(weaponAnchor.GetChild(0).gameObject);

            weaponObject = null;
            weaponScript = null;

            weaponObject = Instantiate(weaponCatalogue.GetWeapon(weaponID), weaponAnchor);
            weaponScript = weaponObject.GetComponent<Weapon>();

            weaponScript.bullet = AssetManager.instance.GetBulletPrefab(bulletID);
        }

        public void SetProjectile(int bulletID)
        {
            weaponScript.bullet = AssetManager.instance.GetBulletPrefab(bulletID);
        }
    }
}