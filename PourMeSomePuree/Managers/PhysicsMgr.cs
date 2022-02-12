using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    static class PhysicsMgr
    {
        private static List<RigidBody> items;

        static PhysicsMgr()
        {
            items = new List<RigidBody>();
        }

        public static void Update()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Owner.IsActive)
                {
                    items[i].Update();
                }
            }
        }

        public static void CheckCollision()
        {
            for (int i = 0; i < items.Count - 1; i++)
            {
                if (items[i].Owner.IsActive && items[i].IsCollisionAffected)
                {
                    for (int j = 1 + i; j < items.Count; j++)
                    {
                        if (items[j].Owner.IsActive && items[j].IsCollisionAffected)
                        {
                            bool firstCheck = items[i].CollisionTypeMatches(items[j].Type);
                            bool secondCheck = items[j].CollisionTypeMatches(items[i].Type);

                            if((firstCheck || secondCheck) && items[i].Collides(items[j]))
                            {
                                if(firstCheck)
                                {
                                    items[i].Owner.OnCollide(items[j].Owner);
                                }
                                if(secondCheck)
                                {
                                    items[j].Owner.OnCollide(items[i].Owner);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void AddItem(RigidBody item)
        {
            items.Add(item);
        }
        public static void RemoveItem(RigidBody item)
        {
            items.Remove(item);
        }
        public static void ClearAll()
        {
            items.Clear();
        }
    }
}
