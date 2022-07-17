using UnityEngine;

public class KOBuff : Buff
{
    private float currentHealth;
    private bool gameOver;

    public KOBuff(Sprite buffSprite, string name) : base(buffSprite, name)
    {
    }

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
}