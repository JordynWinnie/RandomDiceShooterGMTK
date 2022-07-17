using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : Buff
{
    
    protected override void InitialiseBuff()
    {
        GameManager.instance.AddHealth(5f);
    }

    protected override void UpdateBuff()
    {

    }

    protected override void CleanUpBuff()
    {

    }

    public HealthBuff(Sprite buffSprite) : base(buffSprite)
    {
    }
}
