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

        public Door(int id) : base("door", 96, 80)
        {
            this.id = id;

            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.5f, sprite.Height - 21.5f); //TODO SpawnManager

            animation = GfxMgr.AddAnimation($"{this.id}doorOpen", 6, 4, 96, 80, loop:false);

            animation.Start(); //TODO spawnManager

            IsActive = true;
        }

        public override void Update()
        {
            if (IsActive)
            {
                if (animation.CurrentFrame == 3)
                {
                    animation.Pause();
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
    }
}
