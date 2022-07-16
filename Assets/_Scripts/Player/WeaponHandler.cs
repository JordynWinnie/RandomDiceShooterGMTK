using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponNamespace;

namespace Player
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private Transform weaponAnchor;
        [SerializeField] private WeaponCatalogueSO weaponCatalogue;

        private GameObject weaponObject;
        private Weapon weaponScript;

        private void Start()
        {
            EquipWeapon(0);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                weaponScript.Shoot();
            }
        }

        public void EquipWeapon(int weaponID)
        {
            for (int i = 0; i < weaponAnchor.childCount; i++)
            {
                Destroy(weaponAnchor.GetChild(0));
            }

            weaponObject = null;
            weaponScript = null;

            weaponObject = Instantiate(weaponCatalogue.GetWeapon(weaponID), weaponAnchor);
            weaponScript = weaponObject.GetComponent<Weapon>();
        }
    }
}