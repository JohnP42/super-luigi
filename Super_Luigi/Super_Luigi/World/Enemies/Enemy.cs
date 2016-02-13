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
using Super_Luigi.World.Particles;
using Super_Luigi.World.Items;

namespace Super_Luigi.World.Enemies
{
    abstract class Enemy
    {
        //Position
        Rectangle position;
        bool facingRight;
        bool alive;
        int hurt, stomped;

        //Phyics
        float speed, acc;
        Vector2 velocity;
        float gravity;

        //Sprites
        Texture2D spriteSheet;
        int imageIndex;
        float imageSpeed, imageTime, imageCount;
        SpriteEffects flip = SpriteEffects.None;
        Rectangle[] srcRect;
        Rectangle[] srcWalk, srcTurn, srcDie;
        Vector2 offDraw;

        SoundEffect sndStomp, sndHit;

        //Accessors
        public Rectangle Position { get { return position; } set { position = value; } }
        public bool FacingRight { get { return facingRight; } set { facingRight = value; } }
        public bool Alive { get { return alive; } set { alive = value; } }
        public int Hurt { get { return hurt; } set { hurt = value; } }
        public int Stomped { get { return stomped; } set { stomped = value; } }

        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        public Vector2 OffDraw { get { return offDraw; } set { offDraw = value; } }
        public float Gravity { get { return gravity; } set { gravity = value; } }
        public float Acc { get { return acc; } set { acc = value; } }
        public float Speed { get { return speed; } set { speed = value; } }

        public Texture2D SpriteSheet { get { return spriteSheet; } set { spriteSheet = value; } }
        public int ImageIndex { get { return imageIndex; } set { imageIndex = value; } }
        public float ImageSpeed { get { return imageSpeed; } set { imageSpeed = value; } }
        public float ImageTime { get { return imageTime; } set { imageTime = value; } }
        public float ImageCount { get { return imageCount; } set { imageCount = value; } }

        public SoundEffect SndHit { get { return sndHit; } set { sndHit = value; } }
        public SoundEffect SndStomp { get { return sndStomp; } set { sndStomp = value; } }

        public SpriteEffects Flip { get { return flip; } set { flip = value; } }

        public Rectangle[] SrcRect { get { return srcRect; } set { srcRect = value; } }
        public Rectangle[] SrcWalk { get { return srcWalk; } set { srcWalk = value; } }
        public Rectangle[] SrcTurn { get { return srcTurn; } set { srcTurn = value; } }
        public Rectangle[] SrcDie { get { return srcDie; } set { srcDie = value; } }

        public virtual void onHit(Level l)
        {

        }

        public virtual void onStomp(Level l)
        {

        }

        public virtual void update(Level l)
        {

            if (hurt > 0)
                hurt--;
            if (hurt == 0)
                alive = false;

            if (stomped > 0)
                stomped--;
            if (stomped == 0)
                alive = false;

            move(l);
            animate();
            sprites();

        }

        public virtual void move(Level l)
        {
            velocity.Y += gravity;

            position.X += (int)velocity.X;
            position.Y += (int)velocity.Y;

            if(hurt == -1)
                collision(l);
        }

        public virtual void sprites()
        {

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
