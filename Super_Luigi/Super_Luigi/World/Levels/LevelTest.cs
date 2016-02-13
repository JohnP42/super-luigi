using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Luigi.World.Blocks;
using Super_Luigi.World.Items;
using Super_Luigi.World.Enemies;
using Super_Luigi.World.Blocks.BlockSystems;
using Super_Luigi.World.Blocks.BlockSystems.Doodads;

namespace Super_Luigi.World.Levels
{
    class LevelTest : Level
    {

        public LevelTest()
        {
            this.XSize = 212;
            this.YSize = 15;

            this.Blocks = new List<Block>();
            this.Doodads = new List<Block>();
            this.Items = new List<Item>();
            this.Enemies = new List<Enemy>();
            this.SprBlocks = Game1.sprBlockTiles;
            this.SprItems = Game1.sprItemTiles;
            this.SprEnemies = Game1.sprEnemies;
            MainSong = Game1.songs[0];

            this.ParticleSystems = new List<Particles.ParticleSystem>();

            addDoodads();

            for (int i = 0; i < YSize; i++)
                Blocks.Add(new BlockHill(-1, -i, 0));

            foreach (Block b in new BlockSystemHill(0, -2, 70, 3).Blocks)
            {
                Blocks.Add(b);
            }

            Blocks.Add(new ItemBlock(16, -6, new ItemCoin(16, -5, Vector2.Zero)));

            Enemies.Add(new Goomba(22, -3));

            Blocks.Add(new BlockBrick(20, -6));
            Blocks.Add(new ItemBlock(21, -6, new Mushroom(21, -6)));//Mushroom
            Blocks.Add(new BlockBrick(22, -6)); Blocks.Add(new ItemBlock(22, -10, new ItemCoin(22, -9, Vector2.Zero)));
            Blocks.Add(new ItemBlock(23, -6, new ItemCoin(23, -5, Vector2.Zero)));
            Blocks.Add(new BlockBrick(24, -6));

            foreach (Block b in new BlockSystemPipe(28, -4, 2).Blocks)
            {
                Blocks.Add(b);
            }

            foreach (Block b in new BlockSystemPipe(37, -5, 3).Blocks)
            {
                Blocks.Add(b);
            }

            Enemies.Add(new Goomba(39, -3));

            foreach (Block b in new BlockSystemPipe(44, -6, 4).Blocks)
            {
                Blocks.Add(b);
            }

            Enemies.Add(new Goomba(50, -3));
            Enemies.Add(new Goomba(52, -3));

            foreach (Block b in new BlockSystemPipe(54, -6, 4).Blocks) //warp down
            {
                Blocks.Add(b);
            }

            Blocks.Add(new SecretBlock(64, -7, new Mushroom(64, -7)));//Lifeshroom


            foreach (Block b in new BlockSystemHill(72, -2, 15, 3).Blocks)
            {
                Blocks.Add(b);
            }


            Blocks.Add(new BlockBrick(77, -6));
            Blocks.Add(new ItemBlock(78, -6, new Fireflower(78, -6)));//fireflower
            Blocks.Add(new BlockBrick(79, -6)); Enemies.Add(new Goomba(79, -8));

            for (int i = 80; i < 89; i++)
                Blocks.Add(new BlockBrick(i, -10));
            Enemies.Add(new Goomba(82, -11));


            foreach (Block b in new BlockSystemHill(90, -2, 64, 3).Blocks)
            {
                Blocks.Add(b);
            }

            Blocks.Add(new BlockBrick(93, -10));
            Blocks.Add(new BlockBrick(94, -10));
            Blocks.Add(new BlockBrick(95, -10));

            Blocks.Add(new CoinBlock(95, -6));//CoinBlock
            Enemies.Add(new Goomba(96, -3));
            Enemies.Add(new Goomba(97, -3));

            Blocks.Add(new BlockBrick(101, -6));
            Blocks.Add(new SecretBrick(102, -6, new Starman(102, -6)));//Star

            Enemies.Add(new Koopa(107, -3));

            Blocks.Add(new ItemBlock(107, -6, new ItemCoin(107, -5, Vector2.Zero)));
            Blocks.Add(new ItemBlock(110, -6, new ItemCoin(110, -5, Vector2.Zero))); Blocks.Add(new ItemBlock(110, -10, new Mushroom(110, -10)));//Mushroom
            Blocks.Add(new ItemBlock(113, -6, new ItemCoin(113, -5, Vector2.Zero)));

            Blocks.Add(new BlockBrick(119, -6));

            Blocks.Add(new BlockBrick(122, -10));
            Blocks.Add(new BlockBrick(123, -10));
            Blocks.Add(new BlockBrick(124, -10));

            Enemies.Add(new Goomba(125, -3));
            Enemies.Add(new Goomba(126, -3));

            Enemies.Add(new Goomba(128, -3));
            Enemies.Add(new Goomba(129, -3));

            Blocks.Add(new BlockBrick(129, -10));
            Blocks.Add(new BlockBrick(130, -6)); Blocks.Add(new ItemBlock(130, -10, new ItemCoin(130, -9, Vector2.Zero)));
            Blocks.Add(new BlockBrick(131, -6)); Blocks.Add(new ItemBlock(131, -10, new ItemCoin(131, -9, Vector2.Zero)));
            Blocks.Add(new BlockBrick(132, -10));

            //Steps
            foreach (Block b in new BlockSystemSteps(135, -6, 4, 4, true).Blocks)
            {
                Blocks.Add(b);
            }

            foreach (Block b in new BlockSystemSteps(141, -6, 4, 4, false).Blocks)
            {
                Blocks.Add(b);
            }

            foreach (Block b in new BlockSystemSteps(149, -6, 4, 4, true).Blocks)
            {
                Blocks.Add(b);
            }

            Blocks.Add(new SolidBlock(153, -6)); Blocks.Add(new SolidBlock(153, -5)); Blocks.Add(new SolidBlock(153, -4)); Blocks.Add(new SolidBlock(153, -3));


            foreach (Block b in new BlockSystemHill(158, -2, 56, 3).Blocks)
            {
                Blocks.Add(b);
            }

            foreach (Block b in new BlockSystemSteps(158, -6, 4, 4, false).Blocks)
            {
                Blocks.Add(b);
            }

            foreach (Block b in new BlockSystemPipe(165, -4, 2).Blocks) //warp up
            {
                Blocks.Add(b);
            }

            Blocks.Add(new BlockBrick(170, -6));
            Blocks.Add(new BlockBrick(171, -6));
            Blocks.Add(new ItemBlock(172, -6, new ItemCoin(172, -5, Vector2.Zero)));
            Blocks.Add(new BlockBrick(173, -6));

            Enemies.Add(new Goomba(174, -3));
            Enemies.Add(new Goomba(175, -3));

            foreach (Block b in new BlockSystemPipe(179, -4, 2).Blocks)
            {
                Blocks.Add(b);
            }

            foreach (Block b in new BlockSystemSteps(181, -10, 8, 8, true).Blocks)
            {
                Blocks.Add(b);
            }

            for (int i = -10; i < -2; i++)
                Blocks.Add(new SolidBlock(189, i));

            Blocks.Add(new SolidBlock(198, -3));

        }

        public void addDoodads()
        {

            foreach (Block b in new BlockSystemHillBunch(1, -4, 3, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(9, -11, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemBush(12, -3, 4).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemHillEyes(16, -3, 1).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(20, -12, 2).Blocks)
            {
                Doodads.Add(b);
            }

            Doodads.Add(new BlockBush(23, -3, 0));

            foreach (Block b in new BlockSystemCloud(28, -11, 4).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(36, -12, 3).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemBush(40, -3, 3).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemHillBunch(46, -4, 3, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(54, -11, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemBush(56, -3, 5).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemHillEyes(61, -4, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(65, -12, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemBush(72, -3, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(77, -11, 4).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(86, -12, 3).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemBush(90, -3, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemHillBunch(97, -5, 5, 3).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(105, -11, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemBush(108, -3, 4).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemHillEyes(112, -4, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(114, -13, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemBush(120, -3, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(124, -12, 4).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(133, -13, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemBush(138, -3, 4).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemHillBunch(146, -5, 5, 3).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(154, -11, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemBush(159, -3, 4).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemHillEyes(163, -3, 1).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(166, -12, 2).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemBush(169, -3, 3).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemCloud(174, -11, 4).Blocks)
            {
                Doodads.Add(b);
            }


            foreach (Block b in new BlockSystemCloud(183, -12, 3).Blocks)
            {
                Doodads.Add(b);
            }

            foreach (Block b in new BlockSystemHillBunch(192, -5, 5, 3).Blocks)
            {
                Doodads.Add(b);
            }

        }

    }
}
