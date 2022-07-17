using Projectiles.Buffs;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    [SerializeField] private Image[] BuffUIElements;
    [SerializeField] private Buff[] Buffs;
    [SerializeField] private Buff[] currentBuffs;

    private Buff currentBuff = null;
    // Start is called before the first frame update
    void Start()
    {
        Buffs = new Buff[]
        {
            new HealthBuff(Resources.Load<Sprite>("heart_sprite")),
            new SpeedBuff(Resources.Load<Sprite>("speed")),
            new ExplosiveBuff(Resources.Load<Sprite>("explosives")),
            new HeavyWeightBuff(Resources.Load<Sprite>("heavyweight")),
            new KOBuff(Resources.Load<Sprite>("death")),
            new ScoreMultiplier(Resources.Load<Sprite>("2")),
        };
        currentBuffs = new[] {
            Buffs[5],
            Buffs[5],
            Buffs[5],
            Buffs[5],
            Buffs[5],
            Buffs[5]
        };
        UpdateBuffUI();
    }

    void UpdateBuffUI()
    {
        for (int i = 0; i < currentBuffs.Length; i++)
        {
            BuffUIElements[i].sprite = currentBuffs[i].BuffSprite;
        }
    }

    public void ChangeBuff(int index)
    {
        if (currentBuff != null)
        {
            currentBuff.CleanUpBuff();
        }
        currentBuff = currentBuffs[index];
        currentBuff.InitialiseBuff();
    }
    // Update is called once per frame
    void Update()
    {
        if (currentBuff != null)
        {
            currentBuff.UpdateBuff();
        }
    }
}
