using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Super_Luigi.World.Blocks.BlockSystems
{
    class BlockSystemSteps
    {
        List<Block> blocks;

        public List<Block> Blocks { get { return blocks; } set { blocks = value; } }

        public BlockSystemSteps(int x, int y, int w, int h, bool facingRight)
        {
            blocks = new List<Block>();

            for (int i = 0; i < w; i++)
            {
                for (int z = 0; z < h; z++)
                {

                    if (facingRight)
                    {
                        if (z + i >= h - 1)
                        {
                            blocks.Add(new SolidBlock(x + i, y + z));
                        }
                    }
                    else
                    {
                        if (z - i > -1)
                        {
                            blocks.Add(new SolidBlock(x + i, y + z));
                        }
                    }

                }
            }
        }

    }
}
