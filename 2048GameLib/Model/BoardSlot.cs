using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048GameLib.Model
{
    public class BoardSlot
    {
        private Block? Block = null;

        public BoardSlot()
        {

        }

        public void PutBlock(Block block)
        {
            Block = block;
        }

        public Block? GetBlock()
        {
            return Block;
        }

        public bool IsEmpty()
        {
            return Block == null;
        }
    }
}
