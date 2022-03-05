using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    static class DrawMgr
    {
        private static List<I_Drawable> items;

        static DrawMgr()
        {
            items = new List<I_Drawable>();
        }

        public static void AddItem(I_Drawable item)
        {
            items.Add(item);
        }

        public static void RemoveItem(I_Drawable item)
        {
            items.Remove(item);
        }

        public static void ClearAll()
        {
            items.Clear();
        }

        public static void Draw()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Draw();
            }
        }
    }
}
