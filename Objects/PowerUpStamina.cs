using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace PourMeSomePuree
{
    class PowerUpStamina : PowerUp
    {               
        public PowerUpStamina() : base("powerUpStamina")
        {
            isVisible = false;
            IsActive = false;
            sprite.position = new Vector2(53.0f, 130.0f);
        }

        public override void Effect(Player player)
        {
            owner = player;

            player.StaminaAttCost = 0;
            isVisible = true;

        }

        public override void EndPowerUp()
        {
            owner.StaminaAttCost = 25;
            isVisible = false;
            PowerUpMgr.Restore(this);
        }
    }
}
