using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    class Chest : GameObject
    {
        protected Animation animation;
        protected bool isOpen;

        public Chest() : base("chest", 64, 64)
        {
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.25f);

            RigidBody = new RigidBody(this);
            RigidBody.Collider = CollidersFactory.CreateBoxFor(this);

            RigidBody.Type = RigidBodyType.CHEST;
            RigidBody.AddCollisionType(RigidBodyType.PLAYER);

            animation = GfxMgr.AddAnimation("chest", 15, 9, 64, 64, 1, 1, false);
            

            IsActive = true;
            isOpen = false;
        }


        public override void Update()
        {
            if (IsActive && isOpen)
            {
                animation.Update();

                if (animation.IsPlaying == false)
                {
                    isOpen = false;
                }
            }           
        }

        public override void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture, animation.XOffset, animation.YOffset, animation.FrameWidth, animation.FrameHeight);
            }
        }

        public void OpenChest()
        {
            if (isOpen == false)
            {
                animation.Start();
                isOpen = true; 
            }
        }

        public override void OnCollide(GameObject other)
        {
            if (other is Player)
            {
                if (((Player)other).CanOpen)
                {
                    OpenChest();
                }              
            }
        }
    }
}
