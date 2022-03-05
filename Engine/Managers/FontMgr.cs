using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    static class FontMgr
    {
        private static Dictionary<string, Font> fonts;

        private static Font defaultFont;

        static FontMgr()
        {
            fonts = new Dictionary<string, Font>();
        }

        public static Font AddFont(string textureName, string texturePath, int numColumns, int firstCharacterASCIIvalue, int charWidth, int charHeight)
        {
            Font f = new Font(textureName, texturePath, numColumns, firstCharacterASCIIvalue, charWidth, charHeight);

            if(f != null)
            {
                fonts.Add(textureName, f);
            }

            if(defaultFont == null)
            {
                defaultFont = f;
            }

            return f;
        }

        public static Font GetFont(string fontName = "")
        {
            if(fontName != "" && fonts.ContainsKey(fontName))
            {
                return fonts[fontName];
            }

            return defaultFont;
        }

        public static void ClearAll()
        {
            fonts.Clear();
            defaultFont = null;
        }
    }
}
