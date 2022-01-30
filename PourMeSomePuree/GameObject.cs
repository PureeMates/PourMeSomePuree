using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;
namespace PourMeSomePuree
{
    class GameObject        //Debug
    {
        protected Sprite sprite;
        protected Texture texture;
        protected float maxSpeed;

        public RigidBody RigidBody;
        public bool IsActive;

        public Vector2 Position { get { return sprite.position; } set{ sprite.position = value; } }

        public float HalfWidth { get { return sprite.Width * 0.5f;} }
        public float HalfHeight { get { return sprite.Height * 0.5f;} }

        public GameObject(string texturePath, int spriteWidth = 0, int spriteHeight = 0)
        {
            texture = new Texture(texturePath);

            int spriteW = spriteWidth != 0 ? spriteWidth : texture.Width;
            int spriteH = spriteHeight != 0 ? spriteHeight : texture.Height;

            sprite = new Sprite(spriteW, spriteH);
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
        }

        public virtual void Update()
        {

        }

        public virtual void OnCollide(GameObject other)
        {

        }
        public virtual void Draw()
        {
            sprite.DrawTexture(texture);
        }
    }
}
