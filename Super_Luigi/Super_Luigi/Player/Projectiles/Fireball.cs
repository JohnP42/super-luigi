using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Super_Luigi.Player.Projectiles
{
    class Fireball : Projectile
    {

        public Fireball(int x, int y, bool facingRight)
        {
            this.CreateSound = Game1.otherSounds[8];
            this.Gravity = 0.5f;
            this.ImageCount = 0f;
            this.ImageIndex = 0;
            this.ImageSpeed = 1f;
            this.ImageTime = 10f;
            this.Alive = true;
            this.OffsetX = 0;
            this.OffsetY = 0;
            this.Position = new Rectangle(x, y, 12, 12);
            this.FacingRight = facingRight;

            if(facingRight)
                this.Velocity = new Vector2(7, 0);
            else
                this.Velocity = new Vector2(-7, 0);

            this.SrcRect = new Rectangle[4];
            SrcRect[0] = new Rectangle(0, 29, 14, 12);
            SrcRect[1] = new Rectangle(17, 27, 12, 14);
            SrcRect[2] = new Rectangle(33, 29, 14, 12);
            SrcRect[3] = new Rectangle(51, 30, 12, 14);

        }

    }
}
