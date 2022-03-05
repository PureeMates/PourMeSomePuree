using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class TextChar : GameObject
    {
        private Font font;

        private char character;
        private Vector2 textureOffset;

        public char Character { get { return character; } set { character = value; ComputeOffset(); } }

        public TextChar(Vector2 position, char character, Font font) : base(font.TextureName, font.CharWidth, font.CharHeight)
        {
            this.font = font;
            Position = position;
            Pivot = Vector2.Zero;
            this.character = character;

            IsActive = true;
        }

        private void ComputeOffset()
        {
            textureOffset = font.GetOffset(character);
        }

        public override void Draw()
        {
            sprite.DrawTexture(texture, (int)textureOffset.X, (int)textureOffset.Y, font.CharWidth, font.CharHeight);
        }
    }
}
