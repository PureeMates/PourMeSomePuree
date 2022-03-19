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

        protected bool isDamageDone;

        public Sword(Actor owner) : base()
        {
            this.owner = owner;
            offset = 30;
            RigidBody = new RigidBody(this);
            sprite.position = new Vector2(owner.Position.X, owner.Position.Y + offset);
            RigidBody.AddCollisionType(RigidBodyType.ENEMY | RigidBodyType.PLAYER);
            RigidBody.Collider = CollidersFactory.CreateBoxFor(this, 88, 28);
            RigidBody.IsCollisionAffected = false;
            IsActive = true;
        }

        public override void Update()
        {
            if (IsActive)
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
        }

        public void ActiveSword()
        {
            if (IsActive)
            {
                ((BoxCollider)RigidBody.Collider).ChangeRotation(owner.Dir);
                RigidBody.IsCollisionAffected = true;  
            }
        }

        public void DeactiveSword()
        {
            if (IsActive)
            {
                RigidBody.IsCollisionAffected = false;
                isDamageDone = false;
            }
        }

        public override void OnCollide(GameObject other)
        {
            if (owner is Player && other is Enemy)
            {
                other.OnDie();
            }
            else if(owner is Enemy && other is Player)
            {
                Player player = (Player)other;
                Enemy enemy = (Enemy)owner;

                if(!isDamageDone && player != null)
                {
                    isDamageDone = true;
                    player.AddDamage(enemy.Damage, player.Defence);
                }
            }
        }
    }
}
