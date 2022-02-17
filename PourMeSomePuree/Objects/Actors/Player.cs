using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;
using Aiv.Audio;

namespace PourMeSomePuree
{
    class Player : Actor
    {
        public Player() : base("player", 64, 64)
        {
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
            maxSpeed = 200.0f;
            IsActive = true;
            RigidBody.Collider = CollidersFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.Player;
            RigidBody.AddCollisionType(RigidBodyType.Background);
            LoadAnimations();
            movementAnimation = GfxMgr.GetAnimation("down");
            attackAnimation = GfxMgr.GetAnimation("attackDown");
            actualAnimation = movementAnimation;
        }

        private void LoadAnimations()
        {
            //when player is moving
            GfxMgr.AddAnimation("down", 15, 4, 64, 64, 1, 1);
            GfxMgr.AddAnimation("up", 15, 4, 64, 64, 1, 2);
            GfxMgr.AddAnimation("right", 15, 4, 64, 64, 1, 3);
            GfxMgr.AddAnimation("left", 15, 4, 64, 64, 1, 4);

            //When Player stop
            GfxMgr.AddAnimation("idleDown", 15, 1, 64, 64, 1, 1);
            GfxMgr.AddAnimation("idleUp", 15, 1, 64, 64, 1, 2);
            GfxMgr.AddAnimation("idleRight", 15, 1, 64, 64, 1, 3);
            GfxMgr.AddAnimation("idleLeft", 15, 1, 64, 64, 1, 4);

            //when Player is attacking
            GfxMgr.AddAnimation("attackDown", 15, 4, 64, 64, 5, 1, false);
            GfxMgr.AddAnimation("attackUp", 15, 4, 64, 64, 5, 2, false);
            GfxMgr.AddAnimation("attackRight", 15, 4, 64, 64, 5, 3, false);
            GfxMgr.AddAnimation("attackLeft", 15, 4, 64, 64, 5, 4, false);
        }

        public void Input()
        {
            if (Game.Win.GetKey(KeyCode.Space))
            {
                if (!isAttackPressed)
                {
                    Attack();
                }
            }
            else if (isAttackPressed)
            {
                isAttackPressed = false;
            }

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

            if (RigidBody.Velocity.X != 0 || RigidBody.Velocity.Y != 0)
            {
                RigidBody.Velocity = RigidBody.Velocity.Normalized() * maxSpeed;
            }
            else
            {
                isMoving = false;
                movementAnimation.Stop();
            }
        }

        public override void Update()
        {
            if(attackAnimation.IsPlaying)
            {
                actualAnimation = attackAnimation;
            }
            else
            {
                actualAnimation = movementAnimation;
            }
            base.Update();
        }

        private void MovingRight()
        {
            isMoving = true;
            RigidBody.Velocity.X = maxSpeed;
            direction = Direction.RIGHT;
            movementAnimation = GfxMgr.GetAnimation("right");
            movementAnimation.Start();
        }

        private void MovingLeft()
        {
            isMoving = true;
            RigidBody.Velocity.X = -maxSpeed;
            direction = Direction.LEFT;
            movementAnimation = GfxMgr.GetAnimation("left");
            movementAnimation.Start(); 
        }

        private void MovingDown()
        {
            isMoving = true;
            RigidBody.Velocity.Y = maxSpeed;
            direction = Direction.DOWN;
            movementAnimation = GfxMgr.GetAnimation("down");
            movementAnimation.Start(); 
        }

        private void MovingUp()
        {
            isMoving = true;
            RigidBody.Velocity.Y = -maxSpeed;
            direction = Direction.UP;
            movementAnimation = GfxMgr.GetAnimation("up");
            movementAnimation.Start(); 
        }

        public override void OnCollide(GameObject other) { }

        protected override void Attack()
        {
            isAttackPressed = true;

            switch (direction)
            {
                case Direction.DOWN:
                    attackAnimation = GfxMgr.GetAnimation("attackDown");
                    break;
                case Direction.UP:
                    attackAnimation = GfxMgr.GetAnimation("attackUp");
                    break;
                case Direction.RIGHT:
                    attackAnimation = GfxMgr.GetAnimation("attackRight");
                    break;
                case Direction.LEFT:
                    attackAnimation = GfxMgr.GetAnimation("attackLeft");
                    break;
            }

            attackAnimation.Start();
        }

        public override void Draw()
        {
            sprite.DrawTexture(texture, actualAnimation.XOffset, actualAnimation.YOffset, actualAnimation.FrameWidth, actualAnimation.FrameHeight);
        }
    }
}
