using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    static class CollidersFactory
    {
        public static BoxCollider CreateBoxFor(GameObject obj, int colliderWidth = 0, int colliderHeight = 0)
        {
            int colliderW = (int)(colliderWidth != 0 ? colliderWidth : obj.HalfWidth * 2);
            int colliderH = (int)(colliderHeight != 0 ? colliderHeight : obj.HalfHeight * 2);

            return new BoxCollider(obj.RigidBody, colliderW, colliderH);
        }

        public static BoxColliderInverted CreateInvertedBoxFor(GameObject obj, int colliderWidth = 0, int colliderHeight = 0)
        {
            int colliderW = (int)(colliderWidth != 0 ? colliderWidth : obj.HalfWidth * 2);
            int colliderH = (int)(colliderHeight != 0 ? colliderHeight : obj.HalfHeight * 2);

            return new BoxColliderInverted(obj.RigidBody, colliderW, colliderH);
        }

        public static CircleCollider CreateCircleFor(GameObject obj, bool isInnerCircle = true)
        {
            return new CircleCollider(obj.RigidBody, CalculateRadius(obj, isInnerCircle));
        }

        public static CircleColliderInverted CreateInvertedCircleFor(GameObject obj, bool isInnerCircle = true)
        {
            return new CircleColliderInverted(obj.RigidBody, CalculateRadius(obj, isInnerCircle));
        }

        private static float CalculateRadius(GameObject obj, bool isInnerCircle)
        {
            float radius;

            if (isInnerCircle)
            {
                if (obj.HalfWidth >= obj.HalfHeight)
                {
                    radius = obj.HalfHeight;
                }
                else
                {
                    radius = obj.HalfWidth;
                }
            }
            else
            {
                radius = (float)Math.Sqrt(obj.HalfWidth * obj.HalfWidth + obj.HalfHeight * obj.HalfHeight);
            }

            return radius;
        }
    }
}
