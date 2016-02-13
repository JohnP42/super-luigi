using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Super_Luigi.World.Particles;

namespace Super_Luigi.World.Items
{
    class ItemCoin : Item
    {

        public ItemCoin(int x, int y)
        {
            this.GrabSound = Game1.otherSounds[2];
            this.Gravity = 0f;
            this.ImageCount = 0f;
            this.ImageIndex = 0;
            this.ImageSpeed = 1f;
            this.ImageTime = 10f;
            this.Obtained = false;
            this.OffsetX = 0;
            this.OffsetY = 0;
            this.Position = new Rectangle(x * 24, y * 24, 24, 24);
            this.Velocity = Vector2.Zero;
            this.Creating = 0;

            this.SrcRect = new Rectangle[4];
            SrcRect[0] = new Rectangle(0, 209, 14, 16);
            SrcRect[1] = new Rectangle(17, 209, 13, 16);
            SrcRect[2] = new Rectangle(39, 209, 4, 16);
            SrcRect[3] = new Rectangle(52, 209, 13, 16);

        }

        public ItemCoin(int x, int y, Vector2 v)
        {
            this.GrabSound = Game1.otherSounds[2];
            this.Gravity = 0.5f;
            this.ImageCount = 0f;
            this.ImageIndex = 0;
            this.ImageSpeed = 1f;
            this.ImageTime = 10f;
            this.Obtained = false;
            this.OffsetX = 0;
            this.OffsetY = 0;
            this.Position = new Rectangle(x * 24, y * 24, 24, 24);
            this.Velocity = v;
            this.Creating = 0;

            this.SrcRect = new Rectangle[4];
            SrcRect[0] = new Rectangle(0, 209, 14, 16);
            SrcRect[1] = new Rectangle(17, 209, 13, 16);
            SrcRect[2] = new Rectangle(39, 209, 4, 16);
            SrcRect[3] = new Rectangle(52, 209, 13, 16);

        }

        public override void grabbed(Levels.Level l)
        {
            base.grabbed(l);

            l.ParticleSystems.Add(new ParticleSystemSparkle(Position.X + 12, Position.Y + 12));
        }

    }
}
