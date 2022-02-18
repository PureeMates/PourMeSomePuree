using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    static class DebugMgr
    {
        private static List<Collider> items;
        private static List<Sprite> sprites;

        static DebugMgr()
        {
            items = new List<Collider>();
            sprites = new List<Sprite>();
        }

        public static void AddItem(Collider c)
        {
            items.Add(c);

            if(c is CircleCollider)
            {
                Sprite sprite = new Sprite(((CircleCollider)c).Radius * 2, ((CircleCollider)c).Radius * 2);
                sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
                sprites.Add(sprite);
            }
            else if (c is CircleColliderInverted)
            {
                Sprite sprite = new Sprite(((CircleColliderInverted)c).Radius * 2, ((CircleColliderInverted)c).Radius * 2);
                sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
                sprites.Add(sprite);
            }
            else if(c is BoxCollider)
            {
                Sprite sprite = new Sprite(((BoxCollider)c).HalfWidth * 2, ((BoxCollider)c).HalfHeight * 2);
                sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
                sprites.Add(sprite);
            }
            else if (c is BoxColliderInverted)
            {
                Sprite sprite = new Sprite(((BoxColliderInverted)c).HalfWidth * 2, ((BoxColliderInverted)c).HalfHeight * 2);
                sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
                sprites.Add(sprite);
            }
            else
            {
                Sprite sprite = new Sprite(c.RigidBody.Owner.HalfWidth * 2, c.RigidBody.Owner.HalfHeight * 2);
                sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
                sprites.Add(sprite);
            }
        }

        public static void RemoveItem(Collider c)
        {
            int index = items.IndexOf(c);
            sprites.RemoveAt(index);
            items.Remove(c);
        }

        public static void ClearAll()
        {
            items.Clear();
            sprites.Clear();
        }

        public static void Draw()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].RigidBody.Owner.IsActive)
                {
                    Vector4 color = new Vector4();

                    if(items[i] is CircleCollider)
                    {
                        color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                    }
                    else if(items[i] is BoxCollider)
                    {
                        color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
                    }
                    else if (items[i] is CircleColliderInverted)
                    {
                        color = new Vector4(0.5f, 0.5f, 0.0f, 1.0f);
                    }
                    else if (items[i] is BoxColliderInverted)
                    {
                        color = new Vector4(0.0f, 0.5f, 0.5f, 1.0f);
                    }
                    else
                    {
                        color = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
                    }

                    sprites[i].position = items[i].Position + items[i].Offset;
                    sprites[i].DrawWireframe(color);
                }
            }
        }
    }
}
