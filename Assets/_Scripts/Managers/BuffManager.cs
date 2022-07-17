using System.Collections.Generic;
using Projectiles.Buffs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    [SerializeField] private Image[] BuffUIElements;
    [SerializeField] private RectTransform BuffUI;
    [SerializeField] private Image CurrentBuffUI;
    [SerializeField] private Buff[] Buffs;
    private TextMeshProUGUI buffText;
    private Buff currentBuff;

    [SerializeField] private List<Buff> currentBuffs;

    // Start is called before the first frame update
    private void Start()
    {
        buffText = BuffUI.GetComponent<TextMeshProUGUI>();
        Buffs = new Buff[]
        {
            new HealthBuff(Resources.Load<Sprite>("heart_sprite"), "+Health"),
            new SpeedBuff(Resources.Load<Sprite>("speed"), "+Speed"),
            new ExplosiveBuff(Resources.Load<Sprite>("explosives"), "BOOM BOOM"),
            new HeavyWeightBuff(Resources.Load<Sprite>("heavyweight"), "Slow! :("),
            new KOBuff(Resources.Load<Sprite>("death"), "Don't get touched!"),
            new ScoreMultiplier(Resources.Load<Sprite>("2"), "2x Score!"),
            new BananaBuff(Resources.Load<Sprite>("banana"), "Banana??"),
            new PotatoBuff(Resources.Load<Sprite>("potato"), "poTato??!")
        };


        currentBuffs = new List<Buff>
        {
            Buffs[0],
            Buffs[1],
            Buffs[2],
            Buffs[3],
            Buffs[6],
            Buffs[7]
        };

        UpdateBuffUI();
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentBuff != null) currentBuff.UpdateBuff();
    }

    private void UpdateBuffUI()
    {
        for (var i = 0; i < currentBuffs.Count; i++) BuffUIElements[i].sprite = currentBuffs[i].BuffSprite;
    }

    public void ChangeBuff(int index)
    {
        if (currentBuff != null) currentBuff.CleanUpBuff();
        currentBuff = currentBuffs[index];
        CurrentBuffUI.gameObject.SetActive(true);
        CurrentBuffUI.sprite = currentBuff.BuffSprite;
        buffText.SetText(currentBuff.name);
        GameManager.instance.AbilityChimeSound();
        LeanTween.scale(BuffUI, Vector3.one, 1f).setOnComplete(
            () => { LeanTween.scale(BuffUI, Vector3.zero, 1f); }
        );

        currentBuff.InitialiseBuff();
    }
}