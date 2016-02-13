using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Super_Luigi.World.Blocks;
using Super_Luigi.World.Particles;
using Super_Luigi.World.Items;
using Super_Luigi.World.Enemies;

namespace Super_Luigi.World.Levels
{
    abstract class Level
    {
        int xSize, ySize;
        Texture2D sprBlocks;
        Texture2D sprItems;
        Texture2D sprEnemies;
        List<Block> blocks;
        List<Block> doodads;
        List<Item> items;
        List<Enemy> enemies;
        List<ParticleSystem> particleSystems;
        Song mainSong;

        public int XSize { get { return xSize; } set { xSize = value; } }
        public int YSize { get { return ySize; } set { ySize = value; } }
        public Texture2D SprBlocks { get { return sprBlocks; } set { sprBlocks = value; } }
        public Texture2D SprItems { get { return sprItems; } set { sprItems = value; } }
        public Texture2D SprEnemies { get { return sprEnemies; } set { sprEnemies = value; } }
        public List<Block> Blocks { get { return blocks; } set { blocks = value; } }
        public List<Block> Doodads { get { return doodads; } set { doodads = value; } }
        public List<Item> Items { get { return items; } set { items = value; } }
        public List<Enemy> Enemies { get { return enemies; } set { enemies = value; } }
        public List<ParticleSystem> ParticleSystems { get { return particleSystems; } set { particleSystems = value; } }
        public Song MainSong { get { return mainSong; } set { mainSong = value; } }

        public void disposeBlocksandItems()
        {
            for (int i = blocks.Count - 1; i >= 0; i--)
            {
                if (blocks[i].Destroyed)
                {
                    if(blocks[i] is BlockBrick)
                        particleSystems.Add(new ParticleSystemBrokenBrick(blocks[i].Position.X, blocks[i].Position.Y));
                    blocks.RemoveAt(i);
                }
            }

            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].Obtained)
                {
                    items.RemoveAt(i);
                }
            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (!enemies[i].Alive)
                {
                    enemies.RemoveAt(i);
                }
            }

        }

        public List<Block> getAdjacentBlocks(Block block)
        {
            List<Block> temp = new List<Block>();

            foreach (Block b in blocks)
            {
                if (((b.Position.X == block.Position.X - block.Position.Width || b.Position.X == block.Position.X + block.Position.Width) && b.Position.Y == block.Position.Y) || ((b.Position.Y == block.Position.Y - block.Position.Height || b.Position.Y == block.Position.Y + block.Position.Height) && b.Position.X == block.Position.X))
                {
                    temp.Add(b);
                }
                    
            }

            return temp;
        }

        public Item getItemAboveBlock(Block block)
        {
            foreach (Item i in items)
            {
                if (i.Velocity.Y != 0 || i is ItemCoin)
                    continue;

                if (new Rectangle(block.Position.X, block.Position.Y - block.Position.Height, block.Position.Width, block.Position.Height).Intersects(i.Position))
                    return i;
            }

            return null;
        }

        public Enemy getEnemyAboveBlock(Block block)
        {
            foreach (Enemy i in enemies)
            {
                if (i.Velocity.Y != 0 || i.Stomped != -1 || i.Hurt != -1)
                    continue;

                if (new Rectangle(block.Position.X, block.Position.Y - block.Position.Height, block.Position.Width, block.Position.Height).Intersects(i.Position))
                    return i;
            }

            return null;
        }

        public void update(Super_Luigi.Player.Luigi luigi)
        {
            if (luigi.Star == 1)
                MediaPlayer.Play(MainSong);

            foreach (Block b in blocks)
            {
                if (!b.Destroyed)
                    b.update();
            }

            foreach (Item i in items)
            {
                if (!i.Obtained)
                    i.update(this);
            }

            foreach (Enemy e in enemies)
            {
                if (e.Alive && MathHelper.Distance(luigi.Body.X, e.Position.X) <= 600 || (e is Koopa && ((Koopa)e).InShell) || e.Hurt != -1 || e.Stomped != -1)
                    e.update(this);
            }

            foreach (ParticleSystem p in particleSystems)
            {
                p.update();
            }

            for (int i = particleSystems.Count - 1; i >= 0; i--)
            {
                if (particleSystems[i].Particles.Count == 0)
                    particleSystems.RemoveAt(i);
            }

        }

        public void Draw(SpriteBatch spritebatch)
        {

            foreach (Block b in blocks)
            {
                if (!b.Destroyed)
                    spritebatch.Draw(sprBlocks, new Rectangle(b.Position.X + b.OffsetX, b.Position.Y + b.OffsetY, b.Position.Width, b.Position.Height), b.SrcRect[b.ImageIndex], b.Colour, 0f, Vector2.Zero, SpriteEffects.None, .9f);
            }

            foreach (Block b in doodads)
            {
                if (!b.Destroyed)
                    spritebatch.Draw(sprBlocks, new Rectangle(b.Position.X + b.OffsetX, b.Position.Y + b.OffsetY, b.Position.Width, b.Position.Height), b.SrcRect[b.ImageIndex], b.Colour, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            }

            foreach (Item i in items)
            {
                if (!i.Obtained)
                    spritebatch.Draw(sprItems, new Rectangle(i.Position.X + (i.Position.Width - i.SrcRect[i.ImageIndex].Width) / 2, i.Position.Y + (i.Position.Height - i.SrcRect[i.ImageIndex].Height) / 2, i.SrcRect[i.ImageIndex].Width, i.SrcRect[i.ImageIndex].Height), i.SrcRect[i.ImageIndex], Color.White, 0f, Vector2.Zero, SpriteEffects.None, .95f);
            }

            foreach (Enemy e in enemies)
            {
                if (e.Alive)
                    spritebatch.Draw(sprEnemies, new Vector2(e.Position.X + e.OffDraw.X, e.Position.Y + e.OffDraw.Y), e.SrcRect[e.ImageIndex], Color.White, 0f, Vector2.Zero, 1f, e.Flip, .5f);
            }

            foreach (ParticleSystem p in particleSystems)
            {
                p.draw(spritebatch);
            }

        }

    }
}
