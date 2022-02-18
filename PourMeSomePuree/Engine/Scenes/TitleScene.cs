using Aiv.Audio;
using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class TitleScene : Scene
    {
        protected Texture texture;
        protected Sprite sprite;

        protected string texturePath;

        public TitleScene(string texturePath) : base()
        {
            this.texturePath = texturePath;
        }

        public override void Start()
        {
            texture = new Texture(texturePath);
            sprite = new Sprite(texture.Width, texture.Height);

            base.Start();
        }

        public override void OnExit()
        {
            texture = null;
            sprite = null;

            base.OnExit();
        }

        public override void Input()
        {
            Quit();
        }
        public override void Update()
        {
            throw new NotImplementedException();
        }
        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
