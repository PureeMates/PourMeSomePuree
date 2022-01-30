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
        private static Player player;
        public static Window Win { get { return window; } }
        public static float DeltaTime { get { return Win.DeltaTime; } }
        public static void Init()
        {
            window = new Window(1280, 720, "PourMeSomePuree");
            background = new Background("Assets/Background1.png");
           
            player = new Player("Assets/Character.png");       //Debug                           
        }
        
        public static void Play()
        {
            while (window.IsOpened)
            {
                //INPUT
                player.Input();
                //UPDATE
                PhysicsMngr.CheckCollision();
                //DRAW
                background.Draw();
                player.Draw();
                window.Update();
            }
        }

      
    }
}
