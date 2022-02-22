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
            GfxMgr.AddAnimation("down", 15, 4, 64, 64, 1, 1);
            GfxMgr.AddAnimation("up", 15, 4, 64, 64, 1, 2);
            GfxMgr.AddAnimation("right", 15, 4, 64, 64, 1, 3);
            GfxMgr.AddAnimation("left", 15, 4, 64, 64, 1, 4);

            //When Player stop
            GfxMgr.AddAnimation("idleDown", 15, 1, 64, 64, 1, 1);
            GfxMgr.AddAnimation("idleUp", 15, 1, 64, 64, 1, 2);
            GfxMgr.AddAnimation("idleRight", 15, 1, 64, 64, 1, 3);
            GfxMgr.AddAnimation("idleLeft", 15, 1, 64, 64, 1, 4);

            //when Player is attacking
            GfxMgr.AddAnimation("attackDown", 15, 4, 64, 64, 5, 1, false);
            GfxMgr.AddAnimation("attackUp", 15, 4, 64, 64, 5, 2, false);
            GfxMgr.AddAnimation("attackRight", 15, 4, 64, 64, 5, 3, false);
            GfxMgr.AddAnimation("attackLeft", 15, 4, 64, 64, 5, 4, false);
        }

        public static void LoadEnemyAnimations()
        {

        }
        public static void LoadPotAnimations()
        {
            GfxMgr.AddAnimation("breaking", 15, 4, 64, 64, 1, 1, false);
        }

    }
}
