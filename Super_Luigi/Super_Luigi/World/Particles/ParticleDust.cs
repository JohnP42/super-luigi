using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Super_Luigi.World.Particles
{
    class ParticleDust : Particle
    {

        public ParticleDust(int x, int y)
        {
            this.Alive = true;
            this.TimeAlive = 40f;
            this.Count = 0f;
            this.Velocity = new Vector2(0, 0);
            this.Gravity = 0f;
            this.Colour = Color.White;

            int n = Particle.random.Next(1, 4);

            switch (n)
            {
                case 1:
                    this.SrcRect = new Rectangle(7, 11, 3, 3);
                    this.Position = new Rectangle(x + Particle.random.Next(-4, 5), y - Particle.random.Next(0, 5), 3, 3);
                    break;
                case 2:
                    this.SrcRect = new Rectangle(14, 10, 5, 5);
                    this.Position = new Rectangle(x + Particle.random.Next(-4, 5), y - Particle.random.Next(0, 5), 5, 5);
                    break;
                case 3:
                    this.SrcRect = new Rectangle(21, 7, 7, 8);
                    this.Position = new Rectangle(x + Particle.random.Next(-4, 5), y - Particle.random.Next(0, 5), 7, 8);
                    break;

            }

        }


        public override void move()
        {
            base.move();

            Colour *= .98f;

        }

    }
}
