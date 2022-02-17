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
    //enum DirectionType { DOWN = 0, UP = 64, RIGHT = 128, LEFT = 192 }
    //enum ActionType { MOVEMENT = 0, ATTACK = 256 }

    class Player : Actor
    {
        //private bool isSwordPressed;
        //private bool isAttacking;
        //private Vector2 offSet;

        AudioSource playerSource= new AudioSource();
        AudioClip playerAttack = new AudioClip("Assets/Audio/Sound_sword_attack.mp3");

        public Player() : base("player", 64, 64)
        {
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
            maxSpeed = 200.0f;
            IsActive = true;
            RigidBody.Collider = CollidersFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.Player;
            RigidBody.AddCollisionType(RigidBodyType.Background);
            //offSet = new Vector2(0, 0);

            //animation = new Animation(15, 4, 64, 64, loop: false);
            //animation = new Animation(15, 4, 64, 64, loop: false);
        }

        public void Input()
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

            if (RigidBody.Velocity.X != 0 || RigidBody.Velocity.Y != 0)
            {
                RigidBody.Velocity = RigidBody.Velocity.Normalized() * maxSpeed;
            }

            if (Game.Win.GetKey(KeyCode.Space))
            {
                Attack();
            }

            // AUDIO
            if (Game.Win.GetKey(KeyCode.Space))     // <- aggiungere attacco all'enemy
            {
                playerSource.Stream(playerAttack, Game.Win.DeltaTime);
            }
            // fare attacco melee
        }

        private bool MovingRight()
        {
            RigidBody.Velocity.X = maxSpeed;
            //offSet.X = (int)ActionType.MOVEMENT;
            //offSet.Y = (int)DirectionType.RIGHT;
            //animation.Start();

            return true;
        }

        private bool MovingLeft()
        {
            RigidBody.Velocity.X = -maxSpeed;
            //offSet.X = (int)ActionType.MOVEMENT;
            //offSet.Y = (int)DirectionType.LEFT;
            //animation.Start();

            return true;
        }

        private bool MovingDown()
        {
            RigidBody.Velocity.Y = maxSpeed;
            //offSet.X = (int)ActionType.MOVEMENT;
            //offSet.Y = (int)DirectionType.DOWN;
            //animation.Start();

            return true;
        }

        private bool MovingUp()
        {
            RigidBody.Velocity.Y = -maxSpeed;
            //offSet.X = (int)ActionType.MOVEMENT;
            //offSet.Y = (int)DirectionType.UP;
            //animation.Start();

            return true;
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
            //offSet.X = (int)ActionType.ATTACK;

            /*if (!isSwordPressed)
            {
                isSwordPressed = true;
                Attack();
                attackAnimation.Start();
            }
            else if (isSwordPressed)
            {
                isSwordPressed = false;
            }*/
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
