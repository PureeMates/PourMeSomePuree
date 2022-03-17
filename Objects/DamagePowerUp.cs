using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace PourMeSomePuree
{
    class DamagePowerUp : PowerUp
    {
        public DamagePowerUp() : base("powerUpDamage")
        {
            isVisible = false;
            IsActive = false;
            sprite.position = new Vector2(53.0f, 180.0f);
        }

        public  override void Effect(Player player)
        {
            owner = player;

            if (IsActive)
            {
                player.Damage = 100;
                isVisible = true;
            }
        }

        public override void EndPowerUp()
        {
            isVisible = false;
            owner.Damage = 50;
            PowerUpMgr.Restore(this);
        }


    }
}
