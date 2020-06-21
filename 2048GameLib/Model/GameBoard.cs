using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048GameLib.Model
{
    public class GameBoard
    {
        public int Size { get; private set; }
        public Dictionary<Point, BoardSlot> Slots { get; private set; }

        public GameBoard(int size)
        {
            Size = size;
            Slots = new Dictionary<Point, BoardSlot>();

            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for(int x = 0;x < Size;x++)
            {
                for(int y = 0;y < Size;y++)
                {
                    Slots.Add(new Point(x, y), new BoardSlot());
                }
            }
        }
    }
}
