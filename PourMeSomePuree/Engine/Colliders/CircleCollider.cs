using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace PourMeSomePuree
{
    class CircleCollider : Collider
    {
        private Sprite sprite;
        private float radius;

        public float Radius { get { return radius; } }

        public CircleCollider(RigidBody rb, float radius) : base(rb)
        {
            this.radius = radius;
            sprite = new Sprite(radius * 2, radius * 2);
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            sprite.position = rb.Owner.Position;

            DebugMgr.AddItem(this);
        }

        public override bool Collides(Collider other)
        {
            return other.Collides(this);
        }

        public override bool Collides(CircleColliderInverted other)
        {
            return other.Collides(this);
        }

        public override bool Collides(CircleCollider other)
        {
            Vector2 dist = other.Position - Position;
            return dist.LengthSquared <= Math.Pow(radius + other.radius, 2);
        }

        public override bool Collides(BoxCollider other)
        {
            return other.Collides(this);
        }

        public override bool Collides(BoxColliderInverted other)
        {
            return other.Collides(this);
        }
    }
}
