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
        protected bool isReturnPressed; // DEBUG

        public virtual int Energy { get { return energy; } set { energy = MathHelper.Clamp(value, 0, maxEnergy); } }
        public bool IsAlive { get { return energy > 0; } }
        
        public Actor(string textureName, int w = 0, int h = 0) : base (textureName, w, h)
        {
            RigidBody = new RigidBody(this);
            audioSource = new AudioSource();
        }

        public override void Update()
        {
            actualAnimation.Update();
        }

        public virtual void AddDamage(int dmg)
        {
            isReturnPressed = true;

            Energy -= dmg;

            if (Energy <= 0)
            {
                OnDie();
            }
        }

        public override void Destroy()
        {
            actualAnimation = null;
            movementAnimation = null;
            attackAnimation = null;

            base.Destroy();
        }

        public virtual void Restore()
        {
            Energy = maxEnergy;
        }

        protected virtual void Attack() { }
    }
}
