using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Super_Luigi.World.Particles;

namespace Super_Luigi.World.Blocks
{
    class BlockBrick : Block
    {

        public BlockBrick(int x, int y)
        {
            this.Breakable = true;
            this.Solid = true;
            this.Position = new Rectangle(x * 24, y * 24, 24, 24);
            this.ImageCount = 0f;
            this.ImageIndex = 0;
            this.ImageSpeed = 1f;
            this.ImageTime = 10f;
            this.HeldItem = null;
            this.OffsetX = 0;
            this.OffsetY = 0;
            this.BreakSound = Game1.otherSounds[0];
            this.HitSound = Game1.otherSounds[1];

            this.SrcRect = new Rectangle[10];
            for (int i = 0; i < this.SrcRect.Length; i++)
            {
                if (i < 7)
                    this.SrcRect[i] = new Rectangle(136, 0, 16, 16);
                else
                    this.SrcRect[i] = new Rectangle(136 + (i - 6) * 17, 0, 16, 16);
            }

        }

        public BlockBrick(int x, int y, Items.Item item)
        {
            this.Breakable = true;
            this.Solid = true;
            this.Position = new Rectangle(x * 24, y * 24, 24, 24);
            this.ImageCount = 0f;
            this.ImageIndex = 0;
            this.ImageSpeed = 1f;
            this.ImageTime = 10f;
            this.HeldItem = item;
            this.OffsetX = 0;
            this.OffsetY = 0;
            this.BreakSound = Game1.otherSounds[0];
            this.HitSound = Game1.otherSounds[1];

            this.SrcRect = new Rectangle[10];
            for (int i = 0; i < this.SrcRect.Length; i++)
            {
                if (i < 7)
                    this.SrcRect[i] = new Rectangle(136, 0, 16, 16);
                else
                    this.SrcRect[i] = new Rectangle(136 + (i - 6) * 17, 0, 16, 16);
            }

        }

    }
}
