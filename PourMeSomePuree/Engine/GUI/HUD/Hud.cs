using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    class Hud : GameObject
    {
        private Texture textureMaskAvatar;
        private Texture textureMaskEnergy;
        private Texture textureMaskStamina;
        private Texture texturePortrait;

        private Sprite spriteMaskAvatar;
        private Sprite spriteMaskEnergy;
        private Sprite spriteMaskStamina;
        private Sprite spritePortrait;

        private Vector2 offSetMaskAvatar;
        private Vector2 offSetMaskEnergy;
        private Vector2 offSetMaskStamina;
        private Vector2 offSetPortrait;

        private float energyBarWidth;

        private float staminaBarWidth;

        public override Vector2 Position 
        { 
            get => base.Position;
            set
            {
                base.Position = value;
                spriteMaskAvatar.position = value + offSetMaskAvatar;
                spriteMaskEnergy.position = value + offSetMaskEnergy;
                spriteMaskStamina.position = value + offSetMaskStamina;
                spritePortrait.position = value + offSetPortrait;
            }    
        }

        public Hud(string textureMaskAvatarName, string textureMaskEnergyName, string textureMaskStaminaName, string texturePortraitName, Vector2 offSetMaskAvatar, Vector2 offSetMaskEnergy, Vector2 offSetMaskStamina, Vector2 offSetPortrait) : base("hud")
        {
            textureMaskAvatar = GfxMgr.GetTexture(textureMaskAvatarName);
            textureMaskEnergy = GfxMgr.GetTexture(textureMaskEnergyName);
            textureMaskStamina = GfxMgr.GetTexture(textureMaskStaminaName);
            texturePortrait = GfxMgr.GetTexture(texturePortraitName);

            spriteMaskAvatar = new Sprite(textureMaskAvatar.Width, textureMaskAvatar.Height);
            spriteMaskEnergy = new Sprite(textureMaskEnergy.Width, textureMaskEnergy.Height);
            spriteMaskStamina = new Sprite(textureMaskStamina.Width, textureMaskStamina.Height);
            spritePortrait = new Sprite(texturePortrait.Width, texturePortrait.Height);

            this.offSetMaskAvatar = offSetMaskAvatar;
            this.offSetMaskEnergy = offSetMaskEnergy;
            this.offSetMaskStamina = offSetMaskStamina;
            this.offSetPortrait = offSetPortrait;

            Pivot = Vector2.Zero;
                        
            energyBarWidth = spriteMaskEnergy.Width;
            staminaBarWidth = spriteMaskStamina.Width;

            IsActive = true;
        }

        public override void Draw()
        {
            base.Draw();
            spriteMaskAvatar.DrawTexture(textureMaskAvatar);
            spriteMaskEnergy.DrawTexture(textureMaskEnergy, 0, 0, (int)energyBarWidth, (int)spriteMaskEnergy.Height );
            spriteMaskStamina.DrawTexture(textureMaskStamina, 0, 0, (int)staminaBarWidth, (int)spriteMaskStamina.Height);
            spritePortrait.DrawTexture(texturePortrait);
        }

        public void ScaleEnergy(float energyScale)
        {
            energyScale = MathHelper.Clamp(energyScale, 0, 1);

            spriteMaskEnergy.scale.X = energyScale;
            energyBarWidth = spriteMaskEnergy.Width * energyScale;

            float redMultiplier = 50.0f;
            float greenMultiplier = 2.0f;
            float blueMultiplier = 0.5f;
            spriteMaskEnergy.SetMultiplyTint((1.0f - energyScale) * redMultiplier, energyScale * greenMultiplier, energyScale * blueMultiplier, 1.0f);
            spriteMaskAvatar.SetMultiplyTint((1.0f - energyScale) * redMultiplier, energyScale * greenMultiplier, energyScale * blueMultiplier, 1.0f);
        }

        public void ScaleStamina(float staminaScale)
        {
            staminaScale = MathHelper.Clamp(staminaScale, 0, 1);

            spriteMaskStamina.scale.X = staminaScale;
            staminaBarWidth = spriteMaskStamina.Width * staminaScale;

            float redMultiplier = 0.5f;
            float greenMultiplier = 1.5f;
            float blueMultiplier = 50.0f;
            spriteMaskStamina.SetMultiplyTint(staminaScale * redMultiplier, staminaScale * greenMultiplier, (1.0f - staminaScale) * blueMultiplier, 1.0f);            
        }
    }
}
