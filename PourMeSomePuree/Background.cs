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
        public Background() : base("background")
        {
            sprite.scale = new Vector2(0.67f);
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            sprite.position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
            IsActive = true;
            RigidBody = new RigidBody(this);
            RigidBody.Collider = new CircleCollider(RigidBody, isInverted : false);
        }

        public void Draw()
        {
            sprite.DrawTexture(texture);
        }

        public override void OnCollide(GameObject other)
        {
            if (other is Player)
            {
                Console.WriteLine("COLLIDE");
            }
        }
    }
}
