using System;
using System.Collections.Generic;
using System.IO;
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
        private TextObject coinsText;
        private TextObject highScoreText;
        private TextObject timeText;
        private Sword sword;

        private int maxStamina;
        private int stamina;
        private int staminaAttCost;
        private float staminaRechargeRatio;

        private int coins;
        private long timeStart;
        private int time;
        private long counter;
        private int damage;

        private string path;
        private int highScore;

        public override int Energy { get => base.Energy; set { base.Energy = value; hud.ScaleEnergy((float)value / maxEnergy); } }
        public int Stamina { get { return stamina; } set { stamina = value; hud.ScaleStamina((float)value / maxStamina); } }

        public int StaminaAttCost { get { return staminaAttCost; } set { staminaAttCost = value; } } 
        public bool CanOpen { get; set; }

        public int Damage { get { return damage; } set { damage = value; } }
        public int Defence { get; set; }

        public Player() : base("player", 64, 64)
        {
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
            maxSpeed = 200.0f;

            RigidBody.Collider = CollidersFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.PLAYER;
            RigidBody.AddCollisionType(RigidBodyType.COIN);
            AnimationStorage.LoadPlayerAnimations();
            movementAnimation = GfxMgr.GetAnimation("playerDown");
            attackAnimation = GfxMgr.GetAnimation("playerAttackDown");
            actualAnimation = movementAnimation;

            CanMoveUp = true;
            CanMoveDown = true;
            CanMoveLeft = true;
            CanMoveRight = true;
            CanOpen = false;

            audioSource.Volume = 0.25f;
            audioClip = AudioMgr.GetClip("sword");

            hud = new Hud("hudMaskAvatar", "hudMaskEnergy", "hudMaskStamina", "portrait", new Vector2(17.5f, 17.5f), new Vector2(73.0f,16.0f), new Vector2(86.75f,34.0f), new Vector2(24.5f,17.5f));
            hud.Position = new Vector2(10.0f, 10.0f);

            coins = 0;
            coinsText = new TextObject(hud.Position + new Vector2(100.0f, 47.0f), horizontalSpace: -13);
            coinsText.IsActive = true;
            UpdateScoreText();
            coinsText.SetColor(new Vector4(0.921f, 0.545f, 0.117f, 1.0f));

            timeStart = DateTime.Now.Ticks;
            counter = 0;
            timeText = new TextObject(hud.Position + new Vector2(300.0f, hud.Position.Y), horizontalSpace: -13);
            timeText.IsActive = true;
            UpdateTimeText();

            Defence = 0;
            damage = 50;
            maxEnergy = 100;
            maxStamina = 100;
            staminaAttCost = 25;
            staminaRechargeRatio = 0.2f;

            sword = new Sword(this);

            Restore();

            path = @".\HighScore.txt";
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    highScore = int.Parse(sr.ReadToEnd().Trim());
                }
            }
            catch (FileNotFoundException) { }

            highScoreText = new TextObject(hud.Position + new Vector2(850.0f, hud.Position.Y), horizontalSpace: -13);
            highScoreText.IsActive = true;
            UpdateHighScore();

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

                if ((Game.Win.GetKey(KeyCode.D) || Game.Win.GetKey(KeyCode.Right)) && CanMoveRight)
                {
                    MovingRight();
                    CanMoveDown = false;
                    CanMoveLeft = false;
                    CanMoveUp = false;
                }
                else if ((Game.Win.GetKey(KeyCode.A) || Game.Win.GetKey(KeyCode.Left)) && CanMoveLeft)
                {
                    MovingLeft();
                    CanMoveDown = false;
                    CanMoveRight = false;
                    CanMoveUp = false;
                }
                else
                {
                    RigidBody.Velocity.X = 0.0f;
                    CanMoveRight = true;
                    CanMoveLeft = true;
                }

                if ((Game.Win.GetKey(KeyCode.W) || Game.Win.GetKey(KeyCode.Up)) && CanMoveUp)
                {
                    MovingUp();
                    CanMoveDown = false;
                    CanMoveRight = false;
                    CanMoveLeft = false;
                }
                else if ((Game.Win.GetKey(KeyCode.S) || Game.Win.GetKey(KeyCode.Down)) && CanMoveDown)
                {
                    MovingDown();
                    CanMoveLeft = false;
                    CanMoveRight = false;
                    CanMoveUp = false;
                }
                else
                {
                    RigidBody.Velocity.Y = 0.0f;
                    CanMoveDown = true;
                    CanMoveUp = true;
                }

                if (Game.Win.GetKey(KeyCode.E) && coins >= 5)
                {
                    coins -= 5;
                    CanOpen = true;
                    UpdateScoreText();
                }
                else
                {
                    CanOpen = false;
                }
                if (Game.Win.GetKey(KeyCode.P))
                {
                    coins += 5;
                }

                //CHEATER
                if (Game.Win.GetKey(KeyCode.Return))
                {
                    if (!isReturnPressed)
                    {
                        AddDamage(15, Defence);
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
                counter = DateTime.Now.Ticks - timeStart;
                if(counter >= TimeSpan.TicksPerSecond)
                {
                    timeStart = DateTime.Now.Ticks;
                    time++;
                    UpdateTimeText();
                }


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

                    if (attackAnimation.CurrentFrame == 3)
                    {
                        sword.DeactiveSword();
                    }
                }
                else
                {
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

            sword.ActiveSword();
            attackAnimation.Start();
        }

        protected void UpdateHighScore()
        {
            highScoreText.Text = $"HighScore: {highScore}";
        }

        protected void UpdateScoreText()
        {
            coinsText.Text = coins.ToString("000");
        }

        protected void UpdateTimeText()
        {
            timeText.Text = $"{time}s";
        }

        public void AddCoins(int points)
        {
            coins += MathHelper.Clamp(points, 0, 999);
            UpdateScoreText();
        }

        public override void Restore()
        {
            Stamina = maxStamina;
            base.Restore();
        }

        public override void OnDie()
        {
            IsActive = false;
            sword.IsActive = false;
            sword = null;

            if (time > highScore)
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(time);
                } 
            }
        }
    }
}
