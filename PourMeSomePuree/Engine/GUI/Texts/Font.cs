using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class Font
    {
        private Texture texture;
        
        private int numCol;
        private int firstVal;

        public string TextureName { get; }
        public int CharWidth { get; }
        public int CharHeight { get; }

        public Font(string textureName, string texturePath, int numColumns, int firstCharacterASCIIvalue, int charWidth, int charHeight)
        {
            TextureName = textureName;
            texture = GfxMgr.AddTexture(TextureName, texturePath);

            numCol = numColumns;
            firstVal = firstCharacterASCIIvalue;
            CharWidth = charWidth;
            CharHeight = charHeight;
        }

        public Vector2 GetOffset(char c)
        {
            int cVal = c;
            int delta = cVal - firstVal;
            int x = delta % numCol;
            int y = delta / numCol;

            return new Vector2(x * CharWidth, y * CharHeight);
        }
    }
}
