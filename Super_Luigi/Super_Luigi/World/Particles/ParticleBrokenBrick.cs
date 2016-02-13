using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Luigi.World.Particles
{
    class ParticleBrokenBrick : Particle
    {

        public ParticleBrokenBrick(int x, int y)
        {
            this.Alive = true;
            this.TimeAlive = 50f;
            this.Count = 0f;
            this.SrcRect = new Rectangle(0,0,6,6);
            this.Velocity = new Vector2(Particle.random.Next(-3, 4), -6);
            this.Position = new Rectangle(x + 8 + Particle.random.Next(-8, 9), y + 8 + Particle.random.Next(-8, 9), 6, 6);
            this.Gravity = .5f;
            this.Colour = Color.White;
        }

    }
}
