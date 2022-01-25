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

        public static Window Win { get { return window; } }


        public static void Init()
        {
            window = new Window(1920, 1080, "PourMeSomePuree");
           
        }

        public static void Play()
        {
            while (window.IsOpened)
            {
                //INPUT
                

                //UPDATE
               

                //DRAW
                

                window.Update();
            }
        }
    }
}
