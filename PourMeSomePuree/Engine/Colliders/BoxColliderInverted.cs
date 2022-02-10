using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class BoxColliderInverted : Collider
    {
        private float halfWidth;
        private float halfHeight;

        public float HalfWidth { get { return halfWidth; } }
        public float HalfHeight { get { return halfHeight; } }

        public BoxColliderInverted(RigidBody rb, int w, int h) : base(rb)
        {
            halfWidth = w * 0.5f;
            halfHeight = h * 0.5f;

            DebugMgr.AddItem(this);
        }

        public override bool Collides(Collider other)
        {
            return other.Collides(this);
        }

        public override bool Collides(CircleColliderInverted other)
        {
            return true;
        }

        public override bool Collides(BoxCollider other)
        {
            float deltaX = Math.Abs(other.Position.X - Position.X);
            float deltaY = Math.Abs(other.Position.Y - Position.Y);

            return !((deltaX <= -other.HalfWidth + halfWidth) && (deltaY <= -other.HalfHeight + halfHeight));
        }

        public override bool Collides(CircleCollider other)
        {
            float deltaX = other.Position.X - Math.Max(Position.X - halfWidth, Math.Min(other.Position.X, Position.X + halfWidth));
            float deltaY = other.Position.Y - Math.Max(Position.Y - halfHeight, Math.Min(other.Position.Y, Position.Y + halfHeight));

            return deltaX * deltaX + deltaY * deltaY >= other.Radius * other.Radius;
        }

        public override bool Collides(BoxColliderInverted other)
        {
            return true;
        }
    }
}
