using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Super_Luigi.World.Particles;
using Super_Luigi.World.Blocks;

namespace Super_Luigi.World.Items
{
    class Fireflower : Item
    {
        public Fireflower(int x, int y)
        {
            this.GrabSound = Game1.otherSounds[6];
            this.Gravity = 0.5f;
            this.ImageCount = 0f;
            this.ImageIndex = 0;
            this.ImageSpeed = 1f;
            this.ImageTime = 10f;
            this.Obtained = false;
            this.OffsetX = 0;
            this.OffsetY = 0;
            this.Position = new Rectangle(x * 24 + 4, y * 24 + 4, 16, 16);
            this.Velocity = new Vector2(0, 0);
            this.Creating = 24;

            this.SrcRect = new Rectangle[4];
            SrcRect[0] = new Rectangle(0, 68, 16, 16);
            SrcRect[1] = new Rectangle(17, 68, 16, 16);
            SrcRect[2] = new Rectangle(0, 68, 16, 16);
            SrcRect[3] = new Rectangle(51, 68, 16, 16);

        }
    }
}
