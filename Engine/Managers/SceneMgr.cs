using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    static class SceneMgr
    {
        private static List<Scene> scenes;

        private static int currentSceneIndex;

        public static Scene CurrentScene { get; private set; }

        static SceneMgr()
        {
            scenes = new List<Scene>();
            currentSceneIndex = 0;
        }

        public static void Start()
        {
            CurrentScene = scenes[currentSceneIndex];
            CurrentScene.Start();
        }

        public static void Update()
        {
            if (!CurrentScene.IsPlaying)
            {
                if(++currentSceneIndex >= scenes.Count)
                {
                    currentSceneIndex = 0;
                }

                CurrentScene.OnExit();
                CurrentScene = scenes[currentSceneIndex];
                CurrentScene.Start();
            }
        }

        public static void AddScene(Scene scene)
        {
            scenes.Add(scene);
        }
    }
}
