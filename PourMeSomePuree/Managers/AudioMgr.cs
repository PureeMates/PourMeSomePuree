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
     static class AudioMgr
    {
        private static Dictionary<string, Texture> sounds;

        static AudioMgr()
        {
            sounds = new Dictionary<string, Texture>();
        }

        public static Texture AddSounds(string name, string path)
        {
            Texture s = new Texture(path);

            if (s != null)
            {
                sounds[name] = s;
            }

            return s;
        }

        public static Texture GetSounds(string name)
        {
            Texture s = null;

            if (sounds.ContainsKey(name))
            {
                s = sounds[name];
            }

            return s;
        }

        public static void ClearAll()
        {
            sounds.Clear();
        }

    }
}
