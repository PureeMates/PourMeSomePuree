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
    class Background : GameObject
    {
        private int width;
        private int height;

        AudioSource bgSource = new AudioSource();
        AudioClip bgMusic = new AudioClip("Assets/Audio/Horde_theme.ogg");

        public Background() : base("background")
        {
            width = 1070;
            height = 507;

            sprite.scale = new Vector2(0.67f);
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            sprite.position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);

            RigidBody = new RigidBody(this);
            RigidBody.Collider = CollidersFactory.CreateInvertedBoxFor(this, width, height);
            RigidBody.Type = RigidBodyType.Background;
            RigidBody.AddCollisionType(RigidBodyType.Player);

            IsActive = true;
        }

        public override void OnCollide(GameObject other)
        {
            BoxColliderInverted bgCollider = (BoxColliderInverted)RigidBody.Collider;

            if (other.Position.X + other.HalfWidth >= bgCollider.RightPos)
            {
                other.Position = new Vector2(bgCollider.RightPos - other.HalfWidth, other.Position.Y);
            }
            else if (other.Position.X - other.HalfWidth <= bgCollider.LeftPos)
            {
                other.Position = new Vector2(bgCollider.LeftPos + other.HalfWidth, other.Position.Y);
            }

            if (other.Position.Y - other.HalfHeight <= bgCollider.UpPos)
            {
                other.Position = new Vector2(other.Position.X, bgCollider.UpPos + other.HalfHeight);
            }
            else if (other.Position.Y + other.HalfHeight >= bgCollider.DownPos)
            {
                other.Position = new Vector2(other.Position.X, bgCollider.DownPos - other.HalfHeight);
            }
        }

        public void Input()     //audio
        {
            bgSource.Stream(bgMusic, Game.Win.DeltaTime);
        }
    }
}
