using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Luigi.World.Particles
{
    abstract class Particle
    {
        public static Random random = new Random();

        Rectangle position;
        Vector2 velocity;
        float timeAlive, count, gravity;
        bool alive;
        Rectangle srcRect;
        Color color;

        public Rectangle Position { get { return position; } set { position = value; } }
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        public float TimeAlive { get { return timeAlive; } set { timeAlive = value; } }
        public float Count { get { return count; } set { count = value; } }
        public float Gravity { get { return gravity; } set { gravity = value; } }
        public bool Alive { get { return alive; } set { alive = value; } }
        public Rectangle SrcRect { get { return srcRect; } set { srcRect = value; } }
        public Color Colour { get { return color; } set { color = value; } }

        public void update()
        {
            move();

            if (count >= timeAlive)
                alive = false;
            else
                count++;
        }

        public virtual void move() 
        {
            velocity.Y += gravity;

            position.X += (int)velocity.X;
            position.Y += (int)velocity.Y;
        }

    }
}
