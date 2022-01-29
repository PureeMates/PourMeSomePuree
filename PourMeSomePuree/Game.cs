using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;


namespace PourMeSomePuree
{

    class Game
    {
        private static Window window;
        static Background background;
        public static Window Win { get { return window; } }


        public static void Init()
        {
            window = new Window(1280, 720, "PourMeSomePuree");
            background = new Background();
        }

        public static void Play()
        {
            while (window.IsOpened)
            {
                //INPUT


                //UPDATE


                //DRAW
                background.Draw();

                window.Update();
            }
        }
    }
}
