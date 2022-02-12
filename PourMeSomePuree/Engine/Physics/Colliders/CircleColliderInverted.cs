using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class CircleColliderInverted : Collider
    {
        private float radius;

        public float Radius { get { return radius; } }

        public CircleColliderInverted(RigidBody rb, float radius) : base(rb)
        {
            this.radius = radius;

            DebugMgr.AddItem(this);
        }

        public override bool Collides(Collider other)
        {
            return other.Collides(this);
        }

        public override bool Collides(CircleCollider other)
        {
            Vector2 dist = other.Position - Position;
            return dist.LengthSquared >= Math.Pow(radius - other.Radius, 2);
        }

        public override bool Collides(CircleColliderInverted other)
        {
            return true;
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
