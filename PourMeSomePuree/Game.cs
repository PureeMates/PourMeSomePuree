﻿using System;
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

        public static Window Win { get { return window; } }
        public static float DeltaTime { get { return Win.DeltaTime; } }

        public static void Init()
        {
            window = new Window(1280, 720, "PourMeSomePuree");
            window.SetVSync(false);
            LoadAssets();
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
                player.Update();

                //COLLISIONS
                PhysicsMngr.CheckCollision();

                //DRAW
                background.Draw();
                player.Draw();

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
            GfxMgr.AddTexture("player", "Assets/Character_SpriteSheet.png");
            GfxMgr.AddTexture("enemy", "Assets/Squelette_SpriteSheet.png");
            GfxMgr.AddTexture("background", "Assets/Background1.png");
        }

    }
}
