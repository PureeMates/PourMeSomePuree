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
            Game.Win.SetMouseVisible(false);

            LoadAssets();
            LoadAudio();

            background = new Background();
            player = new Player();

            DoorMgr.Init(4, 5);
            //TODO - girare le porte nella direzione corretta
            DoorMgr.PutDoorAt(new Vector2(Game.Win.Width * 0.5f, 80 - 21.5f));
            DoorMgr.PutDoorAt(new Vector2(Game.Win.Width * 0.5f, Game.Win.Height - 80 + 21.5f));
            DoorMgr.PutDoorAt(new Vector2(80 - 21.5f, Game.Win.Height * 0.5f));
            DoorMgr.PutDoorAt(new Vector2(Game.Win.Width - 80 + 21.5f, Game.Win.Height * 0.5f));

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

            if (!player.IsAlive)
            {
                IsPlaying = false;
            }

            player.Input();
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
        }

        public override void Draw()
        {
            if (player.IsAlive)
            {
                DrawMgr.Draw();
                //DebugMgr.Draw();
            }
        }

        private void LoadAssets()
        {
            GfxMgr.AddTexture("background", "Assets/Graphic/Background_Tiles/Background1.png");

            GfxMgr.AddTexture("player", "Assets/Graphic/Player/Character_SpriteSheet.png");
            GfxMgr.AddTexture("enemy", "Assets/Graphic/Enemy/Squelette_SpriteSheet.png");     
            
            GfxMgr.AddTexture("hud", "Assets/Graphic/HUD/HUD.png");
            GfxMgr.AddTexture("portrait", "Assets/Graphic/HUD/Portrait.png");
            GfxMgr.AddTexture("hudMaskAvatar", "Assets/Graphic/HUD/HudMaskAvatar.png");
            GfxMgr.AddTexture("hudMaskEnergy", "Assets/Graphic/HUD/HudMaskEnergyBar.png");
            GfxMgr.AddTexture("hudMaskStamina", "Assets/Graphic/HUD/HudMaskStaminaBar.png");

            GfxMgr.AddTexture("coin", "Assets/Graphic/Objects/coinAnimation.png");
            GfxMgr.AddTexture("door", "Assets/Graphic/Objects/Door.png");
            GfxMgr.AddTexture("chest", "Assets/Graphic/Objects/Chest_SpriteSheet.png");
        }

        private void LoadAudio()
        {
            AudioMgr.AddClip("coin", "Assets/Audio/Sound_Coin.ogg");
            AudioMgr.AddClip("sword", "Assets/Audio/Sound_sword_melee.ogg");
        }
    }
}
