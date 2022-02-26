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

        protected bool isPickedUp;

        protected int addCoins;

        public Coin() : base("coin", 32, 32)
        {
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.25f, Game.Win.Height * 0.5f);

            RigidBody = new RigidBody(this);
            RigidBody.Collider = CollidersFactory.CreateCircleFor(this);

            RigidBody.Type = RigidBodyType.Coin;
            RigidBody.AddCollisionType(RigidBodyType.Player);

            AnimationStorage.LoadCoinAnimations();
            animation = GfxMgr.GetAnimation("rotationCoin");

            animation.Start();

            addCoins = 1;

            IsActive = true;
            isPickedUp = false;
        }

        public override void Update()
        {
            animation.Update();
        }
        
        public override void Draw()
        {
            if (isPickedUp == false)
            {
                sprite.DrawTexture(texture, animation.XOffset, animation.YOffset, animation.FrameWidth, animation.FrameHeight);
            }
        }

        public override void OnCollide(GameObject other)
        {
            if (other is Player)
            {
                IsActive = false;
                isPickedUp = true;
                ((Player)other).AddCoin(addCoins);
            }
        }
    }
}
