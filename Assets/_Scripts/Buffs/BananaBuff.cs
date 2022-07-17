using Player;
using UnityEngine;

namespace Projectiles.Buffs
{
    public class BananaBuff : Buff
    {
        private WeaponHandler weaponHandler;

        public BananaBuff(Sprite buffSprite, string name) : base(buffSprite, name)
        {
        }

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
    }
}