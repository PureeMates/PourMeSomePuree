using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    static class UpdateMgr
    {
        private static List<I_Updatable> items;

        static UpdateMgr()
        {
            items = new List<I_Updatable>();
        }

        public static void AddItem(I_Updatable item)
        {
            items.Add(item);
        }

        public static void RemoveItem(I_Updatable item)
        {
            items.Remove(item);
        }

        public static void ClearAll()
        {
            items.Clear();
        }

        public static void Update()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Update();
            }
        }
    }
}
