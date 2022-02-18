using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;
using Aiv.Audio;

namespace PourMeSomePuree
{
    static class Game
    {
        private static Window window;
        private static Background background;
        private static Player player;
        private static AudioDevice playerEar;

        public static Window Win { get { return window; } }
        public static float DeltaTime { get { return Win.DeltaTime; } }

        public static void Init()
        {
            window = new Window(1280, 720, "PourMeSomePuree");
            window.SetVSync(false);

            LoadAssets();
            LoadAudio();

            background = new Background();
            player = new Player();
        }
        
        public static void Play()
        {
            while (window.IsOpened)
            {
                window.SetTitle($"FPS: {1.0f / DeltaTime}");

                //INPUT
                Quit();
                player.Input();

                //UPDATE
                UpdateMgr.Update();
                PhysicsMgr.Update();

                playerEar.Position = new Vector3(player.Position.X, player.Position.Y, 0.0f);

                //COLLISIONS
                PhysicsMgr.CheckCollision();

                //DRAW
                DrawMgr.Draw();
                //DebugMgr.Draw();

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
            GfxMgr.AddTexture("background", "Assets/Graphic/Background_Tiles/Background1.png");
            GfxMgr.AddTexture("player", "Assets/Graphic/Player/Character_SpriteSheet.png");
            GfxMgr.AddTexture("enemy", "Assets/Graphic/Enemy/Squelette_SpriteSheet.png");
        }
        private static void LoadAudio()
        {
            playerEar = new AudioDevice();

            AudioMgr.AddClip("background", "Assets/Audio/Horde_theme.ogg");
            AudioMgr.AddClip("coin", "Assets/Audio/Sound_Coin.ogg");
            AudioMgr.AddClip("sword", "Assets/Audio/Sound_sword_melee.ogg");
        }
    }
}
