﻿using Aiv.Fast2D;
using Aiv.Audio;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class PlayScene : Scene
    {
        private Background background;
        private Player player;

        public PlayScene() : base() { }

        public override void Start()
        {
            LoadAssets();
            LoadAudio();

            background = new Background();
            player = new Player();

            base.Start();
        }

        public override void OnExit()
        {
            player.Destroy();
            background.Destroy();

            player = null;
            background = null;

            base.OnExit();
        }

        public override void Input()
        {
            Quit();

            if (player.IsAlive)
            {
                player.Input();
            }
            else
            {
                IsPlaying = false;
            }
        }
        public override void Update()
        {
            if(player.IsAlive)
            {
                UpdateMgr.Update();
                PhysicsMgr.Update();

                Game.PlayerEarPosition = new Vector3(player.Position.X, player.Position.Y, 0.0f);

                //COLLISIONS
                PhysicsMgr.CheckCollision();
            }
            else
            {
                IsPlaying = false;
            }
        }
        public override void Draw()
        {
            if (player.IsAlive)
            {
                DrawMgr.Draw();
                //DebugMgr.Draw();
            }
            else
            {
                IsPlaying = false;
            }
        }

        private void LoadAssets()
        {
            GfxMgr.AddTexture("background", "Assets/Graphic/Background_Tiles/Background1.png");
            GfxMgr.AddTexture("player", "Assets/Graphic/Player/Character_SpriteSheet.png");
            GfxMgr.AddTexture("enemy", "Assets/Graphic/Enemy/Squelette_SpriteSheet.png");
            GfxMgr.AddTexture("hudMask", "Assets/Graphic/HUD/HUD_Mask.png");
            GfxMgr.AddTexture("hud", "Assets/Graphic/HUD/HUD.png");

        }
        private void LoadAudio()
        {
            AudioMgr.AddClip("coin", "Assets/Audio/Sound_Coin.ogg");
            AudioMgr.AddClip("sword", "Assets/Audio/Sound_sword_melee.ogg");
        }
    }
}
