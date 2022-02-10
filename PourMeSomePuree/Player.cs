using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    class Player : GameObject
    {
        public Player(string texturePath, int spriteWidth = 0, int spriteHeight = 0) : base(texturePath, spriteWidth, spriteHeight)
        {
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
            maxSpeed = 500f;
            
            RigidBody = new RigidBody(this);
            RigidBody.Collider = CollidersFactory.CreateCircleFor(this);

            IsActive = true;
        }

        public void Input()                     //debug
        {                                       //debug
            if (Game.Win.GetKey(KeyCode.W))     //debug
            {                                   //debug
                sprite.position.Y -= maxSpeed * Game.DeltaTime;     //debug
            }
            else if (Game.Win.GetKey(KeyCode.S))                //debug
            {                                                  //debug
                sprite.position.Y += maxSpeed * Game.DeltaTime;//debug
            }                                   //debug
        }                                       //debug
        public override void OnCollide(GameObject other)
        {
            if (other is Background)
            {
                Console.WriteLine("Player is colliding with background");
            }
        }
    }
}
