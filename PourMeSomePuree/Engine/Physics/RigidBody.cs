using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace PourMeSomePuree
{
    enum RigidBodyType { PLAYER = 1, BACKGROUND = 2, ENEMY = 4 , COIN = 8, POT = 16 }

    class RigidBody
    {
        private GameObject owner;
        private uint collisionMask;

        public Collider Collider;
        public Vector2 Velocity;
        public bool IsCollisionAffected = true;
        public RigidBodyType Type;

        public GameObject Owner { get { return owner; } }
        public Vector2 Position { get { return Owner.Position; } }

        public RigidBody(GameObject owner)
        {
            this.owner = owner;

            PhysicsMgr.AddItem(this);
        }

        public void Update()
        {
            Owner.Position += Velocity * Game.DeltaTime;
        }

        public bool Collides(RigidBody other)
        {
            return Collider.Collides(other.Collider);
        }

        public void AddCollisionType(RigidBodyType type)
        {
            collisionMask |= (uint)type;
        }

        public bool CollisionTypeMatches(RigidBodyType type)
        {
            return (collisionMask & (uint)type) != 0;
        }
    }
}
