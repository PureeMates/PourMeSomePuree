﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace PourMeSomePuree
{
    static class GfxMgr
    {
        private static Dictionary<string, Texture> textures;

        private static Dictionary<string, Animation> animations;

        static GfxMgr()
        {
            textures = new Dictionary<string, Texture>();
            animations = new Dictionary<string, Animation>();
        }

        public static Texture AddTexture(string name, string path)
        {
            Texture t = new Texture(path);

            if (t != null)
            {
                textures[name] = t;
            }

            return t;
        }

        public static Texture GetTexture(string name)
        {
            Texture t = null;

            if (textures.ContainsKey(name))
            {
                t = textures[name];
            }

            return t;
        }

        public static Animation AddAnimation(string name,int fps, int totFrames, int frameW, int frameH, int startCol, int startRow, bool loop = true)
        {
            Animation a = new Animation(fps, totFrames, frameW, frameH, startCol, startRow, loop);

            if (a != null)
            {
                animations[name] = a;
            }

            return a;
        }

        public static Animation GetAnimation(string name)
        {
            Animation a = null;

            if (animations.ContainsKey(name))
            {
                a = animations[name];
            }

            return a;
        }

        public static void ClearAll()
        {
            textures.Clear();
            animations.Clear();
        }
    }
}
