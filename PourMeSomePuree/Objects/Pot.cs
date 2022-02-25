using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace PourMeSomePuree
{
    class Pot : GameObject
    {
        protected int healthRestored;
        protected bool destroyed;
        protected Animation animation;
        protected Animation actualAnimation;

        public Pot() : base("pot", 64, 64)
        {
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.25f, Game.Win.Height * 0.5f);
            healthRestored = -50;
            destroyed = false;
            RigidBody = new RigidBody(this);
            RigidBody.Collider = CollidersFactory.CreateBoxFor(this/*, 24, 26*/);
            //RigidBody.Collider = CollidersFactory.CreateCircleFor(this);
            RigidBody.Type = RigidBodyType.Pot;
            RigidBody.AddCollisionType(RigidBodyType.Player);

            AnimationStorage.LoadPotAnimations();
            animation = GfxMgr.GetAnimation("breaking");
            actualAnimation = animation;

            IsActive = true;
        }
        public override void Update()
        {
            if (animation.IsPlaying)
            {
                actualAnimation = animation;
            }
            else
            {
                animation.Stop();
            }

            actualAnimation.Update();
        }
        public override void Draw()
        {
            if (destroyed == false || animation.IsPlaying)
            {
                sprite.DrawTexture(texture, actualAnimation.XOffset, actualAnimation.YOffset, actualAnimation.FrameWidth, actualAnimation.FrameHeight);
            }
        }

        public override void OnCollide(GameObject other)
        {
            #region Box Mk1 
            BoxCollider pot = (BoxCollider)RigidBody.Collider;
            Player player = (Player)other;
            //if (!animation.IsPlaying && destroyed == false)
            //{
            //    animation.Start();
            //    destroyed = true;
            //    //IsActive = false;
            //}

            //if ((other.Position.X < pot.LeftPos && other.Position.X + other.HalfWidth >= pot.LeftPos))
            //{
            //    other.Position = new Vector2(pot.LeftPos - other.HalfWidth, other.Position.Y);
            //}
            //else if (other.Position.X > pot.RightPos && other.Position.X - other.HalfWidth <= pot.RightPos)
            //{
            //    other.Position = new Vector2(pot.RightPos + other.HalfWidth, other.Position.Y);
            //}
            //else if (other.Position.Y > pot.DownPos && other.Position.Y - other.HalfHeight <= pot.DownPos)
            //{
            //    other.Position = new Vector2(other.Position.X, pot.DownPos + other.HalfHeight);
            //}
            //else if (other.Position.Y < pot.UpPos && other.Position.Y + other.HalfHeight >= pot.UpPos)
            //{
            //    other.Position = new Vector2(other.Position.X, pot.UpPos - other.HalfHeight);
            //}

            //if ((other.Position.Y + other.HalfHeight > pot.UpPos && other.Position.Y + other.HalfHeight < pot.DownPos))
            //{
            //    if (other.Position.X < pot.LeftPos && other.Position.X + other.HalfWidth >= pot.LeftPos)
            //    {
            //        other.Position = new Vector2(pot.LeftPos - other.HalfWidth, other.Position.Y);
            //    }
            //    else if (other.Position.X > pot.RightPos && other.Position.X - other.HalfWidth <= pot.RightPos)
            //    {
            //        other.Position = new Vector2(pot.RightPos + other.HalfWidth, other.Position.Y);
            //    }
            //}
            //else if ((other.Position.X - other.HalfWidth > pot.LeftPos && other.Position.X + other.HalfWidth< pot.RightPos))
            //{
            //    if (other.Position.Y > pot.DownPos && other.Position.Y - other.HalfHeight < pot.DownPos)
            //    {
            //        other.Position = new Vector2(other.Position.X, pot.DownPos + other.HalfHeight);
            //    }
            //    else if (other.Position.Y < pot.UpPos && other.Position.Y + other.HalfHeight > pot.UpPos)
            //    {
            //        other.Position = new Vector2(other.Position.X, pot.UpPos - other.HalfHeight);
            //    }
            //}

            //if (other.Position.Y + other.HalfHeight >= pot.UpPos)
            //{
            //    player.CanMoveDown = false;
            //    player.CanMoveUp = true;
            //}
            //else if (other.Position.Y - other.HalfHeight <= pot.DownPos)
            //{
            //    player.CanMoveUp = false;
            //    player.CanMoveDown = true;
            //}
            //else if (other.Position.X + other.HalfWidth >= pot.LeftPos)
            //{
            //    player.CanMoveRight = false;
            //    player.CanMoveLeft = true;
            //}
            //else if (other.Position.X - other.HalfWidth <= pot.RightPos )
            //{
            //    player.CanMoveLeft = false;
            //    player.CanMoveRight = true;
            //}

            //if ((other.Position.X < pot.LeftPos && other.Position.X + other.HalfWidth >= pot.LeftPos))
            //{
            //    player.CanMoveRight = false;
            //    player.CanMoveUp = true;
            //    player.CanMoveDown = true;
            //    player.CanMoveLeft = true;
            //}
            //else if (other.Position.X > pot.RightPos && other.Position.X - other.HalfWidth <= pot.RightPos)
            //{
            //    player.CanMoveLeft = false;
            //    player.CanMoveUp = true;
            //    player.CanMoveDown = true;
            //    player.CanMoveRight = true;
            //}
            //else if (other.Position.Y > pot.DownPos && other.Position.Y - other.HalfHeight <= pot.DownPos)
            //{
            //    player.CanMoveUp = false;
            //    player.CanMoveRight = true;
            //    player.CanMoveLeft = true;
            //    player.CanMoveDown = true;
            //}
            //else if (other.Position.Y < pot.UpPos && other.Position.Y + other.HalfHeight >= pot.UpPos)
            //{
            //    player.CanMoveDown = false;
            //    player.CanMoveRight = true;
            //    player.CanMoveLeft = true;
            //    player.CanMoveUp = true;
            //}


            #endregion

            #region Circle Mk1
            //CircleCollider pot = (CircleCollider)RigidBody.Collider;

            //if (other.Position.X < pot.Position.X - pot.Radius && other.Position.X + other.HalfWidth >= pot.Position.X - pot.Radius)
            //{
            //    other.Position = new Vector2(pot.Position.X - pot.Radius - other.HalfWidth, pot.Position.Y);
            //}
            //else if (other.Position.Y < pot.Position.Y - pot.Radius && other.Position.Y + other.HalfHeight >= pot.Position.Y - pot.Radius)
            //{
            //    other.Position = new Vector2(other.Position.X, pot.Position.Y - pot.Radius - other.HalfHeight);
            //} 
            #endregion
        }

        public void AddHealth(Player player)
        {
            player.Restore();
        }
    }
}
