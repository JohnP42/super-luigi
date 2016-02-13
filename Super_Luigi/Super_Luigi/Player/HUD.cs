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
using Super_Luigi.World.Particles;

namespace Super_Luigi.Player
{
    class HUD
    {
        public HUD()
        {

        }

        public void draw(SpriteBatch spritebatch, Luigi luigi,Viewport v)
        {
            spritebatch.Begin();

            spritebatch.DrawString(Game1.font1, "coins X " + luigi.Coins, new Vector2(v.Width - 135 - ((luigi.Coins.ToString().Length) * 16) + 1, 6), Color.DarkGoldenrod, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spritebatch.DrawString(Game1.font1, "coins X " + luigi.Coins, new Vector2(v.Width - 135 - ((luigi.Coins.ToString().Length) * 16), 5), Color.Gold, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);

            spritebatch.DrawString(Game1.font1, "hp X ", new Vector2(6, 6), Color.DarkRed, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spritebatch.DrawString(Game1.font1, "hp X ", new Vector2(5, 5), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);

            for(int i = 0; i < luigi.Hp; i++)
            {
                spritebatch.Draw(Game1.sprLuigi, new Rectangle(70 + (i * 25), 5, 24, 24), new Rectangle(0, 0, 16, 16), Color.White);
            }

            spritebatch.End();
        }

    }
}
