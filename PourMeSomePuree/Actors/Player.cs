using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    class Player : Actor
    {
        private bool isSwordPressed;

        private Vector2 offSett;


        public Player() : base ("Assets/Character_SpriteSheet.png", 64, 64)
        {
            
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
            maxSpeed = 200f;
            IsActive = true;
            RigidBody.Collider = new CircleCollider(RigidBody, isInverted: true);
            animation = new Animation(15, 4, 64, 64);
            offSett = new Vector2(0,0);

        }

        public void Input()
        {


            if (Game.Win.GetKey(KeyCode.D)) 
            {
                RigidBody.Velocity.X = maxSpeed;
            }
            else if (Game.Win.GetKey(KeyCode.A) && Position.X - sprite.Width * 0.5f >= 0)
            {
                RigidBody.Velocity.X = -maxSpeed;
            }
            else
            {
                RigidBody.Velocity.X = 0.0f;
            }

            if (Game.Win.GetKey(KeyCode.W))
            {
                RigidBody.Velocity.Y = -maxSpeed;
                offSett.Y = 64;               
                animation.Start();
            }
            else if (Game.Win.GetKey(KeyCode.S))
            {
                RigidBody.Velocity.Y = maxSpeed;
                offSett.Y = 0;
                animation.Start();
            }
            else
            {
                RigidBody.Velocity.Y = 0.0f;
                animation.Stop();
            }

            if (RigidBody.Velocity.X != 0 && RigidBody.Velocity.Y != 0)
            {
                RigidBody.Velocity = RigidBody.Velocity.Normalized() * maxSpeed;
            }

            if (Game.Win.GetKey(KeyCode.Space))
            {
                if (!isSwordPressed)
                {
                    isSwordPressed = true;
                    Sword();
                }
                else if (isSwordPressed)
                {
                    isSwordPressed = false;
                }
            }


        }

        public override void OnCollide(GameObject other)
        {
            
        }

        public override void Update()
        {
            RigidBody.Update();
            animation.Update();
        }

        public override void Draw()
        {
            sprite.DrawTexture(texture,(int)offSett.X + animation.XOffset,(int)offSett.Y + animation.YOffset, 64, 64);
        }


    }
}
