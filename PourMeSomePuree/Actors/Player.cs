using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    enum DirectionType { DOWN = 0, UP = 64, RIGHT = 128, LEFT = 192 }
    enum ActionType { MOVEMENT = 0, ATTACK = 256 }
    class Player : Actor
    {
        private bool isSwordPressed;
        private bool isAttacking;
        private Vector2 offSet;
        private Animation attackAnimation;
        public Player() : base("player", 64, 64)
        {
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
            maxSpeed = 200f;
            IsActive = true;
            RigidBody.Collider = CollidersFactory.CreateCircleFor(this);
            offSet = new Vector2(0, 0);

            animation = new Animation(15, 4, 64, 64, loop: false);
            attackAnimation = new Animation(15, 4, 64, 64, loop: false);
        }

        public void Input()
        {
            if (Game.Win.GetKey(KeyCode.D))
            {
                RigidBody.Velocity.X = maxSpeed;
                offSet.X = (int)ActionType.MOVEMENT;
                offSet.Y = (int)DirectionType.RIGHT;
                animation.Start();
            }
            else if (Game.Win.GetKey(KeyCode.A))
            {
                RigidBody.Velocity.X = -maxSpeed;
                offSet.X = (int)ActionType.MOVEMENT;
                offSet.Y = (int)DirectionType.LEFT;
                animation.Start();
            }
            else
            {
                RigidBody.Velocity.X = 0.0f;
            }

            if (Game.Win.GetKey(KeyCode.W))
            {
                RigidBody.Velocity.Y = -maxSpeed;
                offSet.X = (int)ActionType.MOVEMENT;
                offSet.Y = (int)DirectionType.UP;
                animation.Start();
            }
            else if (Game.Win.GetKey(KeyCode.S))
            {
                RigidBody.Velocity.Y = maxSpeed;
                offSet.X = (int)ActionType.MOVEMENT;
                offSet.Y = (int)DirectionType.DOWN;
                animation.Start();
            }
            else
            {
                RigidBody.Velocity.Y = 0.0f;
            }

            if (RigidBody.Velocity.X != 0 && RigidBody.Velocity.Y != 0)
            {
                RigidBody.Velocity = RigidBody.Velocity.Normalized() * maxSpeed;
            }

            if (Game.Win.GetKey(KeyCode.Space))
            {
                offSet.X = (int)ActionType.ATTACK;

                if (!isSwordPressed)
                {
                    isSwordPressed = true;
                    Sword();
                    attackAnimation.Start();
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
            sprite.DrawTexture(texture, (int)offSet.X + animation.XOffset, (int)offSet.Y + animation.YOffset, 64, 64);
        }

        public override void Sword()
        {

        }

        //public bool CanAttack()
        //{
        //    if (attackCounter <= 0)
        //    {
        //        attackCounter = 1000f;
        //        return true;
        //    }
        //    return false;
        //}
    }
}
