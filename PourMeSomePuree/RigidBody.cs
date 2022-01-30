using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace PourMeSomePuree
{
    class RigidBody
    {
        public GameObject Owner;
        public Collider Collider;
        public Vector2 Velocity;
        public bool IsCollisionAffected = true;
        public Vector2 Position { get { return Owner.Position; } }
        public bool IsActive { get { return Owner.IsActive; } }

        public RigidBody(GameObject owner)
        {
            this.Owner = owner;

            PhysicsMngr.AddItem(this);
        }
        public void Update()
        {
            Owner.Position += Velocity * Game.DeltaTime;
        }
        public bool Collides(RigidBody other)
        {
            return Collider.Collides(other.Collider);
        }
    }
}
