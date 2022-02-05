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
            //bgSource.Stream(bgMusic, win.DeltaTime);
        }

        public void Update()
        {

        }

    }
}
