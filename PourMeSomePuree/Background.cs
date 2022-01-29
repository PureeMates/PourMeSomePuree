using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    class Background
    {
        private Texture texture;
        private Sprite sprite;

        public Background()
        {
            texture = new Texture("Assets/Background1.png");
            sprite = new Sprite(texture.Width, texture.Height);
            sprite.scale = new Vector2(0.67f);
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            sprite.position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
        }

        public void Draw()
        {
            sprite.DrawTexture(texture);
        }
    }
}
