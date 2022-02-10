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
        private int width;
        private int height;

        public Background() : base("background")
        {
            width = 1070;
            height = 507;

            sprite.scale = new Vector2(0.67f);
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            sprite.position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);

            RigidBody = new RigidBody(this);
            RigidBody.Collider = CollidersFactory.CreateInvertedBoxFor(this, width, height);
            RigidBody.Collider.Offset = new Vector2(sprite.Width * 0.5f - ((BoxColliderInverted)RigidBody.Collider).HalfWidth, sprite.Height * 0.5f - ((BoxColliderInverted)RigidBody.Collider).HalfHeight);

            IsActive = true;
        }

        public void Draw()
        {
            sprite.DrawTexture(texture);
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
