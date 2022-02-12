using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace PourMeSomePuree
{
    class Enemy : Actor
    {
        private float nextAttack;

        public Enemy() : base ("enemy")
        {
            maxSpeed = 170;

            RigidBody.Velocity.X = maxSpeed;
            RigidBody.Velocity.Y = maxSpeed;

            RigidBody.Collider = CollidersFactory.CreateBoxFor(this);

            nextAttack = RandomGenerator.GetRandomInt(1, 3);
        }

        public override void Update()
        {
            if (IsActive)
            {
                nextAttack -= Game.DeltaTime;

                if (nextAttack <= 0.0f)
                {
                    nextAttack = RandomGenerator.GetRandomFloat() * 2 + 1;
                    Attack();
                }
            }
        }
    }
}
