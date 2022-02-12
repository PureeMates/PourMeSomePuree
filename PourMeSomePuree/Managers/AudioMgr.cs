using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using Aiv.Audio;
using OpenTK;

namespace PourMeSomePuree
{
     class AudioMgr
    {
        AudioSource bgSource = new AudioSource();
        AudioClip bgMusic = new AudioClip("Assets/Audio/Horde_theme.ogg");
        

        public void Input()
        {
            bgSource.Position = new Vector3(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f, 0.0f);

            bgSource.Stream(bgMusic, Game.Win.DeltaTime);
        }

        public void Update()
        {

        }

    }
}
