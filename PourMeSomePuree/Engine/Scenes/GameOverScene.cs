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
        protected Sprite sprite;
        protected Texture texture;
        protected Sprite playAgain;
        protected Texture texturePlayAgain;

        public GameOverScene() : base()
        {
            LoadAssets();
            texture = GfxMgr.GetTexture("gameOver");
            texturePlayAgain = GfxMgr.GetTexture("playAgain");
            sprite = new Sprite(texture.Width, texture.Height);
            playAgain = new Sprite(texturePlayAgain.Width, texturePlayAgain.Height);
            playAgain.position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f + 150);
            playAgain.pivot = new Vector2(texturePlayAgain.Width * 0.5f, texturePlayAgain.Height * 0.5f);
        }

        public override void Input()
        {           
            if (Game.Win.GetKey(KeyCode.N))
            {
                Game.Win.Exit();
            }
            if (Game.Win.GetKey(KeyCode.Y))
            {
                
            }
        }
        public override void Update()
        {

        }
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
