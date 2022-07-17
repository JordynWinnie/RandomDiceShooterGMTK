using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    [SerializeField] private Image[] BuffUIElements;
    [SerializeField] private Buff[] Buffs;

    [SerializeField] private Buff[] currentBuffs;
    // Start is called before the first frame update
    void Start()
    {
        Buffs = new Buff[]
        {
            new HealthBuff(Resources.Load<Sprite>("heart_sprite"))
        };
        currentBuffs = new[] {
            Buffs[0]
        };
        UpdateBuffs();
    }

    void UpdateBuffs()
    {
        for (int i = 0; i < currentBuffs.Length; i++)
        {
            BuffUIElements[i].sprite = currentBuffs[i].BuffSprite;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
