using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Super_Luigi.World.Particles
{
    class ParticleStar :Particle
    {

        public ParticleStar(int x, int y)
        {
            this.Alive = true;
            this.TimeAlive = 18f;
            this.Count = 0f;
            this.Velocity = new Vector2(0, 0);
            this.Gravity = 0f;
            this.Colour = new Color(Particle.random.Next(0, 256), Particle.random.Next(0, 256), Particle.random.Next(0, 256), 255);
            this.SrcRect = new Rectangle(0, 18, 11, 11);
            this.Position = new Rectangle(x + Particle.random.Next(-8, 9), y + Particle.random.Next(-8, 9), 10, 10);

        }

        public override void move()
        {
            base.move();

            if (Count < 6)
            {
                this.SrcRect = new Rectangle(0, 18, 11, 11);
            }
            else if (Count < 12)
            {
                this.SrcRect = new Rectangle(14, 16, 16, 16);
            }
            else
            {
                this.SrcRect = new Rectangle(31, 19, 16, 12);
            }

            Colour *= .98f;
        }

    }
}
