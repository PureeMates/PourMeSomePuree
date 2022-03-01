using Aiv.Audio;
using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class TitleScene : Scene
    {
        private GameObject button;

        private Texture texture;
        private Texture playButTexture;
        private Sprite sprite;

        private AudioSource audioSource;
        private AudioClip audioClip;

        private bool isHovering;

        public TitleScene() : base() { }

        public override void Start()
        {
            Game.Win.SetMouseVisible(true);

            LoadAssets();

            button = new GameObject("playButton1");
            texture = GfxMgr.GetTexture("titleBackground");
            sprite = new Sprite(texture.Width, texture.Height);
            playButTexture = GfxMgr.GetTexture("playButton2");

            button.RigidBody = new RigidBody(button);
            button.Position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
            button.RigidBody.Collider = CollidersFactory.CreateBoxFor(button);

            audioSource = new AudioSource();
            audioClip = AudioMgr.GetClip("sword");

            isHovering = false;

            base.Start();
        }

        public override void OnExit()
        {
            button = null;
            texture = null;
            playButTexture = null;
            sprite = null;
            audioClip = null;
            audioSource = null;

            base.OnExit();
        }

        public override void Input()
        {
            Quit();

            if (((BoxCollider)button.RigidBody.Collider).Contains(Game.Win.MousePosition))
            {
                isHovering = true;
                if (Game.Win.MouseLeft)
                {
                    audioSource.Play(audioClip);
                    IsPlaying = false;
                }
            }
            else
            {
                isHovering = false;
            }
        }

        public override void Update() { }

        public override void Draw()
        {
            sprite.DrawTexture(texture);
            if (isHovering)
            {
                button.Draw(playButTexture);
            }
            else
            {
                button.Draw();
            }
        }

        private void LoadAssets()
        {
            GfxMgr.AddTexture("titleBackground", "Assets/Graphic/Background_Tiles/bfg2.png");
            GfxMgr.AddTexture("playButton1", "Assets/Graphic/Objects/PlayButton1.png");
            GfxMgr.AddTexture("playButton2", "Assets/Graphic/Objects/PlayButton2.png");

            AudioMgr.AddClip("sword", "Assets/Audio/Sound_sword_melee.ogg");
        }
    }
}
