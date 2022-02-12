using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace PourMeSomePuree
{
    abstract class Actor : GameObject
    {
        protected Animation animation;

        protected int energy;
        protected float maxSpeed;
        protected int maxEnergy;

        public bool IsAlive { get { return energy > 0; } }
        
        public Actor(string textureName, int w = 0, int h = 0) : base (textureName, w, h)
        {
            RigidBody = new RigidBody(this);
            maxEnergy = 100;
        }

        public virtual void AddDamage(int dmg)
        {
            energy -= dmg;

            if (energy <= 0)
            {
                OnDie();
            }
        }

        public virtual void OnDie() { }

        public virtual void Restore()
        {
            energy = maxEnergy;
        }

        protected virtual void Attack() { }
    }
}
