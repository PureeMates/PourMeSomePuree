using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    static class PowerUpMgr
    {
        static List<PowerUp> powerUps;

        public static void Init()
        {
            powerUps = new List<PowerUp>();

            powerUps.Add(new PowerUpStamina());
            powerUps.Add(new InvulnerabilityPowerUp());
            powerUps.Add(new DamagePowerUp());

        }


        public static PowerUp GetPowerUp(Player player)
        {            
            if (powerUps.Count >= 1)
            {
                int index = RandomGenerator.GetRandomInt(0, powerUps.Count);
                PowerUp powerUp = powerUps[index];
                powerUps.RemoveAt(index);
                powerUp.owner = player;
                powerUp.IsActive = true;
                powerUp.Effect(player);
                return powerUp;
            }
            else
            {
                return null;
            }
        }
        
        public static void Restore(PowerUp powerUp)
        {
            powerUp.owner = null;
            powerUps.Add(powerUp);
        }

        public static void Clear()
        {
            powerUps.Clear();
        }

        
    }
}
