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
            maxEnergy = 100;
            Restore();

            RigidBody.Collider = CollidersFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.Player;
            RigidBody.AddCollisionType(RigidBodyType.Background);

            AnimationStorage.LoadPlayerAnimations();
            movementAnimation = GfxMgr.GetAnimation("down");
            attackAnimation = GfxMgr.GetAnimation("attackDown");
            actualAnimation = movementAnimation;

            audioSource.Volume = 0.25f;
            audioClip = AudioMgr.GetClip("sword");
            
            IsActive = true;
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
                if((attackAnimation.CurrentFrame == 2) && !audioSource.IsPlaying)
                {
                    audioSource.Play(audioClip);
                }
            }
            else
            {
                actualAnimation = movementAnimation;
            }
            base.Update();
        }
        public override void Draw()
        {
            sprite.DrawTexture(texture, actualAnimation.XOffset, actualAnimation.YOffset, actualAnimation.FrameWidth, actualAnimation.FrameHeight);
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

        public override void OnDie()
        {
            base.Destroy();
        }
    }
}
