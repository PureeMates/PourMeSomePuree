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
        private bool pingPong;

        private bool pingPongBegan;

        public int XOffset { get; private set; }
        public int YOffset { get; private set; }
        public float Duration { get { return frameDuration * frameNumber; } }
        public int CurrentFrame { get { return currentFrame; } }
        public bool IsPlaying { get { return isPlaying; } }

        public int FrameWidth { get { return frameWidth; } }
        public int FrameHeight { get { return frameHeight; } }

        public Animation(int fps, int totFrames, int frameW, int frameH, int startColumn = 1, int startRow = 1, bool loop = true, bool pingPong = false)
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
            this.pingPong = pingPong;
            pingPongBegan = false;
        }

        public void Update()
        {
            if (isPlaying)
            {
                elapsedTime += Game.Win.DeltaTime;

                if (elapsedTime >= frameDuration)
                {
                    elapsedTime = 0.0f;
                    if (!pingPongBegan)
                    {
                        currentFrame++;
                        actualColumn++;
                    }
                    else
                    {
                        currentFrame--;
                        actualColumn--;
                    }

                    if (!pingPongBegan && (currentFrame >= frameNumber))
                    {
                        if (pingPong)
                        {
                            pingPongBegan = true;
                            currentFrame--;
                            actualColumn--;
                        }
                        else
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
                    else if (pingPongBegan && (currentFrame <= 0))
                    {
                        if (loop)
                        {
                            pingPongBegan = false;
                        }
                        else
                        {
                            Stop();
                        }
                    }
                }
                ShiftOffset();
            }
        }

        private void ShiftOffset()
        {
            if (startColumn != 0)
            {
                XOffset = frameWidth * actualColumn;
            }
            else
            {
                XOffset = frameWidth * currentFrame;
            }
        }

        public void Stop()
        {
            isPlaying = false;
            currentFrame = 0;
            actualColumn = startColumn;
            ShiftOffset();
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
