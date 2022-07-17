using UnityEngine;

namespace Projectiles.Buffs
{
    public class ScoreMultiplier : Buff
    {
        private int currentScore = 0;

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

        public ScoreMultiplier(Sprite buffSprite, string name) : base(buffSprite, name)
        {
        }
    }
}