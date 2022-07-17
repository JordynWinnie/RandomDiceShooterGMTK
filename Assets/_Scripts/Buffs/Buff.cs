using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    [SerializeField] protected Sprite BuffSprite;
    protected abstract void InitialiseBuff();
    protected abstract void UpdateBuff();
    protected abstract void CleanUpBuff();
}
