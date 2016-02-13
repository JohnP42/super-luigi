using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Super_Luigi.World.Blocks.BlockSystems.Doodads
{
    class BlockCloud : Block
    {
        public BlockCloud(int x, int y, int index)
        {
            this.Breakable = false;
            this.Solid = false;
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

            this.SrcRect = new Rectangle[3];
            for (int i = 0; i < this.SrcRect.Length; i++)
            {
                this.SrcRect[i] = new Rectangle(i * 17 + 68, 357, 16, 16);
            }
        }
    }
}