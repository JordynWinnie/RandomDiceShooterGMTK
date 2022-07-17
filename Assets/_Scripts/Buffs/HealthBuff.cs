using UnityEngine;

public class HealthBuff : Buff
{
    public HealthBuff(Sprite buffSprite, string name) : base(buffSprite, name)
    {
    }

    public override void InitialiseBuff()
    {
        Debug.Log("Initialized Buff");
        GameManager.instance.AddHealth(5f);
    }

    public override void UpdateBuff()
    {
    }

    public override void CleanUpBuff()
    {
        Debug.Log("Cleanup Buff");
    }
}