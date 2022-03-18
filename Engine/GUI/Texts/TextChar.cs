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
        public Vector4 Color { set { ChangeColor(value); } }

        public TextChar(Vector2 position, char character, Font font) : base(font.TextureName, font.CharWidth * 0.55f, font.CharHeight * 0.55f)
        {
            Position = position;
            this.font = font;
            Character = character;
            Pivot = Vector2.Zero;

            IsActive = true;
        }

        private void ComputeOffset()
        {
            textureOffset = font.GetOffset(character);
        }

        private void ChangeColor(Vector4 color)
        {
            sprite.SetMultiplyTint(color);
        }

        public override void Draw()
        {
            sprite.DrawTexture(texture, (int)textureOffset.X, (int)textureOffset.Y, (int)font.CharWidth, (int)font.CharHeight);
        }
    }
}
