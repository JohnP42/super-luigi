using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Luigi.World.Particles
{
    class ParticleSystem
    {
        List<Particle> particles;
        Texture2D sprParticles;

        public List<Particle> Particles { get { return particles; } set { particles = value; } }
        public Texture2D SprParticles { get { return sprParticles; } set { sprParticles = value; } }

        public ParticleSystem()
        {
            particles = new List<Particle>();
            sprParticles = Game1.sprParticles;
        }

        public void update()
        {

            foreach (Particle p in particles)
            {
                p.update();
            }

            for (int i = particles.Count - 1; i >= 0; i--)
            {
                if (!particles[i].Alive)
                    particles.RemoveAt(i);
            }
        }

        public void draw(SpriteBatch spritebatch)
        {
            foreach (Particle p in particles)
                if(p.Alive)
                    spritebatch.Draw(sprParticles, new Rectangle(p.Position.X - p.SrcRect.Width / 2, p.Position.Y - p.SrcRect.Height / 2, p.Position.Width, p.Position.Height), p.SrcRect, p.Colour, 0f, Vector2.Zero, SpriteEffects.None, .6f);
        }

    }
}
