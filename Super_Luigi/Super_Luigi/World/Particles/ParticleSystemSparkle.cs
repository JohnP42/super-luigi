using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Super_Luigi.World.Particles
{
    class ParticleSystemSparkle : ParticleSystem
    {
        public ParticleSystemSparkle(int x, int y)
        {
            this.SprParticles = Game1.sprParticles;
            this.Particles = new List<Particle>();

            for(int i = 0; i < Particle.random.Next(1, 4); i++)
                Particles.Add(new ParticleSparkle(x, y));
        }

    }
}
