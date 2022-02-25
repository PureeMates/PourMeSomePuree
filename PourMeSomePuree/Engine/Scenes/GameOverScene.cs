using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class GameOverScene : Scene
    {
        public GameOverScene() : base() { }

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
