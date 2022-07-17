using UnityEngine;
using WeaponNamespace;
using Player;
using Core;

namespace Projectiles.Buffs
{
    public class PotatoBuff : Buff
    {
        WeaponHandler weaponHandler;        


        public override void InitialiseBuff()
        {
            weaponHandler = GameManager.instance.Player.GetComponent<WeaponHandler>();
            weaponHandler.SetProjectile(2);
        }

        public override void UpdateBuff()
        {
            
        }

        public override void CleanUpBuff()
        {
            weaponHandler.SetProjectile(0);
        }

        public PotatoBuff(Sprite buffSprite, string name) : base(buffSprite, name)
        {
        }
    }
}