using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Super_Luigi.World.Blocks.BlockSystems
{
    class BlockPipe : Block
    {
        public BlockPipe(int x, int y, int index)
        {
            this.Breakable = false;
            this.Solid = true;
            this.Position = new Rectangle(x * 24, y * 24, 24, 24);
            this.ImageCount = 0f;
            this.ImageIndex = index;
            this.ImageSpeed = 0f;
            this.ImageTime = 1f;
            this.HeldItem = null;
            this.OffsetX = 0;
            this.OffsetY = 0;
            this.BreakSound = Game1.otherSounds[0];
            this.HitSound = Game1.otherSounds[1];

            this.SrcRect = new Rectangle[4];
            for (int i = 0; i < this.SrcRect.Length; i++)
            {
                if (i < 2)
                    this.SrcRect[i] = new Rectangle(i * 17 + 51, 272, 16, 16);
                else
                    this.SrcRect[i] = new Rectangle((i - 2) * 17 + 51, 289, 16, 16);
            }
        }
    }
}