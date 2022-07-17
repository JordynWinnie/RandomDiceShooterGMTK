using UnityEngine;

[CreateAssetMenu(fileName = "WeaponCatalogue", menuName = "ScriptableObjects/New Buff")]
public class BuffSO : ScriptableObject
{
    [SerializeField] public Sprite sprite;
    [SerializeField] public Buff CurrentBuff;
}