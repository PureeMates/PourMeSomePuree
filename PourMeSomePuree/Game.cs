using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    static class Game
    {
        private static Window window;
        private static Background background;
        private static Player player;
        private static AudioMgr bgMusic;

        public static Window Win { get { return window; } }
        public static float DeltaTime { get { return Win.DeltaTime; } }

        public static void Init()
        {
            window = new Window(1280, 720, "PourMeSomePuree");
            window.SetVSync(false);
            LoadAssets();
            background = new Background();
            player = new Player();
            bgMusic = new AudioMgr();
        }
        
        public static void Play()
        {
            while (window.IsOpened)
            {
                window.SetTitle($"FPS: {1.0f / DeltaTime}");

                //INPUT
                Quit();
                player.Input();
                //bgMusic.Input();

                //UPDATE
                UpdateMgr.Update();
                PhysicsMgr.Update();


                //COLLISIONS
                PhysicsMgr.CheckCollision();

                //DRAW
                DrawMgr.Draw();
                DebugMgr.Draw();

                window.Update();
            }
        }

        private static void Quit()
        {
            if(window.GetKey(KeyCode.Esc))
            {
                window.Exit();
            }
        }

        private static void LoadAssets()
        {
            GfxMgr.AddTexture("background", "Assets/Background1.png");
            GfxMgr.AddTexture("player", "Assets/Character_SpriteSheet.png");
            GfxMgr.AddTexture("enemy", "Assets/Squelette_SpriteSheet.png");

            //when player is moving
            GfxMgr.AddAnimation("Down", 15, 4, 64, 64, 1, 1);
            GfxMgr.AddAnimation("Up", 15, 4, 64, 64, 1, 2);
            GfxMgr.AddAnimation("Right", 15, 4, 64, 64, 1, 3);
            GfxMgr.AddAnimation("Left", 15, 4, 64, 64, 1, 4);
            GfxMgr.AddAnimation("Attack", 15, 4, 64, 64, 5, 1);

            //When Player stop
            GfxMgr.AddAnimation("IdleDown", 15, 1, 64, 64, 1, 1);
            GfxMgr.AddAnimation("IdleUp", 15, 1, 64, 64, 1, 2);
            GfxMgr.AddAnimation("IdleRight", 15, 1, 64, 64, 1, 3);
            GfxMgr.AddAnimation("IdleLeft", 15, 1, 64, 64, 1, 4);

            //when Player is attacking
            GfxMgr.AddAnimation("AttackDown", 15, 4, 64, 64, 5, 1);
            GfxMgr.AddAnimation("AttackUp", 15, 4, 64, 64, 5, 2);
            GfxMgr.AddAnimation("AttackRight", 15, 4, 64, 64, 5, 3);
            GfxMgr.AddAnimation("AttackLeft", 15, 4, 64, 64, 5, 4);

            

        }

    }
}
