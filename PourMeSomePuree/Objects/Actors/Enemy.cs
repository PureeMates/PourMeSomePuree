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
        private int id;

        private float nextAttack;
        private float nextMovement;

        public Enemy(int id) : base ("enemy", 64, 64)
        {
            this.id = id;

            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(250.0f, 250.0f); //TODO enemyManager
            maxSpeed = 150.0f;

            RigidBody.Collider = CollidersFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.Enemy;
            RigidBody.AddCollisionType(RigidBodyType.Background);

            AnimationStorage.LoadEnemyAnimations(this.id);
            direction = Direction.DOWN;
            movementAnimation = GfxMgr.GetAnimation($"{this.id}enemyDown");
            attackAnimation = GfxMgr.GetAnimation($"{this.id}enemyAttackDown");
            actualAnimation = movementAnimation;

            maxEnergy = 100;
            Restore();

            nextAttack = RandomGenerator.GetRandomInt(1, 3);
            nextMovement = RandomGenerator.GetRandomInt(0, 4);
            ChangeDirection(nextMovement);

            IsActive = true; //TODO enemyManager
        }

        public override void Update()
        {
            if (IsActive)
            {
                nextAttack -= Game.DeltaTime;
                nextMovement -= Game.DeltaTime;

                if (nextAttack <= 0.0f)
                {
                    nextAttack = RandomGenerator.GetRandomFloat() * 2 + 1;
                    Attack();
                }

                if(nextMovement <= 0.0f)
                {
                    nextMovement = RandomGenerator.GetRandomInt(0, 4);
                    ChangeDirection(nextMovement);
                }

                if (attackAnimation.IsPlaying)
                {
                    actualAnimation = attackAnimation;
                }
                else
                {
                    actualAnimation = movementAnimation;
                }

                base.Update();
            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture, actualAnimation.XOffset, actualAnimation.YOffset, actualAnimation.FrameWidth, actualAnimation.FrameHeight);
            }
        }

        private void CheckMovementAnimation()
        {
            switch (direction)
            {
                case Direction.DOWN:
                    movementAnimation = GfxMgr.GetAnimation($"{id}enemyDown");
                    break;
                case Direction.UP:
                    movementAnimation = GfxMgr.GetAnimation($"{id}enemyUp");
                    break;
                case Direction.RIGHT:
                    movementAnimation = GfxMgr.GetAnimation($"{id}enemyRight");
                    break;
                case Direction.LEFT:
                    movementAnimation = GfxMgr.GetAnimation($"{id}enemyLeft");
                    break;
            }

            movementAnimation.Start();
        }

        public void ChangeDirection(float nextMovement)
        {
            switch (nextMovement)
            {
                case 0:
                    direction = Direction.DOWN;
                    RigidBody.Velocity.Y = maxSpeed;
                    break;
                case 1:
                    direction = Direction.UP;
                    RigidBody.Velocity.Y = -maxSpeed;
                    break;
                case 2:
                    direction = Direction.LEFT;
                    RigidBody.Velocity.X = -maxSpeed;
                    break;
                case 3:
                    direction = Direction.RIGHT;
                    RigidBody.Velocity.X = maxSpeed;
                    break;
            }

            CheckMovementAnimation();
        }

        protected override void Attack()
        {
            switch (direction)
            {
                case Direction.DOWN:
                    attackAnimation = GfxMgr.GetAnimation($"{id}enemyAttackDown");
                    break;
                case Direction.UP:
                    attackAnimation = GfxMgr.GetAnimation($"{id}enemyAttackUp");
                    break;
                case Direction.RIGHT:
                    attackAnimation = GfxMgr.GetAnimation($"{id}enemyAttackRight");
                    break;
                case Direction.LEFT:
                    attackAnimation = GfxMgr.GetAnimation($"{id}enemyAttackLeft");
                    break;
            }

            attackAnimation.Start();
        }

        public override void OnCollide(GameObject other)
        {
            ChangeDirection(RandomGenerator.GetRandomInt(0, 4));
        }

        public override void OnDie()
        {
            IsActive = false;
            base.Destroy();
        }
    }
}
