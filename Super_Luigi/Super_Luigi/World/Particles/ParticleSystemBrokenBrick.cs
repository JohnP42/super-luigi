using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Super_Luigi.World.Particles
{
    class ParticleSystemBrokenBrick : ParticleSystem
    {

        public ParticleSystemBrokenBrick(int x, int y)
        {
            this.SprParticles = Game1.sprParticles;
            this.Particles = new List<Particle>();

            for(int i = 0; i < 6; i++)
                Particles.Add(new ParticleBrokenBrick(x, y));
        }

    }
}
