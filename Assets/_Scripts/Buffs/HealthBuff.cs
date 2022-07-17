using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class HealthBuff : Buff
{
    
    public override void InitialiseBuff()
    {
        Debug.Log("Initialized Buff");
        GameManager.instance.AddHealth(5f);
    }

    public override void UpdateBuff()
    {
        Debug.Log("Updating Buff");
    }

    public override void CleanUpBuff()
    {
        Debug.Log("Cleanup Buff");
    }

    public HealthBuff(Sprite buffSprite) : base(buffSprite)
    {
    }
}
