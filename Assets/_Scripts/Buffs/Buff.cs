using UnityEngine;

public abstract class Buff 
{
    protected Buff(Sprite buffSprite, string name)
    {
        this.BuffSprite = buffSprite;
        this.name = name;
    }
    public Sprite BuffSprite;
    public string name;
    public abstract void InitialiseBuff();
    public abstract void UpdateBuff();
    public abstract void CleanUpBuff();
}
