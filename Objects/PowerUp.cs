namespace PourMeSomePuree
{
    abstract class PowerUp : GameObject
    {
        public Player owner;
        protected bool isVisible;
        protected float counter;
        protected float startCounter;

        public PowerUp(string textureName) : base(textureName)
        {
            isVisible = false;
            IsActive = false;
            startCounter = 10.0f;
            counter = startCounter;
        }

        public override void Update()
        {
            if (IsActive)
            {
                counter -= Game.DeltaTime;

                if (counter <= 0)
                {
                    counter = startCounter;
                    IsActive = false;
                    EndPowerUp();
                }
            }
        }

        public virtual void Effect(Player player) { }

        public virtual void EndPowerUp() { }

        public override void Draw()
        {
            if (IsActive)
            {
                base.Draw();
            }
        }
    }
}
