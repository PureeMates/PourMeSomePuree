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
        private Animation animation;

        public Pot() : base("pot", 64, 64)
        {
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.25f, Game.Win.Height * 0.5f);

            RigidBody = new RigidBody(this);
            RigidBody.Collider = CollidersFactory.CreateBoxFor(this, 24, 26);
            RigidBody.Type = RigidBodyType.Pot;
            RigidBody.AddCollisionType(RigidBodyType.Player);

            AnimationStorage.LoadPotAnimations();
            animation = GfxMgr.GetAnimation("breaking");

            IsActive = true;
        }

        public override void Draw()
        {
            sprite.DrawTexture(texture, animation.XOffset, animation.YOffset, animation.FrameWidth, animation.FrameHeight);
        }

        public override void OnCollide(GameObject other)
        {
            // per punizione
            if (other.Position.X < ((BoxCollider)RigidBody.Collider).LeftPos && other.Position.X + other.HalfWidth >= ((BoxCollider)RigidBody.Collider).LeftPos)
            {
                other.Position = new Vector2(((BoxCollider)RigidBody.Collider).LeftPos - other.HalfWidth, other.Position.Y);
            }
            else if (other.Position.X > ((BoxCollider)RigidBody.Collider).RightPos && other.Position.X - other.HalfWidth <= ((BoxCollider)RigidBody.Collider).RightPos)
            {
                other.Position = new Vector2(((BoxCollider)RigidBody.Collider).RightPos + other.HalfWidth, other.Position.Y);
            }
            else if (other.Position.Y > ((BoxCollider)RigidBody.Collider).DownPos && other.Position.Y - other.HalfHeight <= ((BoxCollider)RigidBody.Collider).DownPos)
            {
                other.Position = new Vector2(other.Position.X, ((BoxCollider)RigidBody.Collider).DownPos + other.HalfHeight);
            }
            else if (other.Position.Y < ((BoxCollider)RigidBody.Collider).UpPos && other.Position.Y + other.HalfHeight >= ((BoxCollider)RigidBody.Collider).UpPos)
            {
                other.Position = new Vector2(other.Position.X, ((BoxCollider)RigidBody.Collider).UpPos - other.HalfHeight);
            }
        }
    }
}
