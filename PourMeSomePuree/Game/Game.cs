using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;
using Aiv.Audio;

namespace PourMeSomePuree
{
    static class Game
    {
        private static Window window;
        private static TitleScene titleScene;
        private static PlayScene playScene;

        private static AudioDevice playerEar; 
        private static AudioSource bgAudio;
        private static AudioClip bgAudioClip;

        public static Window Win { get { return window; } }
        public static float DeltaTime { get { return Win.DeltaTime; } }
        public static Vector3 PlayerEarPosition { get { return playerEar.Position; } set { playerEar.Position = value; } }

        public static void Init()
        {
            //CANVAS
            window = new Window(1280, 720, "PourMeSomePuree");
            window.SetVSync(false);

            //BACKGROUND AUDIO
            playerEar = new AudioDevice();
            bgAudio = new AudioSource();
            bgAudio.Volume = 0.06f;
            bgAudioClip = AudioMgr.AddClip("background", "Assets/Audio/Horde_theme.ogg");

            //SCENES
            titleScene = new TitleScene();
            playScene = new PlayScene();
            //GameOverScene gameOverScene = new GameOverScene();
        }

        public static void Play()
        {
            SceneMgr.Start();

            while (window.IsOpened)
            {
                window.SetTitle($"FPS: {1.0f / DeltaTime}");

                //INPUT
                SceneMgr.CurrentScene.Input();

                //UPDATE
                //bgAudio.Stream(bgAudioClip, DeltaTime); riggiungila

                SceneMgr.Update();
                SceneMgr.CurrentScene.Update();

                //DRAW
                SceneMgr.CurrentScene.Draw();

                window.Update();
            }
        }
    }
}
