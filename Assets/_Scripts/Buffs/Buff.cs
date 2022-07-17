using UnityEngine;

public abstract class Buff
{
    public Sprite BuffSprite;
    public string name;

    protected Buff(Sprite buffSprite, string name)
    {
        BuffSprite = buffSprite;
        this.name = name;
    }

    public abstract void InitialiseBuff();
    public abstract void UpdateBuff();
    public abstract void CleanUpBuff();
}