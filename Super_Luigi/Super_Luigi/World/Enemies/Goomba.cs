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
    class Goomba : Enemy
    {

        public Goomba(int x, int y)
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

            SndStomp = Game1.otherSounds[4];
            SndHit = Game1.otherSounds[5];

            this.SrcTurn = new Rectangle[1];
                SrcTurn[0] = new Rectangle(222, 2, 19, 20);

            this.SrcWalk = new Rectangle[8];
            SrcWalk[0] = new Rectangle(0, 0, 19, 22);
            SrcWalk[1] = new Rectangle(24, 1, 19, 21);
            SrcWalk[2] = new Rectangle(49, 2, 19, 20);
            SrcWalk[3] = new Rectangle(73, 1, 19, 21);
            SrcWalk[4] = new Rectangle(98, 0, 19, 22);
            SrcWalk[5] = new Rectangle(123, 1, 19, 21);
            SrcWalk[6] = new Rectangle(148, 2, 19, 20);
            SrcWalk[7] = new Rectangle(173, 1, 19, 21);

            this.SrcDie = new Rectangle[2];
            SrcDie[0] = new Rectangle(199, 11, 19, 11);
            SrcDie[1] = new Rectangle(245, 1, 19, 20);

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
            Velocity = new Vector2(0, Velocity.Y);
            Stomped = 20;
            SndStomp.Play();
        }

        public override void move(Level l)
        {
            if (Stomped == -1 && Hurt == -1)
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


            base.move(l);
        }

        public override void sprites()
        {
            Rectangle[] temp = SrcRect;

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

            if (FacingRight)
                Flip = SpriteEffects.None;
            else
                Flip = SpriteEffects.FlipHorizontally;

            if (!temp.Equals(SrcRect))
                ImageIndex = 0;

            OffDraw =  new Vector2((Position.Width - SrcRect[ImageIndex].Width) / 2, Position.Height - SrcRect[ImageIndex].Height);
        }

    }
}
