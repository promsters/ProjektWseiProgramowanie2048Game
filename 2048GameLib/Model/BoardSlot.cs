using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048GameLib.Model
{
    public class BoardSlot
    {
        private Point Position;
        private Block? Block = null;

        public BoardSlot(Point position)
        {
            Position = position;
        }

        public void PutBlock(Block block)
        {
            Block = block;
        }

        public void RemoveBlock()
        {
            Block = null;
        }

        public Block? GetBlock()
        {
            return Block;
        }

        public bool IsEmpty()
        {
            return Block == null;
        }

        public Point GetPosition()
        {
            return Position;
        }
    }
}
