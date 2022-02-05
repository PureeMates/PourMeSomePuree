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
        //private bool isInverted;
        public float Radius;

        public CircleCollider(RigidBody rb,bool isInner = true, bool isInverted = false) : base(rb)
        {
            //this.isInverted = isInverted;
            if (isInner)
            {
                if (rigidBody.Owner.HalfWidth > rigidBody.Owner.HalfHeight)
                {
                    Radius = rb.Owner.HalfHeight;
                }
                else
                {
                    Radius = rb.Owner.HalfWidth;
                }
            }
            else
            {
                Radius = (float)Math.Sqrt(rb.Owner.HalfWidth * rb.Owner.HalfWidth +
                                          rb.Owner.HalfHeight * rb.Owner.HalfHeight);
            }
        }
        public override bool Collides(Collider other)
        {
            return other.Collides(this);
        }

        public override bool Collides(CircleCollider other)
        {
            Vector2 dist = other.Position - Position;

            /*if (isInverted)
            {
                return (dist.LengthSquared >= Math.Pow(Radius + other.Radius, 2));
            }*/
            //else
            //{
            return (dist.LengthSquared <= Math.Pow(Radius + other.Radius, 2));
            //}
        }
    }
}
