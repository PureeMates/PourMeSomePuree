using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace PourMeSomePuree
{
    class Sword : GameObject
    {
        protected int offset;
        protected Actor owner;
        public Sword(Actor owner) : base(spriteWidth: 50, spriteHeight: 40)
        {
            this.owner = owner;
            offset = 30;
            RigidBody = new RigidBody(this);
            sprite.position = new Vector2(owner.Position.X, owner.Position.Y + offset);
            RigidBody.AddCollisionType(RigidBodyType.ENEMY | RigidBodyType.PLAYER);
            RigidBody.Collider = CollidersFactory.CreateBoxFor(this, 88, 28);
            IsActive = false;
        }

        public override void Update()
        {
            switch (owner.Dir)
            {
                case Direction.DOWN:
                    sprite.position = new Vector2(owner.Position.X, owner.Position.Y + offset);
                    break;
                case Direction.UP:
                    sprite.position = new Vector2(owner.Position.X, owner.Position.Y - offset);
                    break;
                case Direction.RIGHT:
                    sprite.position = new Vector2(owner.Position.X + offset, owner.Position.Y);
                    break;
                case Direction.LEFT:
                    sprite.position = new Vector2(owner.Position.X - offset, owner.Position.Y);
                    break;
            }
        }

        public void ActiveSword()
        {
            ((BoxCollider)RigidBody.Collider).ChangeRotation(owner.Dir);
            IsActive = true;
        }

        public void DeactiveSword()
        {
            IsActive = false;
        }
        public override void OnCollide(GameObject other)
        {
            if (other is Enemy)
            {
                
            }
        }
    }
}
