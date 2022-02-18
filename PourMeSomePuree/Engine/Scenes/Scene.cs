using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    abstract class Scene
    {
        public bool IsPlaying { get; protected set; }

        public Scene()
        {
            SceneMgr.AddScene(this);
        }

        public virtual void Start()
        {
            IsPlaying = true;
        }

        public virtual void OnExit()
        {
            AudioMgr.ClearAll();
            DebugMgr.ClearAll();
            DrawMgr.ClearAll();
            FontMgr.ClearAll();
            GfxMgr.ClearAll();
            PhysicsMgr.ClearAll();
            UpdateMgr.ClearAll();

            IsPlaying = false;
        }

        public abstract void Input();
        public abstract void Update();
        public abstract void Draw();

        protected void Quit()
        {
            if (Game.Win.GetKey(KeyCode.Esc))
            {
                Game.Win.Exit();
            }
        }
    }
}
