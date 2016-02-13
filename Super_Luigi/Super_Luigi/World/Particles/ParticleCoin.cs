using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Super_Luigi.World.Particles
{
    class ParticleCoin : Particle
    {

        public ParticleCoin(int x, int y)
        {
            this.Alive = true;
            this.TimeAlive = 18f;
            this.Count = 0f;
            this.Velocity = new Vector2(0, -1);
            this.Gravity = 0f;
            this.Colour = Color.White;
            this.SrcRect = new Rectangle(0, 38, 14, 16);
            this.Position = new Rectangle(x + 11, y, 16, 16);

        }


        public override void move()
        {
            base.move();

            if (Count < 3)
            {
                this.SrcRect = new Rectangle(0, 38, 14, 16);
                this.Position = new Rectangle(Position.X, Position.Y, 14, 16);
            }
            else if (Count < 6)
            {
                this.SrcRect = new Rectangle(17, 38, 13, 16);
                this.Position = new Rectangle(Position.X, Position.Y, 13, 16);
            }
            else if (Count < 9)
            {
                this.SrcRect = new Rectangle(39, 38, 4, 16);
                this.Position = new Rectangle(Position.X, Position.Y, 4, 16);
            }
            else if (Count < 12)
            {
                this.SrcRect = new Rectangle(52, 38, 13, 16);
                this.Position = new Rectangle(Position.X, Position.Y, 13, 16);
            }
            else if (Count < 15)
            {
                this.SrcRect = new Rectangle(0, 38, 14, 16);
                this.Position = new Rectangle(Position.X, Position.Y, 14, 16);
            }
            else
            {
                this.SrcRect = new Rectangle(17, 38, 13, 16);
                this.Position = new Rectangle(Position.X, Position.Y, 14, 16);
            }

        }

    }
}
