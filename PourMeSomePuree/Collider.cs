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
        public Vector2 Position { get { return rigidBody.Position; } }
        public Collider(RigidBody rb)
        {
            rigidBody = rb;
        }

        public abstract bool Collides(Collider collider);
        public abstract bool Collides(CircleCollider collider);
        public abstract bool Collides(CircleColliderInverted collider);
    }
}
