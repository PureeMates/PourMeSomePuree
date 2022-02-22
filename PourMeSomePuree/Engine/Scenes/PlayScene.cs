using Aiv.Fast2D;
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
        private Pot pot;

        public PlayScene() : base() { }

        public override void Start()
        {
            LoadAssets();
            LoadAudio();

            background = new Background();
            pot = new Pot();
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

            player.Input();
        }
        public override void Update()
        {
            if(!player.IsAlive)
            {
                IsPlaying = false;
            }

            UpdateMgr.Update();
            PhysicsMgr.Update();

            Game.PlayerEarPosition = new Vector3(player.Position.X, player.Position.Y, 0.0f);

            //COLLISIONS
            PhysicsMgr.CheckCollision();
        }
        public override void Draw()
        {
            DrawMgr.Draw();
            //DebugMgr.Draw();
        }

        private void LoadAssets()
        {
            GfxMgr.AddTexture("background", "Assets/Graphic/Background_Tiles/Background1.png");
            GfxMgr.AddTexture("player", "Assets/Graphic/Player/Character_SpriteSheet.png");
            GfxMgr.AddTexture("enemy", "Assets/Graphic/Enemy/Squelette_SpriteSheet.png");
            GfxMgr.AddTexture("pot", "Assets/Graphic/Objects/Jar_SpriteSheet.png");
        }
        private void LoadAudio()
        {
            AudioMgr.AddClip("coin", "Assets/Audio/Sound_Coin.ogg");
            AudioMgr.AddClip("sword", "Assets/Audio/Sound_sword_melee.ogg");
        }
    }
}
