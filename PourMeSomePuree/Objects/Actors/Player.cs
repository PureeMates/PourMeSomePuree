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
        public bool IsAttacking { get { return isAttacking; } }
        public Animation Anim { get { return attackAnimation; } }
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
            attackAnimation = GfxMgr.GetAnimation("IdleDown");
            direction = Direction.DOWN;
        }

        public void Input()
        {
            if (isAttacking == false || attackAnimation.IsFinished == true) //togliere seconda clausola e riaggiungere vector.zero in attack se non si vuole farlo fermare
            {
                if (Game.Win.GetKey(KeyCode.D) || Game.Win.GetKey(KeyCode.Right))
                {
                    MovingRight();
                }
                else if (Game.Win.GetKey(KeyCode.A) || Game.Win.GetKey(KeyCode.Left))
                {
                    MovingLeft();
                }
                else
                {
                    RigidBody.Velocity.X = 0.0f;
                }

                if (Game.Win.GetKey(KeyCode.W) || Game.Win.GetKey(KeyCode.Up))
                {
                    MovingUp();
                }
                else if (Game.Win.GetKey(KeyCode.S) || Game.Win.GetKey(KeyCode.Down))
                {
                    MovingDown();
                }
                else
                {
                    RigidBody.Velocity.Y = 0.0f;
                }
            }

            if (RigidBody.Velocity.X != 0 || RigidBody.Velocity.Y != 0)
            {
                RigidBody.Velocity = RigidBody.Velocity.Normalized() * maxSpeed;
            }

            if (Game.Win.GetKey(KeyCode.Space))
            {
                if (!isAttacking || attackAnimation.IsFinished == true)
                {
                    Attack();
                    attackAnimation.Start();
                    isAttacking = true;
                }
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
        }

        private void MovingLeft()
        {
            RigidBody.Velocity.X = -maxSpeed;
            direction = Direction.LEFT;
        }

        private void MovingDown()
        {
            RigidBody.Velocity.Y = maxSpeed;
            direction = Direction.DOWN;
        }

        private void MovingUp()
        {
            RigidBody.Velocity.Y = -maxSpeed;
            direction = Direction.UP;
        }

        public override void OnCollide(GameObject other) { }

        protected override void Attack()
        {
            switch (direction)
            {
                case Direction.DOWN:
                    attackAnimation = GfxMgr.GetAnimation("AttackDown");
                    break;
                case Direction.UP:
                    attackAnimation = GfxMgr.GetAnimation("AttackUp");
                    break;
                case Direction.RIGHT:
                    attackAnimation = GfxMgr.GetAnimation("AttackRight");
                    break;
                case Direction.LEFT:
                    attackAnimation = GfxMgr.GetAnimation("AttackLeft");
                    break;
            }
        }

        public void UpdateAnimation(Vector2 velocity)
        {
            if ((isAttacking == true || attackAnimation.IsFinished == true) && velocity == Vector2.Zero)
            {
                switch (direction)
                {
                    case Direction.DOWN:
                        animation = GfxMgr.GetAnimation("IdleDown");
                        break;
                    case Direction.UP:
                        animation = GfxMgr.GetAnimation("IdleUp");
                        break;
                    case Direction.RIGHT:
                        animation = GfxMgr.GetAnimation("IdleRight");
                        break;
                    case Direction.LEFT:
                        animation = GfxMgr.GetAnimation("IdleLeft");
                        break;
                }
            }
            else if ((isAttacking == true || attackAnimation.IsFinished == true) && velocity != Vector2.Zero)
            {
                switch (direction)
                {
                    case Direction.DOWN:
                        animation = GfxMgr.GetAnimation("Down");
                        break;
                    case Direction.UP:
                        animation = GfxMgr.GetAnimation("Up");
                        break;
                    case Direction.RIGHT:
                        animation = GfxMgr.GetAnimation("Right");
                        break;
                    case Direction.LEFT:
                        animation = GfxMgr.GetAnimation("Left");
                        break;
                }
            }
            #region ANIMATIONATTACKMK1
            //else
            //{
            //    switch (direction)
            //    {
            //        case Direction.DOWN:
            //            attackAnimation = GfxMgr.GetAnimation("AttackDown");
            //            break;
            //        case Direction.UP:
            //            attackAnimation = GfxMgr.GetAnimation("AttackUp");
            //            break;
            //        case Direction.RIGHT:
            //            attackAnimation = GfxMgr.GetAnimation("AttackRight");
            //            break;
            //        case Direction.LEFT:
            //            attackAnimation = GfxMgr.GetAnimation("AttackLeft");
            //            break;
            //    }
            //} 
            #endregion
        }
        public override void Update()
        {
            UpdateAnimation(RigidBody.Velocity);

            animation.Start();
            animation.Update();

            attackAnimation.Update();
        }



        public override void Draw()
        {
            if (isAttacking || attackAnimation.IsFinished == false)
            {
                sprite.DrawTexture(texture, attackAnimation.XOffset, attackAnimation.YOffset, attackAnimation.FrameWidth, attackAnimation.FrameHeight);
            }
            else
            {
                sprite.DrawTexture(texture, animation.XOffset, animation.YOffset, animation.FrameWidth, animation.FrameHeight);
            }
        }


    }
}
