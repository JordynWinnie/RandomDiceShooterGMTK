using UnityEngine;

namespace Projectiles.Buffs
{
    public class ScoreMultiplier : Buff
    {
        private float currentScore = 0f;
        public ScoreMultiplier(Sprite buffSprite) : base(buffSprite)
        {
        }

        public override void InitialiseBuff()
        {
            currentScore = GameManager.instance._score;
        }

        public override void UpdateBuff()
        {
            if (currentScore != GameManager.instance._score)
            {
                var difference = GameManager.instance._score - currentScore;
                GameManager.instance.AddScore(difference);
                currentScore = GameManager.instance._score;
            }
        }

        public override void CleanUpBuff()
        {
            
        }
    }
}