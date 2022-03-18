using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace PourMeSomePuree
{
    class Coin : GameObject
    {
        protected Animation animation;

        private int id;
        protected int cashToAdd;

        public Coin(int id) : base("coin", 32, 32)
        {
            this.id = id;
            sprite.scale = new Vector2(1.75f);

            RigidBody = new RigidBody(this);
            RigidBody.Collider = CollidersFactory.CreateCircleFor(this);

            RigidBody.Type = RigidBodyType.COIN;
            RigidBody.AddCollisionType(RigidBodyType.PLAYER);

            animation = GfxMgr.AddAnimation($"{this.id}rotationCoin", 15, 8, 32, 32, 1, 1);

            animation.Start();

            cashToAdd = 1;
        }

        public override void Update()
        {
            if(IsActive)
            {
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

        public override void OnCollide(GameObject other)
        {
            IsActive = false;
            ((Player)other).AddCoins(cashToAdd);
        }
    }
}
