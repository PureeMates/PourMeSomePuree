using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    static class AnimationStorage
    {
        public static void LoadPlayerAnimations()
        {
            //when player is moving
            GfxMgr.AddAnimation("playerDown", 15, 4, 64, 64, 1, 1);
            GfxMgr.AddAnimation("playerUp", 15, 4, 64, 64, 1, 2);
            GfxMgr.AddAnimation("playerRight", 15, 4, 64, 64, 1, 3);
            GfxMgr.AddAnimation("playerLeft", 15, 4, 64, 64, 1, 4);

            //when Player is attacking
            GfxMgr.AddAnimation("playerAttackDown", 15, 4, 64, 64, 5, 1, false);
            GfxMgr.AddAnimation("playerAttackUp", 15, 4, 64, 64, 5, 2, false);
            GfxMgr.AddAnimation("playerAttackRight", 15, 4, 64, 64, 5, 3, false);
            GfxMgr.AddAnimation("playerAttackLeft", 15, 4, 64, 64, 5, 4, false);
        }

        public static void LoadEnemyAnimations(int id)
        {
            //when Enemy is moving
            GfxMgr.AddAnimation($"{id}enemyDown", 15, 4, 64, 64, 1, 1);
            GfxMgr.AddAnimation($"{id}enemyUp", 15, 4, 64, 64, 1, 2);
            GfxMgr.AddAnimation($"{id}enemyLeft", 15, 4, 64, 64, 1, 3);
            GfxMgr.AddAnimation($"{id}enemyRight", 15, 4, 64, 64, 1, 4);

            //when Enemy is attacking
            GfxMgr.AddAnimation($"{id}enemyAttackDown", 15, 4, 64, 64, 5, 1, false);
            GfxMgr.AddAnimation($"{id}enemyAttackUp", 15, 4, 64, 64, 5, 2, false);
            GfxMgr.AddAnimation($"{id}enemyAttackLeft", 15, 4, 64, 64, 5, 3, false);
            GfxMgr.AddAnimation($"{id}enemyAttackRight", 15, 4, 64, 64, 5, 4, false);
        }
    }
}
