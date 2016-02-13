using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Super_Luigi.World.Particles;
using Super_Luigi.World.Blocks;
using Microsoft.Xna.Framework.Media;

namespace Super_Luigi.World.Items
{
    class Starman : Item
    {
        public Starman(int x, int y)
        {
            this.GrabSound = Game1.otherSounds[6];
            this.Gravity = 0.15f;
            this.ImageCount = 0f;
            this.ImageIndex = 0;
            this.ImageSpeed = 0f;
            this.ImageTime = 10f;
            this.Obtained = false;
            this.OffsetX = 0;
            this.OffsetY = 0;
            this.Position = new Rectangle(x * 24 + 4, y * 24 + 4, 16, 16);
            this.Velocity = new Vector2(1, 0);
            this.Creating = 24;

            this.SrcRect = new Rectangle[1];
            SrcRect[0] = new Rectangle(17, 170, 16, 16);

        }

        public override void collision(Levels.Level level)
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
                            Velocity = new Vector2(Velocity.X, - 6);
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
                            Velocity = new Vector2(-1, Velocity.Y);
                            Position = new Rectangle(final.Position.Left - Position.Width, Position.Y, Position.Width, Position.Height);

                        }
                        else if (Rectangle.Intersect(Position, final.Position).Right == final.Position.Right)
                        {
                            Velocity = new Vector2(1, Velocity.Y);
                            Position = new Rectangle(final.Position.Right, Position.Y, Position.Width, Position.Height);
                        }

                    }

                    break;
                }
            }
        }

    }
}
