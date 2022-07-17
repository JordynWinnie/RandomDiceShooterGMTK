using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyWeightBuff : Buff
{
    private float currentSpeed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public HeavyWeightBuff(Sprite buffSprite) : base(buffSprite)
    {
    }

    public override void InitialiseBuff()
    {
        currentSpeed = GameManager.instance._playerMovement.speed;
        GameManager.instance._playerMovement.speed *= 0.5f;
    }

    public override void UpdateBuff()
    {
    }

    public override void CleanUpBuff()
    {
        GameManager.instance._playerMovement.speed = currentSpeed;
    }
}
