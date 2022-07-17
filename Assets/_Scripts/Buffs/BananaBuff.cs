using Player;
using UnityEngine;

namespace Projectiles.Buffs
{
    public class BananaBuff : Buff
    {
        WeaponHandler weaponHandler;

        public override void InitialiseBuff()
        {
            weaponHandler = GameManager.instance.Player.GetComponent<WeaponHandler>();
            weaponHandler.SetProjectile(1);
        }

        public override void UpdateBuff()
        {
            
        }

        public override void CleanUpBuff()
        {
            weaponHandler.SetProjectile(0);
        }

        public BananaBuff(Sprite buffSprite, string name) : base(buffSprite, name)
        {
        }
    }
}