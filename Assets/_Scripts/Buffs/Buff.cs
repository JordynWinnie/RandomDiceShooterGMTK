using UnityEngine;

public abstract class Buff 
{
    protected Buff(Sprite buffSprite)
    {
        this.BuffSprite = buffSprite;
    }
    public Sprite BuffSprite;
    protected abstract void InitialiseBuff();
    protected abstract void UpdateBuff();
    protected abstract void CleanUpBuff();
}
