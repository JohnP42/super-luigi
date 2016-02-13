using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Super_Luigi.World.Blocks.BlockSystems
{
    class BlockSystemPipe
    {

        List<Block> blocks;

        public List<Block> Blocks { get { return blocks; } set { blocks = value; } }

        public BlockSystemPipe(int x, int y, int h)
        {
            blocks = new List<Block>();

            for (int i = 0; i < 2; i++)
            {
                for (int z = 0; z < h; z++)
                {
                    int index = 0;

                    if (i == 0 && z == 0)
                        index = 0;
                    else if (i == 1 && z == 0)
                        index = 1;
                    else if (i == 0 && z != 0)
                        index = 2;
                    else
                        index = 3;

                    blocks.Add(new BlockPipe(x + i, y + z, index));
                }
            }
        }

    }
}
