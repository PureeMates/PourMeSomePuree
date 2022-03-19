using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class Door : GameObject
    {
        private int id;

        private Animation animation;

        private Queue<Enemy> enemies;
        private Vector2 offset;
        private int enemyStartingDirection;

        private float timer;
        private float crono;
        private float nextSpawn;

        private bool isAnimationPaused;
        private bool isSpawnStopped;

        public Door(int id, int numEnemies, ref int enemyId, Vector2 position) : base("door", 96, 80)
        {
            this.id = id;

            sprite.scale = new Vector2(1.75f);
            Position = position;

            if(position.Y >= 0 && position.Y <= HalfHeight * 2)
            {
                enemyStartingDirection = 0;
                offset = new Vector2(0.0f, 15.0f);
            }
            else if(position.Y <= Game.Win.Height && position.Y >= Game.Win.Height - HalfHeight * 2)
            {
                sprite.Rotation = MathHelper.Pi;
                enemyStartingDirection = 1;
                offset = new Vector2(0.0f, -15.0f);
            }
            else if(position.X >= 0 && position.X <= HalfWidth * 2)
            {
                sprite.Rotation = MathHelper.ThreePiOver2;
                enemyStartingDirection = 3;
                offset = new Vector2(15.0f, 0.0f);
            }
            else if(position.X <= Game.Win.Width && position.X >= Game.Win.Width - HalfWidth * 2)
            {
                sprite.Rotation = MathHelper.PiOver2;
                enemyStartingDirection = 2;
                offset = new Vector2(-15.0f, 0.0f);
            }

            animation = GfxMgr.AddAnimation($"{this.id}doorOpen", 6, 4, 96, 80, loop:false, pingPong: true);

            enemies = new Queue<Enemy>();
            for (int i = 0; i < numEnemies; i++)
            {
                enemies.Enqueue(new Enemy(enemyId++, Position + offset, enemyStartingDirection));
            }
            timer = RandomGenerator.GetRandomInt(2, 10);
            crono = timer;
            nextSpawn = 0.0f;

            IsActive = true;
        }

        public override void Update()
        {
            if (IsActive)
            {
                if (!isSpawnStopped)
                {
                    if (crono > 0.0f)
                    {
                        crono -= Game.DeltaTime;

                        if (crono <= 0.0f)
                        {
                            animation.Start();
                        }
                    }

                    if (animation.CurrentFrame == 3 && !isAnimationPaused)
                    {
                        isAnimationPaused = true;
                        animation.Pause();
                    }

                    if (isAnimationPaused)
                    {
                        nextSpawn -= Game.DeltaTime;

                        if (nextSpawn <= 0.0f)
                        {
                            Spawn();
                            nextSpawn = RandomGenerator.GetRandomFloat() + 1.7f;
                        }
                    } 
                }

                animation.Update(); 
            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture, animation.XOffset, animation.YOffset, animation.FrameWidth, animation.FrameHeight);
            }
        }

        private void Spawn()
        {
            if(enemies.Count > 0)
            {
                Enemy enemy = enemies.Dequeue();
                enemy.IsActive = true;
            }
            else
            {
                animation.Start();
                StopSpawn();
            }
        }

        public void Restart()
        {
            isSpawnStopped = false;
        }

        private void StopSpawn()
        {
            timer = RandomGenerator.GetRandomInt(2, 10);
            crono = timer;
            nextSpawn = 0.0f;

            isSpawnStopped = true;
        }
    }
}
