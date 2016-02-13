using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Super_Luigi.World.Blocks.BlockSystems
{
    class BlockSystemHill
    {

        List<Block> blocks;

        public List<Block> Blocks { get { return blocks; } set { blocks = value; } }

        public BlockSystemHill(int x, int y, int w, int h)
        {
            blocks = new List<Block>();

            for (int i = 0; i < w; i++)
            {
                for (int z = 0; z < h; z++)
                {
                    int index = 4;

                    if(i == 0 && z == 0)
                        index = 0;
                    else if (i != 0 && i != w - 1 && z == 0)
                        index = 1;
                    else if(i == w - 1 && z == 0)
                        index = 2;
                    else if (i == 0 && z != 0 && z != h - 1)
                        index = 3;
                    else if (i == w - 1 && z != 0 && z != h - 1)
                        index = 5;
                    else if (i == 0 && z == h - 1)
                        index = 6;
                    else if (i != 0 && i != w - 1 && z == h - 1)
                        index = 7;
                    else if (i == w - 1 & z == h - 1)
                        index = 8;
                    else
                        index = 4;

                    blocks.Add(new BlockHill(x + i, y + z, index));
                }
            }
        }

    }
}
