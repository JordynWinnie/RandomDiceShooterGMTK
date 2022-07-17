using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class ExplosiveBuff : Buff
{
    private float currentRange = 0f;
    private WeaponHandler weaponHandler;

    public ExplosiveBuff(Sprite buffSprite) : base(buffSprite)
    {
    }

    public override void InitialiseBuff()
    {
        weaponHandler = GameManager.instance.Player.GetComponent<WeaponHandler>();
        currentRange = weaponHandler.weaponScript.explosionRange;
        weaponHandler.weaponScript.explosionRange += 5f;
    }

    public override void UpdateBuff()
    {
    }

    public override void CleanUpBuff()
    {
        weaponHandler.weaponScript.explosionRange = currentRange;
    }
}
