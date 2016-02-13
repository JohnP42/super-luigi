using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Super_Luigi.World.Blocks
{
    class SecretBlock : ItemBlock
    {

        public SecretBlock(int x, int y, Items.Item item) : base(x, y, item)
        {

            Colour = new Color(0, 0, 0, 0);

        }

    }
}
