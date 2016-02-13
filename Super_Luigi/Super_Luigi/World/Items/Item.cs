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

namespace Super_Luigi.World.Items
{
    abstract class Item
    {

        bool obtained;
        Rectangle position;
        Rectangle[] srcRect;
        int imageIndex;
        float imageTime, imageSpeed, imageCount, gravity;
        Vector2 velocity;
        int offsetX, offsetY;
        SoundEffect grabSound;
        int creating;

        public bool Obtained { get { return obtained; } set { obtained = value; } }
        public Rectangle Position { get { return position; } set { position = value; } }
        public Rectangle[] SrcRect { get { return srcRect; } set { srcRect = value; } }
        public int ImageIndex { get { return imageIndex; } set { imageIndex = value; } }
        public float ImageTime { get { return imageTime; } set { imageTime = value; } }
        public float ImageSpeed { get { return imageSpeed; } set { imageSpeed = value; } }
        public float ImageCount { get { return imageCount; } set { imageCount = value; } }
        public int OffsetX { get { return offsetX; } set { offsetX = value; } }
        public int OffsetY { get { return offsetY; } set { offsetY = value; } }
        public SoundEffect GrabSound { get { return grabSound; } set { grabSound = value; } }
        public float Gravity { get { return gravity; } set { gravity = value; } }
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        public int Creating { get { return creating; } set { creating = value; } }

        public virtual void grabbed(Level l)
        {
            grabSound.Play();
            obtained = true;
        }

        public void update(Level l)
        {

            if (creating > 0)
                creating--;

            animate();
            move(l);

        }

        public virtual void move(Level l)
        {
            if (creating == 0)
            {
                velocity.Y += gravity;
                position.X += (int)velocity.X;
                position.Y += (int)velocity.Y;
                collision(l);
            }
            else
            {
                velocity.Y = -1;

                position.Y += (int)velocity.Y;
            }


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
                            velocity.Y = 0;
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

                        }
                        else if (Rectangle.Intersect(position, final.Position).Right == final.Position.Right)
                        {
                            velocity.X = 0;
                            position.X = final.Position.Right;
                        }

                    }

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
        }

    }
}
