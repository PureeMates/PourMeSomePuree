using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace PourMeSomePuree
{
    class Pot : GameObject
    {
        private int id;

        private Animation animation;
        private Animation actualAnimation;

        private bool destroyed;

        private float counter;
        private float restartCounter;

        public Pot(int id, Vector2 pos) : base("pot", 64, 64)
        {
            this.id = id;

            sprite.scale = new Vector2(1.75f);
            Position = pos;

            RigidBody = new RigidBody(this);
            RigidBody.Collider = CollidersFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.POT;
            RigidBody.AddCollisionType(RigidBodyType.PLAYER);

            animation = GfxMgr.AddAnimation($"{this.id}breaking", 15, 4, 64, 64, 1, 1, false);
            actualAnimation = animation;

            destroyed = false;

            restartCounter = 60.0f;
            counter = restartCounter;

            IsActive = true;
        }

        public override void Update()
        {
            counter -= Game.DeltaTime;

            if(counter <= 0.0f)
            {
                counter = restartCounter;
                IsActive = true;
                destroyed = false;
            }

            actualAnimation.Update();
        }

        public override void Draw()
        {
            if (!destroyed || animation.IsPlaying)
            {
                sprite.DrawTexture(texture, actualAnimation.XOffset, actualAnimation.YOffset, actualAnimation.FrameWidth, actualAnimation.FrameHeight);
            }
        }

        public override void OnCollide(GameObject other)
        {
            Player player = (Player)other;
            if (!animation.IsPlaying && destroyed == false)
            {
                animation.Start();
                destroyed = true;
                IsActive = false;
            }

            player.Restore();
        }
    }
}
