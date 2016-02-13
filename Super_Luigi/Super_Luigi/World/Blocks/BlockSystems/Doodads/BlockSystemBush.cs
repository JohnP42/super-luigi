using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Super_Luigi.World.Blocks.BlockSystems.Doodads
{
    class BlockSystemBush
    {
        List<Block> blocks;

        public List<Block> Blocks { get { return blocks; } set { blocks = value; } }

        public BlockSystemBush(int x, int y, int w)
        {
            blocks = new List<Block>();

            for (int i = 0; i < w; i++)
            {
                int index = 0;

                if (w != 0)
                {
                    if (i == 0)
                        index = 1;
                    else if (i == w - 1)
                        index = 3;
                    else
                        index = 2;
                }

                blocks.Add(new BlockBush(x + i, y, index));
            }
        }
    }
}
