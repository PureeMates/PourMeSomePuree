using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    class ProgressBar : GameObject
    {
        private float barWidth;

        public ProgressBar() : base("hudMask")
        {
            sprite.scale = new Vector2(1.5f);
            Pivot = Vector2.Zero;
            Position = new Vector2(10.0f, 10.0f);

            barWidth = sprite.Width;

            IsActive = true;
        }

        public override void Draw()
        {
            sprite.DrawTexture(texture, 0, 0, (int)barWidth, (int)sprite.Height);
        }

        public void Scale(float scale)
        {
            scale = MathHelper.Clamp(scale, 0, 1);

            sprite.scale.X = scale;
            barWidth = sprite.Width * scale;

            float redMultiplier = 50.0f;
            float greenMultiplier = 2.0f;
            float blueMultiplier = 0.5f;
            sprite.SetMultiplyTint((1.0f - scale) * redMultiplier, scale * greenMultiplier, scale * blueMultiplier, 1.0f);
        }
    }
}
