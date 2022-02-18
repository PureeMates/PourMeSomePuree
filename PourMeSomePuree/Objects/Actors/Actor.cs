using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Audio;

namespace PourMeSomePuree
{
    enum Direction { DOWN, UP, RIGHT, LEFT, LAST }

    abstract class Actor : GameObject
    {
        protected Animation movementAnimation;
        protected Animation attackAnimation;
        protected Animation actualAnimation;
        protected Direction direction;

        protected int energy;
        protected float maxSpeed;
        protected int maxEnergy;
        protected bool isAttackPressed;
        protected bool isMoving;

        public bool IsAlive { get { return energy > 0; } }

        public Actor(string textureName, int w = 0, int h = 0) : base (textureName, w, h)
        {
            RigidBody = new RigidBody(this);
            maxEnergy = 100;

            audioSource = new AudioSource();
        }

        public override void Update()
        {
            actualAnimation.Update();
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
