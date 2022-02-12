using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace PourMeSomePuree
{
    abstract class Collider
    {
        protected RigidBody rigidBody;

        public Vector2 Offset;

        public Vector2 Position { get { return rigidBody.Position + Offset; } }
        public RigidBody RigidBody { get { return rigidBody; } }

        public Collider(RigidBody rb)
        {
            rigidBody = rb;
            Offset = Vector2.Zero;
        }

        public abstract bool Collides(Collider other);
        public abstract bool Collides(CircleCollider other);
        public abstract bool Collides(CircleColliderInverted other);
        public abstract bool Collides(BoxCollider other);
        public abstract bool Collides(BoxColliderInverted other);
    }
}
