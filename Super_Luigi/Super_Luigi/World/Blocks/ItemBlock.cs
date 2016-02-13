using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Super_Luigi.World.Items;

namespace Super_Luigi.World.Blocks
{
    class ItemBlock : Block
    {

        public ItemBlock(int x, int y, Items.Item item)
        {
            this.Breakable = false;
            this.Solid = true;
            this.Position = new Rectangle(x * 24, y * 24, 24, 24);
            this.ImageCount = 0f;
            this.ImageIndex = 0;
            this.ImageSpeed = 1f;
            this.ImageTime = 10f;
            this.HeldItem = item;
            this.OffsetX = 0;
            this.OffsetY = 0;
            if(HeldItem is Items.ItemCoin)
                this.BreakSound = Game1.otherSounds[2];
            else
                this.BreakSound = Game1.otherSounds[3];
            this.HitSound = Game1.otherSounds[1];

            this.SrcRect = new Rectangle[4];
            for (int i = 0; i < this.SrcRect.Length; i++)
            {
                this.SrcRect[i] = new Rectangle(i * 17, 0, 16, 16);
            }

        }

        public override void onHit(Levels.Level l)
        {
            base.onHit(l);

            if (HeldItem != null )
            {
                if (HeldItem is ItemCoin)
                    l.ParticleSystems.Add(new Particles.ParticleSystemCoin(Position.X, Position.Y));
                l.Items.Add(HeldItem);
                HeldItem = null;
            }

            SolidBlock temp = new SolidBlock(Position.X / 24, Position.Y / 24);
            temp.OffsetY -= 5;

            l.Blocks.Add(temp);

            this.destroy(l);

        }


    }
}
