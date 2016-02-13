using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Super_Luigi.World.Blocks
{
    class SolidBlock : Block
    {

        public SolidBlock(int x, int y)
        {
            this.Breakable = false;
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

            this.SrcRect = new Rectangle[1];
            this.SrcRect[0] = new Rectangle(0, 68, 16, 16);

        }

    }
}
