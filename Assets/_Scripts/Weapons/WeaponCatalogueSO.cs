using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponNamespace
{
    [CreateAssetMenu(fileName = "WeaponCatalogue", menuName = "ScriptableObjects/New Weapon Catalogue")]
    public class WeaponCatalogueSO : ScriptableObject
    {
        [SerializeField] private GameObject[] weaponList;

        public GameObject GetWeapon(int weaponID)
        {
            foreach (GameObject weaponObject in weaponList)
            {
                Weapon rangedWeapon = weaponObject.GetComponent<Weapon>();
                if (rangedWeapon.WeaponID == weaponID)
                {
                    return weaponObject;
                }
            }

            Debug.LogError($"Could not find a weapon with the matching ID: {weaponID}");
            return null;
        }
    }
}