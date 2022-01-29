using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    static class Game
    {
        private static Window window;
        private static Texture texture;
        private static Sprite sprite;
        private static Animation animation;

        public static Window Win { get { return window; } }
        public static float DeltaTime { get { return window.DeltaTime; } }

        public static void Init()
        {
            window = new Window(1920, 1080, "PourMeSomePuree");

            texture = new Texture("Assets/Character_SpriteSheet.png"); //Debug
            sprite = new Sprite(64, 64); //Debug
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f); //Debug
            sprite.position = new Vector2(window.Width * 0.5f, window.Height * 0.5f); //Debug
            sprite.scale = new Vector2(5f, 5f); //Debug
            animation = new Animation(12, 4, 64, 64, 5, 4); //Debug
            animation.Start(); //Debug
        }
        
        public static void Play()
        {
            while (window.IsOpened)
            {
                //INPUT
                Quit();

                //UPDATE
                animation.Update(); //Debug

                //DRAW
                sprite.DrawTexture(texture, animation.XOffset, animation.YOffset, (int)sprite.Width, (int)sprite.Height); //Debug

                window.Update();
            }
        }

        public static void Quit()
        {
            if(window.GetKey(KeyCode.Esc))
            {
                window.Exit();
            }
        }
    }
}
