using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOBuff : Buff
{
    private float currentHealth = 0f;
    private bool gameOver = false;

    public override void InitialiseBuff()
    {
        currentHealth = GameManager.instance._playerController.currentHealth;
    }

    public override void UpdateBuff()
    {
        if (gameOver) return;
        if (currentHealth != GameManager.instance._playerController.currentHealth)
        {
            GameManager.instance.GameOver();
            gameOver = true;
            currentHealth = -1;
        }
    }

    public override void CleanUpBuff()
    {
        
    }

    public KOBuff(Sprite buffSprite, string name) : base(buffSprite, name)
    {
    }
}
