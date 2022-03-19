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
        private Font font;
        private Player player;

        private Chest chest1;
        private Chest chest2;
        private Pot pot1;
        private Pot pot2;
        private Pot pot3;

        public int Level;

        public Font Font { get { return font; } }

        public override void Start()
        {
            Game.Win.SetMouseVisible(false);

            LoadAssets();
            LoadAudio();
            
            background = new Background();
            font = FontMgr.GetFont("stdFont");
            player = new Player();

            Level = 1;

            PowerUpMgr.Init();

            DoorMgr.Init(4, Level);
            DoorMgr.CreateAndPutDoorAt(new Vector2(Game.Win.Width * 0.5f, 80 - 21.5f));
            DoorMgr.CreateAndPutDoorAt(new Vector2(Game.Win.Width * 0.5f, Game.Win.Height - 80 + 21.5f));
            DoorMgr.CreateAndPutDoorAt(new Vector2(80 - 21.5f, Game.Win.Height * 0.5f));
            DoorMgr.CreateAndPutDoorAt(new Vector2(Game.Win.Width - 80 + 21.5f, Game.Win.Height * 0.5f));

            Vector2 chest1Pos = new Vector2(1140.0f, 136.0f);
            Vector2 chest2Pos = new Vector2(155.0f, 600.0f);
            Vector2 pot1Pos = new Vector2(1040.0f, 140.0f);
            Vector2 pot2Pos = new Vector2(130.0f, 125.0f);
            Vector2 pot3Pos = new Vector2(1145.0f, 550.0f);

            chest1 = new Chest(0);
            chest1.Position = chest1Pos;
            chest2 = new Chest(1);
            chest2.Position = chest2Pos;
            chest2.Sprite.Rotation = MathHelper.Pi;
            pot1 = new Pot(0, pot1Pos);
            pot2 = new Pot(0, pot2Pos);
            pot3 = new Pot(0, pot3Pos);

            base.Start();
        }

        public override void OnExit()
        {
            player.Destroy();
            background.Destroy();

            player = null;
            background = null;

            FontMgr.ClearAll();

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
                DoorMgr.Update();

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
            GfxMgr.AddTexture("pot", "Assets/Graphic/Objects/Jar_SpriteSheet.png");

            GfxMgr.AddTexture("powerUpStamina", "Assets/Graphic/PowerUp/icon_32.png");
            GfxMgr.AddTexture("powerUpDamage", "Assets/Graphic/PowerUp/icon_84.png");
            GfxMgr.AddTexture("powerUpInvulnerability", "Assets/Graphic/PowerUp/icon_86.png");

            FontMgr.AddFont("stdFont", "Assets/Graphic/Fonts/comics.png", 10, 32, 61, 65);
        }

        private void LoadAudio()
        {
            AudioMgr.AddClip("coin", "Assets/Audio/Sound_Coin.ogg");
            AudioMgr.AddClip("sword", "Assets/Audio/Sound_sword_melee.ogg");
        }
    }
}
