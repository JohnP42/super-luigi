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
using Super_Luigi.Player;

namespace Super_Luigi.World.Auras
{
    class Aura
    {
        Vector2 position;
        Rectangle[] srcRect;
        int imageIndex;
        float imageTime, imageSpeed, imageCount;
        int offsetX, offsetY;

        public Vector2 Position { get { return position; } set { position = value; } }
        public Rectangle[] SrcRect { get { return srcRect; } set { srcRect = value; } }
        public int ImageIndex { get { return imageIndex; } set { imageIndex = value; } }
        public float ImageTime { get { return imageTime; } set { imageTime = value; } }
        public float ImageSpeed { get { return imageSpeed; } set { imageSpeed = value; } }
        public float ImageCount { get { return imageCount; } set { imageCount = value; } }
        public int OffsetX { get { return offsetX; } set { offsetX = value; } }
        public int OffsetY { get { return offsetY; } set { offsetY = value; } }

        public Aura(String s, Luigi l)
        {
            position = new Vector2(l.Body.X, l.Body.Y);
            imageIndex = 0;
            imageTime = 10f;
            imageSpeed = 1.4f;
            imageCount = 0f;
            offsetX = 0;
            offsetY = 0;

            srcRect = new Rectangle[8];
            if (s == "Fire")
            {
                srcRect[0] = new Rectangle(9, 580, 54, 26);
                srcRect[1] = new Rectangle(66, 542, 64, 64);
                srcRect[2] = new Rectangle(133, 505, 80, 101);
                srcRect[3] = new Rectangle(216, 504, 88, 99);
                srcRect[4] = new Rectangle(307, 504, 80, 102);
                srcRect[5] = new Rectangle(390, 510, 80, 96);
                srcRect[6] = new Rectangle(473, 527, 74, 79);
                srcRect[7] = new Rectangle(550, 553, 74, 53);
            }
            else if (s == "Star")
            {
                srcRect[0] = new Rectangle(7, 219, 54, 26);
                srcRect[1] = new Rectangle(64, 181, 64, 64);
                srcRect[2] = new Rectangle(131, 144, 80, 101);
                srcRect[3] = new Rectangle(214, 146, 88, 99);
                srcRect[4] = new Rectangle(305, 143, 80, 102);
                srcRect[5] = new Rectangle(388, 149, 80, 96);
                srcRect[6] = new Rectangle(471, 166, 74, 79);
                srcRect[7] = new Rectangle(548, 192, 74, 53);
            }

        }

        public void update(Level l, Luigi luigi)
        {

            position = new Vector2(luigi.Body.X - srcRect[imageIndex].Width / 2 + luigi.Body.Width / 2, luigi.Body.Bottom - srcRect[imageIndex].Height + luigi.Body.Height / 6);

            animate();

        }

        public void animate()
        {
            if (imageCount >= imageTime)
            {
                if (imageIndex < srcRect.Length - 1)
                {
                    imageIndex++;
                }
                else
                    imageIndex = 0;
                imageCount = 0;
            }
            else
                imageCount += imageSpeed;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Game1.sprAuras, position, srcRect[imageIndex], Color.White * .75f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

    }
}
