using UnityEngine;

public abstract class Buff 
{
    protected Buff(Sprite buffSprite)
    {
        this.BuffSprite = buffSprite;
    }
    public Sprite BuffSprite;
    public abstract void InitialiseBuff();
    public abstract void UpdateBuff();
    public abstract void CleanUpBuff();
}
