using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Super_Luigi.World.Blocks.BlockSystems.Doodads
{
    class BlockSystemCloud
    {
        List<Block> blocks;

        public List<Block> Blocks { get { return blocks; } set { blocks = value; } }

        public BlockSystemCloud(int x, int y, int w)
        {
            blocks = new List<Block>();

            for (int i = 0; i < w; i++)
            {
                int index = 0;

                if (i == 0)
                    index = 0;
                else if (i == w - 1)
                    index = 2;
                else
                    index = 1;

                blocks.Add(new BlockCloud(x + i, y, index));
            }
        }
    }
}
