using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    static class CollidersFactory
    {
        public static CircleCollider CreateCircleFor(GameObject obj, bool isInnerCircle = true)
        {
            return new CircleCollider(obj.RigidBody, CalculateRadius(obj, isInnerCircle));
        }

        public static CircleColliderInverted CreateCircleInvertedFor(GameObject obj, bool isInnerCircle = true)
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
