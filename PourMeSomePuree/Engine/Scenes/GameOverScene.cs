using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class GameOverScene : TitleScene
    {
        public GameOverScene(string texturePath) : base(texturePath) { }

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
