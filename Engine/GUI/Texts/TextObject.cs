using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class TextObject
    {
        private Font font;
        private List<TextChar> sprites;

        private string text;
        private int hSpace;
        private Vector2 position;
        private bool isActive;

        public bool IsActive { get { return isActive; } set { isActive = value; UpdateCharStatus(value); } }
        public string Text { get { return text; } set { SetText(value); } }

        public TextObject(Vector2 position, string textString = "", Font font = null, int horizontalSpace = 0)
        {
            sprites = new List<TextChar>();
            hSpace = horizontalSpace;

            this.position = position;

            if(font == null)
            {
                font = FontMgr.GetFont();
            }
            this.font = font;

            if(textString != "")
            {
                SetText(textString);
            }
        }

        private void SetText(string newText)
        {
            if(newText != text)
            {
                text = newText;

                int charX = (int)position.X;
                int charY = (int)position.Y;

                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];

                    if(i > sprites.Count - 1)
                    {
                        TextChar tc = new TextChar(new Vector2(charX, charY), c, font);
                        sprites.Add(tc);
                    }
                    else if(c != sprites[i].Character)
                    {
                        sprites[i].Character = c;
                    }

                    charX += (int)sprites[i].HalfWidth * 2 + hSpace;
                }

                if(text.Length < sprites.Count)
                {
                    int count = sprites.Count - text.Length;
                    int startCut = text.Length;

                    for (int i = startCut; i < sprites.Count; i++)
                    {
                        sprites[i].Destroy();
                    }
                    sprites.RemoveRange(startCut, count);
                }
            }
        }

        private void UpdateCharStatus(bool activeStatus)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].IsActive = activeStatus;
            }
        }
        public void SetColor(Vector4 color)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].Color = color;
            }
        }
    }
}
