using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace PourMeSomePuree
{
    class GameOverScene : Scene
    {
        private Sprite sprite;
        private Texture texture;
        private Sprite playAgain;
        private Texture texturePlayAgain;

        public GameOverScene() : base() { }

        public override void Start()
        {
            Game.Win.SetMouseVisible(true);

            LoadAssets();

            texture = GfxMgr.GetTexture("gameOver");
            texturePlayAgain = GfxMgr.GetTexture("playAgain");
            
            sprite = new Sprite(texture.Width, texture.Height);
            playAgain = new Sprite(texturePlayAgain.Width, texturePlayAgain.Height);
            playAgain.position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f + 150.0f);
            playAgain.pivot = new Vector2(texturePlayAgain.Width * 0.5f, texturePlayAgain.Height * 0.5f);

            base.Start();
        }

        public override void OnExit()
        {
            texture = null;
            sprite = null;
            playAgain = null;
            texturePlayAgain = null;

            base.OnExit();
        }

        public override void Input()
        {
            if (Game.Win.GetKey(KeyCode.N))
            {
                Game.Win.Exit();
            }
            if (Game.Win.GetKey(KeyCode.Y))
            {
                IsPlaying = false;
            }
        }

        public override void Update() { }

        public override void Draw()
        {
            sprite.DrawTexture(texture);
            playAgain.DrawTexture(texturePlayAgain);
        }

        private void LoadAssets()
        {
            GfxMgr.AddTexture("gameOver", "Assets/Graphic/Background_Tiles/gameOver1.png");
            GfxMgr.AddTexture("playAgain", "Assets/Graphic/Background_Tiles/textPlayAgain2.png");
        }
    }
}
