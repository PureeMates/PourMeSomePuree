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
        private int id;

        private Animation animation;
        private Animation actualAnimation;

        private bool destroyed;

        private float counter;
        private float restartCounter;

        public Pot(int id, Vector2 pos) : base("pot", 64, 64)
        {
            this.id = id;

            sprite.scale = new Vector2(1.75f);
            Position = pos;

            RigidBody = new RigidBody(this);
            RigidBody.Collider = CollidersFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.POT;
            RigidBody.AddCollisionType(RigidBodyType.PLAYER | RigidBodyType.ENEMY);

            animation = GfxMgr.AddAnimation($"{this.id}breaking", 15, 4, 64, 64, 1, 1, false);
            actualAnimation = animation;

            destroyed = false;

            restartCounter = 15;
            counter = restartCounter;

            IsActive = true;
        }

        public override void Update()
        {
            if (!animation.IsPlaying)
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
            BoxCollider pot = (BoxCollider)RigidBody.Collider;
            Player player = (Player)other;
            if (!animation.IsPlaying && destroyed == false)
            {
                animation.Start();
                destroyed = true;
                IsActive = false;
            }

            if ((other.Position.X < pot.LeftPos && other.Position.X + other.HalfWidth >= pot.LeftPos))
            {
                other.Position = new Vector2(pot.LeftPos - other.HalfWidth, other.Position.Y);
            }
            else if (other.Position.X > pot.RightPos && other.Position.X - other.HalfWidth <= pot.RightPos)
            {
                other.Position = new Vector2(pot.RightPos + other.HalfWidth, other.Position.Y);
            }
            else if (other.Position.Y > pot.DownPos && other.Position.Y - other.HalfHeight <= pot.DownPos)
            {
                other.Position = new Vector2(other.Position.X, pot.DownPos + other.HalfHeight);
            }
            else if (other.Position.Y < pot.UpPos && other.Position.Y + other.HalfHeight >= pot.UpPos)
            {
                other.Position = new Vector2(other.Position.X, pot.UpPos - other.HalfHeight);
            }
        }

        public void AddHealth(Player player)
        {
            player.Restore();
        }
    }
}
