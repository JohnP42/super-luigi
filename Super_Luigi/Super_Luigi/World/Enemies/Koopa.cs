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
    class Koopa : Enemy
    {
        bool inShell;
        int lethal;

        public bool InShell { get { return inShell; } set { inShell = value; } }
        public int Lethal { get { return lethal; } set { lethal = value; } }

        public Koopa(int x, int y)
        {
            this.Acc = .2f;
            this.FacingRight = true;
            this.Flip = SpriteEffects.None;
            this.Gravity = .5f;
            this.ImageCount = 0f;
            this.ImageIndex = 0;
            this.ImageSpeed = 1f;
            this.ImageTime = 10f;
            this.Position = new Rectangle(x * 24, y * 24, 19, 22);
            this.Speed = 1f;
            this.SpriteSheet = Game1.sprEnemies;
            this.Velocity = new Vector2(0, 0);
            this.Alive = true;
            this.Hurt = -1;
            this.Stomped = -1;
            inShell = false;

            SndStomp = Game1.otherSounds[4];
            SndHit = Game1.otherSounds[5];

            this.SrcTurn = new Rectangle[1];
                SrcTurn[0] = new Rectangle(0, 27, 22, 30);

            this.SrcWalk = new Rectangle[7];
            SrcWalk[0] = new Rectangle(0, 27, 22, 30);
            SrcWalk[1] = new Rectangle(27, 28, 21, 29);
            SrcWalk[2] = new Rectangle(53, 26, 21, 32);
            SrcWalk[3] = new Rectangle(105, 28, 23, 29);
            SrcWalk[4] = new Rectangle(133, 28, 21, 29);
            SrcWalk[5] = new Rectangle(159, 26, 21, 31);
            SrcWalk[6] = new Rectangle(185, 25, 22, 32);

            this.SrcDie = new Rectangle[6];
            SrcDie[0] = new Rectangle(216, 42, 16, 15);
            SrcDie[1] = new Rectangle(237, 42, 16, 15);
            SrcDie[2] = new Rectangle(258, 42, 16, 15);
            SrcDie[3] = new Rectangle(279, 42, 16, 15);
            SrcDie[4] = new Rectangle(237, 42, 16, 15);
            SrcDie[5] = new Rectangle(258, 42, 16, 15);

            SrcRect = SrcWalk;
        }

        public override void onHit(Level l)
        {
            Velocity = new Vector2(0, -5);
            Hurt = 50;
            SndHit.Play();
        }

        public override void onStomp(Level l)
        {
            if (!inShell)
            {
                Velocity = new Vector2(0, Velocity.Y);
                inShell = true;
                SndStomp.Play();
            }
            else
            {
                if(Velocity.X == 0)
                    Velocity = new Vector2(4, Velocity.Y);
                else
                    Velocity = new Vector2(0, Velocity.Y);
                SndStomp.Play();
            }
        }

        public override void move(Level l)
        {
            if (lethal > 0)
                lethal--;

            if (Stomped == -1 && Hurt == -1 && !inShell)
            {
                if (Velocity.X == 0)
                {
                    if (FacingRight)
                        FacingRight = false;
                    else
                        FacingRight = true;
                }

                if (FacingRight)
                {
                    if (Math.Abs(Velocity.X) < Speed)
                        Velocity = new Vector2(Velocity.X + Acc, Velocity.Y);
                }
                else
                {
                    if (Math.Abs(Velocity.X) < Speed)
                        Velocity = new Vector2(Velocity.X - Acc, Velocity.Y);
                }

            }


            if (inShell)
            {

                if (Velocity.X != 0)
                {
                    foreach (Enemy e in l.Enemies)
                    {
                        if (e.Equals(this))
                            continue;

                        if (e.Alive == false || e.Hurt != -1 || e.Stomped != -1)
                            continue;

                        if (Position.Intersects(e.Position))
                        {
                            e.onHit(l);
                            break;
                        }
                    }
                }
            }


            base.move(l);
        }

        public override void collision(Level level)
        {
            if (inShell)
            {
                foreach (Block b in level.Blocks)
                {

                    if (Position.Intersects(b.Position))
                    {
                        Block final = b;

                        foreach (Block n in level.getAdjacentBlocks(b))
                        {

                            if (Rectangle.Intersect(Position, n.Position).Width * Rectangle.Intersect(Position, n.Position).Height > Rectangle.Intersect(Position, final.Position).Width * Rectangle.Intersect(Position, final.Position).Height)
                                final = n;

                        }
                        if (Rectangle.Intersect(Position, final.Position).Width >= Rectangle.Intersect(Position, final.Position).Height)
                        {

                            if (Rectangle.Intersect(Position, final.Position).Top == final.Position.Top)
                            {
                                Position = new Rectangle(Position.X, final.Position.Top - Position.Height, Position.Width, Position.Height);
                                Velocity = new Vector2(Velocity.X, 0);
                            }
                            else if (Rectangle.Intersect(Position, final.Position).Bottom == final.Position.Bottom)
                            {
                                Position = new Rectangle(Position.X, final.Position.Bottom, Position.Width, Position.Height);
                                Velocity = new Vector2(Velocity.X, Velocity.Y * -1); ;
                            }
                        }
                        else
                        {

                            if (Rectangle.Intersect(Position, final.Position).Left == final.Position.Left)
                            {
                                Velocity = new Vector2(-4, Velocity.Y);
                                Position = new Rectangle(final.Position.Left - Position.Width, Position.Y, Position.Width, Position.Height);
                                Game1.otherSounds[1].Play();

                            }
                            else if (Rectangle.Intersect(Position, final.Position).Right == final.Position.Right)
                            {
                                Velocity = new Vector2(4, Velocity.Y);
                                Position = new Rectangle(final.Position.Right, Position.Y, Position.Width, Position.Height);
                                Game1.otherSounds[1].Play();
                            }

                        }

                        break;
                    }
                }
            }
            else
                base.collision(level);
        }

        public override void sprites()
        {
            Rectangle[] temp = SrcRect;

            if (!inShell)
            {
                if (Stomped == -1 && Hurt == -1)
                {
                    if (Velocity.X == 0)
                    {
                        SrcRect = SrcTurn;
                        ImageSpeed = 1f;
                    }
                    else if (Velocity.X > 0)
                    {
                        SrcRect = SrcWalk;
                        ImageSpeed = Math.Abs(Velocity.X);
                    }
                    else
                    {
                        SrcRect = SrcWalk;
                        ImageSpeed = Math.Abs(Velocity.X);
                    }
                }
                else if (Stomped != -1)
                {
                    SrcRect = SrcDie;
                    ImageSpeed = 0f;
                    ImageIndex = 0;
                }
                else if (Hurt != -1)
                {
                    SrcRect = SrcDie;
                    ImageSpeed = 0f;
                    ImageIndex = 1;
                }
            }
            else
            {
                SrcRect = SrcDie;
                ImageSpeed = Math.Abs(Velocity.X);
            }

            if (FacingRight)
                Flip = SpriteEffects.None;
            else
                Flip = SpriteEffects.FlipHorizontally;

            if (!temp.Equals(SrcRect))
                ImageIndex = 0;

            OffDraw = new Vector2((Position.Width - SrcRect[ImageIndex].Width) / 2, Position.Height - SrcRect[ImageIndex].Height);
        }



    }
}
