using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    class Background : GameObject
    {
        public Background(string texturePath, int spriteWidth = 0, int spriteHeight = 0) : base(texturePath, spriteWidth, spriteHeight)
        {
            sprite.scale = new Vector2(0.67f);
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            sprite.position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);

            RigidBody = new RigidBody(this);
            RigidBody.Collider = CollidersFactory.CreateCircleInvertedFor(this);

            IsActive = true;
        }

        public void Draw()
        {
            sprite.DrawTexture(texture);
            sprite.DrawWireframe(50, 50, 50, 255);
        }

        public override void OnCollide(GameObject other)
        {
            if (other is Player)
            {
                Console.WriteLine("Background is colliding with player");
            }
        }
    }
}
