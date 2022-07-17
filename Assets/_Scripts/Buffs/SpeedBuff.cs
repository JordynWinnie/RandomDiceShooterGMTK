using UnityEngine;

public class SpeedBuff : Buff
{
    private float currentSpeed;

    public SpeedBuff(Sprite buffSprite, string name) : base(buffSprite, name)
    {
    }


    public override void InitialiseBuff()
    {
        currentSpeed = GameManager.instance._playerMovement.speed;
        GameManager.instance._playerMovement.speed *= 1.5f;
    }

    public override void UpdateBuff()
    {
    }

    public override void CleanUpBuff()
    {
        GameManager.instance._playerMovement.speed = currentSpeed;
    }
}