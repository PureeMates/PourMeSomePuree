using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class BoxCollider : Collider
    {
        private float actualHalfWidth;
        private float actualHalfHeight;

        private float rotatedHalfWidth;
        private float rotatedHalfHeight;

        private float baseHalfWidth;
        private float baseHalfHeight;

        public float HalfWidth { get { return actualHalfWidth; } }
        public float HalfHeight { get { return actualHalfHeight; } }
        public float RightPos { get; private set; }
        public float LeftPos { get; private set; }
        public float UpPos { get; private set; }
        public float DownPos { get; private set; }

        public BoxCollider(RigidBody rb, int w, int h) : base(rb)
        {
            baseHalfWidth = w * 0.5f;
            baseHalfHeight = h * 0.5f;

            actualHalfWidth = baseHalfWidth;
            actualHalfHeight = baseHalfHeight;

            rotatedHalfWidth = baseHalfHeight;
            rotatedHalfHeight = baseHalfWidth;

            RightPos = Position.X + actualHalfWidth;
            LeftPos = Position.X - actualHalfWidth;
            UpPos = Position.Y - actualHalfHeight;
            DownPos = Position.Y + actualHalfHeight;

            DebugMgr.AddItem(this);
        }

        public override bool Collides(Collider other)
        {
            return other.Collides(this);
        }

        public override bool Contains(Vector2 point)
        {
            return point.X >= Position.X - actualHalfWidth &&
                   point.X <= Position.X + actualHalfWidth &&
                   point.Y >= Position.Y - actualHalfHeight &&
                   point.Y <= Position.Y + actualHalfHeight;
        }

        public override bool Collides(CircleColliderInverted other)
        {
            float deltaX = other.Position.X - Math.Max(Position.X - actualHalfWidth, Math.Min(other.Position.X, Position.X + actualHalfWidth));
            float deltaY = other.Position.Y - Math.Max(Position.Y - actualHalfHeight, Math.Min(other.Position.Y, Position.Y + actualHalfHeight));

            return deltaX * deltaX + deltaY * deltaY >= other.Radius * other.Radius;
        }

        public override bool Collides(BoxCollider other)
        {
            float deltaX = Math.Abs(other.Position.X - Position.X);
            float deltaY = Math.Abs(other.Position.Y - Position.Y);

            return (deltaX <= other.actualHalfWidth + actualHalfWidth) && (deltaY <= other.actualHalfHeight + actualHalfHeight);
        }

        public override bool Collides(CircleCollider other)
        {
            float deltaX = other.Position.X - Math.Max(Position.X - actualHalfWidth, Math.Min(other.Position.X, Position.X + actualHalfWidth));
            float deltaY = other.Position.Y - Math.Max(Position.Y - actualHalfHeight, Math.Min(other.Position.Y, Position.Y + actualHalfHeight));

            return deltaX * deltaX + deltaY * deltaY <= other.Radius * other.Radius;
        }

        public override bool Collides(BoxColliderInverted other)
        {
            return other.Collides(this);
        }
        public void ChangeRotation(Direction dir)
        {
            switch (dir)
            {
                case Direction.DOWN:
                case Direction.UP:
                    actualHalfWidth = baseHalfWidth;
                    actualHalfHeight = baseHalfHeight;
                    break;
                case Direction.RIGHT:
                case Direction.LEFT:
                    actualHalfWidth = rotatedHalfWidth;
                    actualHalfHeight = rotatedHalfHeight;
                    break;
            }
        }
    }
}
