using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Super_Luigi.World.Particles
{
    class ParticleSystemCoin : ParticleSystem
    {
        public ParticleSystemCoin(int x, int y)
        {
            this.SprParticles = Game1.sprParticles;
            this.Particles = new List<Particle>();

            Particles.Add(new ParticleCoin(x, y));
        }
    }
}
