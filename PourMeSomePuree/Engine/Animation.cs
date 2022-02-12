using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    class Animation
    {
        private int currentFrame;
        private float elapsedTime;
        private int actualColumn;

        private int frameNumber;
        private float frameDuration;

        private int frameWidth;
        private int frameHeight;
        private int startRow;
        private int startColumn;

        private bool isPlaying;
        private bool loop;

        public int XOffset { get; private set; }
        public int YOffset { get; private set; }
        public float Duration { get { return frameDuration * frameNumber; } }
        public int CurrentFrame { get { return currentFrame; } }

        public Animation(int fps, int totFrames, int frameW, int frameH, int startColumn = 1, int startRow = 1, bool loop = true)
        {
            frameNumber = totFrames;
            currentFrame = 0;
            elapsedTime = 0.0f;
            frameDuration = 1.0f / fps;

            frameWidth = frameW;
            frameHeight = frameH;
            this.startRow = startRow - 1;
            this.startColumn = startColumn - 1;
            actualColumn = this.startColumn;

            XOffset = 0;
            YOffset = frameHeight * this.startRow;

            isPlaying = false;
            this.loop = loop;
        }

        public void Update()
        {
            if (isPlaying)
            {
                elapsedTime += Game.Win.DeltaTime;

                if (elapsedTime >= frameDuration)
                {
                    elapsedTime = 0.0f;
                    currentFrame++;
                    actualColumn++;

                    if (currentFrame >= frameNumber)
                    {
                        if (loop)
                        {
                            currentFrame = 0;
                            actualColumn = startColumn;
                        }
                        else
                        {
                            Stop();
                        }
                    }
                }

                if (startColumn != 0)
                {
                    XOffset = frameWidth * actualColumn;
                }
                else
                {
                    XOffset = frameWidth * currentFrame;
                }
            }
        }

        public void Stop()
        {
            isPlaying = false;
            currentFrame = 0;
            actualColumn = startColumn;
            elapsedTime = 0.0f;
        }

        public void Pause()
        {
            isPlaying = false;
        }

        public void Start()
        {
            isPlaying = true;
        }
    }
}
