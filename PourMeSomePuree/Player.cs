using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    class Player : GameObject
    {
        public Player(string texturePath, int spriteWidth = 0, int spriteHeight = 0) : base(texturePath, spriteWidth, spriteHeight)
        {
            sprite.scale = new Vector2(1.75f);
            Position = new Vector2(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f);
            maxSpeed = 500f;
            IsActive = true;
            
            RigidBody = new RigidBody(this);
            RigidBody.Collider = new CircleCollider(RigidBody, isInverted : true);
        }
    }
}
