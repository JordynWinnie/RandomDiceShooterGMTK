using Player;
using UnityEngine;

namespace Projectiles.Buffs
{
    public class PotatoBuff : Buff
    {
        private WeaponHandler weaponHandler;

        public PotatoBuff(Sprite buffSprite, string name) : base(buffSprite, name)
        {
        }


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
    }
}