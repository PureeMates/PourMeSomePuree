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
        private float nextSword;

        public Enemy() : base ("enemy")
        {
            maxSpeed = 170;

            RigidBody.Velocity.X = maxSpeed;
            RigidBody.Velocity.Y = maxSpeed;

            RigidBody.Collider = CollidersFactory.CreateBoxFor(this);

            nextSword = RandomGenerator.GetRandomInt(1, 3);
        }

        public override void Update()
        {
            if (IsActive)
            {
                if (sprite.position.X + HalfWidth < 0)
                {
                    //Spawn
                }
                else
                {
                    nextSword -= Game.DeltaTime;

                    if (nextSword <= 0)
                    {
                        nextSword = RandomGenerator.GetRandomFloat() * 2 + 1;
                        Sword();
                    }
                }
            }
        }
    }
}
