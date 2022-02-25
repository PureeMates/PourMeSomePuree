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
        protected GameObject button;
        protected Texture texture;
        protected Texture playButTexture;
        protected Sprite sprite;
        protected AudioClip audioClip;
        protected AudioSource audioSource;
        protected bool hover;
        public TitleScene() : base() { }

        public override void Start()
        {
            LoadAssets();
            hover = false;
            button = new GameObject("playButton1");
            button.RigidBody = new RigidBody(button);
            button.Position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
            button.RigidBody.Collider = CollidersFactory.CreateBoxFor(button);
            audioSource = new AudioSource();
            audioClip = AudioMgr.GetClip("sword");
            playButTexture = GfxMgr.GetTexture("playButton2");
            texture = GfxMgr.GetTexture("titleBackground");
            sprite = new Sprite(texture.Width, texture.Height);

            base.Start();
        }

        public override void OnExit()
        {
            texture = null;
            sprite = null;

            base.OnExit();
        }

        public override void Input()
        {
            if (((BoxCollider)button.RigidBody.Collider).Contains(Game.Win.MousePosition))
            {
                hover = true;
                if (Game.Win.MouseLeft)
                {
                    audioSource.Play(audioClip);
                    OnExit();
                }
            }
            else
            {
                hover = false;
            }
        }
        public override void Update()
        {
        }
        public override void Draw()
        {
            sprite.DrawTexture(texture);
            if (hover == true)
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
