using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace PourMeSomePuree
{
    class BoxCollider : Collider
    {
        private float halfWidth;
        private float halfHeight;

        public float HalfWidth { get { return halfWidth; } }
        public float HalfHeight { get { return halfHeight; } }

        public float RightPos { get; private set; }
        public float LeftPos { get; private set; }
        public float UpPos { get; private set; }
        public float DownPos { get; private set; }

        public BoxCollider(RigidBody rb, int w, int h) : base(rb)
        {
            halfWidth = w * 0.5f;
            halfHeight = h * 0.5f;

            RightPos = Position.X + HalfWidth;
            LeftPos = Position.X - halfWidth;
            UpPos = Position.Y - halfHeight;
            DownPos = Position.Y + halfHeight;

            DebugMgr.AddItem(this);
        }

        public override bool Collides(Collider other)
        {
            return other.Collides(this);
        }

        public override bool Collides(CircleColliderInverted other)
        {
            float deltaX = other.Position.X - Math.Max(Position.X - halfWidth, Math.Min(other.Position.X, Position.X + halfWidth));
            float deltaY = other.Position.Y - Math.Max(Position.Y - halfHeight, Math.Min(other.Position.Y, Position.Y + halfHeight));

            return deltaX * deltaX + deltaY * deltaY >= other.Radius * other.Radius;
        }

        public override bool Collides(BoxCollider other)
        {
            float deltaX = Math.Abs(other.Position.X - Position.X);
            float deltaY = Math.Abs(other.Position.Y - Position.Y);

            return (deltaX <= other.halfWidth + halfWidth) && (deltaY <= other.halfHeight + halfHeight);
        }

        public override bool Collides(CircleCollider other)
        {
            float deltaX = other.Position.X - Math.Max(Position.X - halfWidth, Math.Min(other.Position.X, Position.X + halfWidth));
            float deltaY = other.Position.Y - Math.Max(Position.Y - halfHeight, Math.Min(other.Position.Y, Position.Y + halfHeight));

            return deltaX * deltaX + deltaY * deltaY <= other.Radius * other.Radius;
        }

        public override bool Collides(BoxColliderInverted other)
        {
            return other.Collides(this);
        }
        public override bool Contains(Vector2 point)
        {
            return point.X >= Position.X - halfWidth &&
                   point.X <= Position.X + halfWidth &&
                   point.Y >= Position.Y - halfHeight &&
                   point.Y <= Position.Y + halfHeight;
        }
    }
}
