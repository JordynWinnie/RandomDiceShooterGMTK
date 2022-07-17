using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponCatalogue", menuName = "ScriptableObjects/New Buff")]
public class BuffSO : ScriptableObject
{
    [SerializeField] public Buff CurrentBuff;
    [SerializeField] public Sprite sprite;
}
