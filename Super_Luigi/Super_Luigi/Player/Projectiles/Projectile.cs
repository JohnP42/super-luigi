using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Super_Luigi.World.Levels;
using Super_Luigi.World.Blocks;
using Super_Luigi.World.Enemies;

namespace Super_Luigi.Player.Projectiles
{
    class Projectile
    {
        bool alive;
        Rectangle position;
        Rectangle[] srcRect;
        bool facingRight;
        int imageIndex;
        float imageTime, imageSpeed, imageCount, gravity;
        Vector2 velocity;
        int offsetX, offsetY;
        SoundEffect createSound;


        public bool Alive { get { return alive; } set { alive = value; } }
        public Rectangle Position { get { return position; } set { position = value; } }
        public Rectangle[] SrcRect { get { return srcRect; } set { srcRect = value; } }
        public bool FacingRight { get { return facingRight; } set { facingRight = value; } }
        public int ImageIndex { get { return imageIndex; } set { imageIndex = value; } }
        public float ImageTime { get { return imageTime; } set { imageTime = value; } }
        public float ImageSpeed { get { return imageSpeed; } set { imageSpeed = value; } }
        public float ImageCount { get { return imageCount; } set { imageCount = value; } }
        public int OffsetX { get { return offsetX; } set { offsetX = value; } }
        public int OffsetY { get { return offsetY; } set { offsetY = value; } }
        public SoundEffect CreateSound { get { return createSound; } set { createSound = value; } }
        public float Gravity { get { return gravity; } set { gravity = value; } }
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }

        public virtual void destroy(Level l)
        {
            alive = false;
            onDestroy(l);
        }

        public virtual void onDestroy(Level l)
        {

        }

        public void update(Level l)
        {

            animate();
            move(l);

        }

        public virtual void move(Level l)
        {

            velocity.Y += gravity;
            position.X += (int)velocity.X;
            position.Y += (int)velocity.Y;
            collision(l);


        }

        public virtual void collision(Level level)
        {
            foreach (Block b in level.Blocks)
            {
                if (position.Intersects(b.Position))
                {
                    Block final = b;

                    foreach (Block n in level.getAdjacentBlocks(b))
                    {

                        if (Rectangle.Intersect(position, n.Position).Width * Rectangle.Intersect(position, n.Position).Height > Rectangle.Intersect(position, final.Position).Width * Rectangle.Intersect(position, final.Position).Height)
                            final = n;

                    }
                    if (Rectangle.Intersect(position, final.Position).Width >= Rectangle.Intersect(position, final.Position).Height)
                    {

                        if (Rectangle.Intersect(position, final.Position).Top == final.Position.Top)
                        {
                            position.Y = final.Position.Top - position.Height;
                            velocity.Y = -6;
                        }
                        else if (Rectangle.Intersect(position, final.Position).Bottom == final.Position.Bottom)
                        {
                            position.Y = final.Position.Bottom;
                            velocity.Y *= -1;
                        }
                    }
                    else
                    {

                        if (Rectangle.Intersect(position, final.Position).Left == final.Position.Left)
                        {
                            velocity.X = 0;
                            position.X = final.Position.Left - position.Width;
                            destroy(level);
                        }
                        else if (Rectangle.Intersect(position, final.Position).Right == final.Position.Right)
                        {
                            velocity.X = 0;
                            position.X = final.Position.Right;
                            destroy(level);
                        }

                    }

                    break;
                }
            }

            foreach (Enemy e in level.Enemies)
            {
                if (e.Alive == false || e.Hurt != -1 || e.Stomped != -1)
                    continue;

                if (position.Intersects(e.Position))
                {

                    this.destroy(level);
                    e.onHit(level);

                    break;
                }
            }


        }

        public void animate()
        {
            if (imageCount >= imageTime)
            {
                if (imageIndex < srcRect.Length - 1)
                {
                    imageIndex++;
                }
                else
                    imageIndex = 0;
                imageCount = 0;
            }
            else
                imageCount += imageSpeed;


            offsetY = position.Height - srcRect[imageIndex].Height;
            offsetX = (position.Width - srcRect[imageIndex].Width) / 2;
        }

    }
}
