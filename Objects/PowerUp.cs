﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    abstract class PowerUp : GameObject
    {
        public Player owner;         
        protected bool isVisible;
        protected float counter;
        protected float startCounter;

        private bool isOpen;

        public PowerUp(string textureName) : base(textureName) 
        {            
            isVisible = false;
            IsActive = false;
            isOpen = false;
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
