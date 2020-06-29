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
        public enum Direction { Left = 0, Right, Top, Bottom };

        public event EventHandler<BoardSlotChangedEventArgs> BoardSlotChanged;

        public int Size { get; private set; }
        public Dictionary<Point, BoardSlot> Slots { get; private set; }

        private Random Random;

        public GameBoard(int size)
        {
            Size = size;
            Slots = new Dictionary<Point, BoardSlot>();
            Random = new Random();

            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for(int y = 0;y < Size;y++)
            {
                for(int x = 0;x < Size;x++)
                {
                    Slots.Add(new Point(x, y), new BoardSlot());
                }
            }
        }

        public void StartRound()
        {
            SpawnBlockRandomly(2);
            SpawnBlockRandomly(2);
        }

        private void SpawnBlockRandomly(int value)
        {
            Point p;
            do
            {
                p = new Point(Random.Next(0, Size - 1), Random.Next(0, Size - 1));
            } while (!Slots[p].IsEmpty());

            Slots[p].PutBlock(new Block(value, ColorTranslator.FromHtml("#eee4da")));
            OnBoardSlotChanged(new BoardSlotChangedEventArgs() { Position = p, Slot = Slots[p] });
        }

        public void MergeBlocks(Direction direction)
        {
            
        }

        protected virtual void OnBoardSlotChanged(BoardSlotChangedEventArgs e)
        {
            EventHandler<BoardSlotChangedEventArgs> handler = BoardSlotChanged;
            handler?.Invoke(this, e);
        }
    }

    public class BoardSlotChangedEventArgs : EventArgs
    {
        public Point Position { get; set; }
        public BoardSlot Slot { get; set; }
    }
}
