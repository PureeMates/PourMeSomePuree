using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    enum Direction { DOWN, UP, RIGHT, LEFT }

    class Player : Actor
    {
        private Direction direction;

        public Player() : base("player", 64, 64)
        {
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
            maxSpeed = 200.0f;
            IsActive = true;
            RigidBody.Collider = CollidersFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.Player;
            RigidBody.AddCollisionType(RigidBodyType.Background);
            animation = GfxMgr.GetAnimation("IdleDown");
            direction = Direction.DOWN;
        }

        public void Input()
        {
            if (Game.Win.GetKey(KeyCode.D) || Game.Win.GetKey(KeyCode.Right))
            {
                if (isAttacking)
                {
                    return;
                }

                MovingRight();
            }
            else if (Game.Win.GetKey(KeyCode.A) || Game.Win.GetKey(KeyCode.Left))
            {
                if (isAttacking)
                {
                    return;
                }

                MovingLeft();
            }
            else
            {
                RigidBody.Velocity.X = 0.0f;

                if (direction == Direction.RIGHT)
                {
                    animation = GfxMgr.GetAnimation("IdleRight");
                }
                else if (direction == Direction.LEFT)
                {
                    animation = GfxMgr.GetAnimation("IdleLeft");
                }
            }

            if (Game.Win.GetKey(KeyCode.W) || Game.Win.GetKey(KeyCode.Up))
            {
                if (isAttacking)
                {
                    return;
                }

                MovingUp();
            }
            else if (Game.Win.GetKey(KeyCode.S) || Game.Win.GetKey(KeyCode.Down))
            {
                if (isAttacking)
                {
                    return;
                }

                MovingDown();
            }
            else
            {
                RigidBody.Velocity.Y = 0.0f;


                if (direction == Direction.DOWN)
                {
                    animation = GfxMgr.GetAnimation("IdleDown");
                }
                else if (direction == Direction.UP)
                {
                    animation = GfxMgr.GetAnimation("IdleUp");
                }

            }

            if (RigidBody.Velocity.X != 0 || RigidBody.Velocity.Y != 0)
            {
                RigidBody.Velocity = RigidBody.Velocity.Normalized() * maxSpeed;
            }

            if (Game.Win.GetKey(KeyCode.Space))
            {
                Attack();
            }
            else 
            {
                isAttacking = false;
            }
            
        }

        private void MovingRight()
        {

            RigidBody.Velocity.X = maxSpeed;
            direction = Direction.RIGHT;
            animation = GfxMgr.GetAnimation("Right");
        }

        private void MovingLeft()
        {
            RigidBody.Velocity.X = -maxSpeed;
            direction = Direction.LEFT;
            animation = GfxMgr.GetAnimation("Left");
        }

        private void MovingDown()
        {
            RigidBody.Velocity.Y = maxSpeed;
            direction = Direction.DOWN;
            animation = GfxMgr.GetAnimation("Down");
        }

        private void MovingUp()
        {
            RigidBody.Velocity.Y = -maxSpeed;
            direction = Direction.UP;
            animation = GfxMgr.GetAnimation("Up");
        }

        public override void OnCollide(GameObject other) { }

        /*public override void Update()
        {
            //animation.Update();
        }*/

        /*public override void Draw()
        {
            //sprite.DrawTexture(texture, (int)offSet.X + animation.XOffset, (int)offSet.Y + animation.YOffset, 64, 64);
            //sprite.DrawTexture(texture);
        }*/

        protected override void Attack()
        {
            switch (direction)
            {
                case Direction.DOWN:
                    animation = GfxMgr.GetAnimation("AttackDown");
                    break;
                case Direction.UP:
                    animation = GfxMgr.GetAnimation("AttackUp");
                    break;
                case Direction.RIGHT:
                    animation = GfxMgr.GetAnimation("AttackRight");
                    break;
                case Direction.LEFT:
                    animation = GfxMgr.GetAnimation("AttackLeft");
                    break;
            }

            if (!isAttacking)
            {
                isAttacking = true;
            }
            else if (isAttacking)
            {
                isAttacking = false;
            }
        }

        public override void Update()
        {
            base.Update();
            animation.Update();
            animation.Start();

        }



        public override void Draw()
        {
            sprite.DrawTexture(texture, animation.XOffset, animation.YOffset, animation.FrameWidth, animation.FrameHeight);
        }


    }
}
