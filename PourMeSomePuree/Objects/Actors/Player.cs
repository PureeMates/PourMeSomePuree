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
        private Hud hud;

        private int maxStamina;
        private int stamina;
        private int staminaAttCost;
        private float staminaRechargeRatio;
        private int coins = 0;

        public override int Energy { get => base.Energy; set { base.Energy = value; hud.ScaleEnergy((float)value / maxEnergy); } }
        public int Stamina { get { return stamina; } set { stamina = value; hud.ScaleStamina((float)value / maxStamina); } }

        

        public Player() : base("player", 64, 64)
        {
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
            maxSpeed = 200.0f;

            RigidBody.Collider = CollidersFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.Player;
            RigidBody.AddCollisionType(RigidBodyType.Background | RigidBodyType.Coin);
            

            AnimationStorage.LoadPlayerAnimations();
            movementAnimation = GfxMgr.GetAnimation("playerDown");
            attackAnimation = GfxMgr.GetAnimation("playerAttackDown");
            actualAnimation = movementAnimation;

            audioSource.Volume = 0.25f;
            audioClip = AudioMgr.GetClip("sword");

            hud = new Hud("hudMaskAvatar", "hudMaskEnergy", "hudMaskStamina", "portrait", new Vector2(17.5f, 17.5f), new Vector2(73.0f,16.0f), new Vector2(86.75f,34.0f), new Vector2(24.5f,17.5f));
            hud.Position = new Vector2(10.0f, 10.0f);

            maxEnergy = 100;
            maxStamina = 100;
            staminaAttCost = 25;
            staminaRechargeRatio = 0.2f;

            Restore();

            IsActive = true;
        }

        public void Input()
        {
            if (IsActive)
            {
                if (Game.Win.GetKey(KeyCode.Space))
                {
                    if (!isAttackPressed && stamina >= staminaAttCost)
                    {
                        Attack();
                        Stamina -= staminaAttCost;
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

                if (Game.Win.GetKey(KeyCode.Return))
                {
                    if (!isReturnPressed)
                    {
                        AddDamage(15);
                    }
                }
                else if (isReturnPressed)
                {
                    isReturnPressed = false;
                } 
            }
        }
        public override void Update()
        {
            if (IsActive)
            {
                if (stamina < maxStamina)
                {
                    staminaRechargeRatio -= Game.DeltaTime;
                    if (staminaRechargeRatio <= 0.0f)
                    {
                        staminaRechargeRatio = 0.2f;
                        Stamina += 5;
                    }
                }

                if (attackAnimation.IsPlaying)
                {
                    actualAnimation = attackAnimation;
                    if ((attackAnimation.CurrentFrame == 2) && !audioSource.IsPlaying)
                    {
                        audioSource.Play(audioClip);
                    }
                }
                else
                {
                    audioSource.Play(audioClip);
                    actualAnimation = movementAnimation;
                }

                base.Update(); 
            } 
        }
        public override void Draw()
        {
            if(IsActive)
            {
                sprite.DrawTexture(texture, actualAnimation.XOffset, actualAnimation.YOffset, actualAnimation.FrameWidth, actualAnimation.FrameHeight);
            }
        }

        private void MovingRight()
        {
            RigidBody.Velocity.X = maxSpeed;
            direction = Direction.RIGHT;
            movementAnimation = GfxMgr.GetAnimation("playerRight");
            movementAnimation.Start();
        }
        private void MovingLeft()
        {
            RigidBody.Velocity.X = -maxSpeed;
            direction = Direction.LEFT;
            movementAnimation = GfxMgr.GetAnimation("playerLeft");
            movementAnimation.Start(); 
        }
        private void MovingDown()
        {
            RigidBody.Velocity.Y = maxSpeed;
            direction = Direction.DOWN;
            movementAnimation = GfxMgr.GetAnimation("playerDown");
            movementAnimation.Start(); 
        }
        private void MovingUp()
        {
            RigidBody.Velocity.Y = -maxSpeed;
            direction = Direction.UP;
            movementAnimation = GfxMgr.GetAnimation("playerUp");
            movementAnimation.Start(); 
        }

        protected override void Attack()
        {
            isAttackPressed = true;

            switch (direction)
            {
                case Direction.DOWN:
                    attackAnimation = GfxMgr.GetAnimation("playerAttackDown");
                    break;
                case Direction.UP:
                    attackAnimation = GfxMgr.GetAnimation("playerAttackUp");
                    break;
                case Direction.RIGHT:
                    attackAnimation = GfxMgr.GetAnimation("playerAttackRight");
                    break;
                case Direction.LEFT:
                    attackAnimation = GfxMgr.GetAnimation("playerAttackLeft");
                    break;
            }

            attackAnimation.Start();
        }

        public override void Restore()
        {
            Stamina = maxStamina;
            base.Restore();
        }

        public override void OnDie()
        {
            IsActive = false;
            base.Destroy();
        }

        public void AddCoin(int coin)
        {
            coins += coin;
        }

        
    }
}
