using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;

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

        public void Clear()
        {
            Slots.Clear();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for(int y = 0;y < Size;y++)
            {
                for(int x = 0;x < Size;x++)
                {
                    Point p = new Point(x, y);
                    Slots.Add(p, new BoardSlot(p));
                }
            }
        }

        public void SpawnBlockRandomly(int value)
        {
            if (!IsAnyEmptySlot())
                return;

            List<BoardSlot> emptySlots = GetEmptySlots();

            SpawnBlock(value, emptySlots[Random.Next(0, emptySlots.Count-1)].GetPosition());
        }

        public bool IsAnyEmptySlot()
        {
            return GetEmptySlots().Count > 0;
        }

        private List<BoardSlot> GetEmptySlots()
        {
            List<BoardSlot> emptySlots = new List<BoardSlot>();

            foreach(KeyValuePair<Point, BoardSlot> kv in Slots)
            {
                if (kv.Value.IsEmpty())
                    emptySlots.Add(kv.Value);
            }

            return emptySlots;
        }

        public void SpawnBlock(int value, Point point)
        {
            SpawnBlock(point, new Block(value));
        }

        public void SpawnBlock(Point point, Block block)
        {
            Slots[point].PutBlock(block);
            OnBoardSlotChanged(BoardSlotChangedEventArgs.FromBoardSlot(Slots[point]));
        }

        public void MoveBlock(BoardSlot from, BoardSlot to)
        {
            to.PutBlock(from.GetBlock());
            from.PutBlock(null);

            OnBoardSlotChanged(BoardSlotChangedEventArgs.FromBoardSlot(from));
            OnBoardSlotChanged(BoardSlotChangedEventArgs.FromBoardSlot(to));
        }

        public void RemoveBlock(BoardSlot slot)
        {
            slot.RemoveBlock();
            OnBoardSlotChanged(BoardSlotChangedEventArgs.FromBoardSlot(slot));
        }

        public void TraverseSlots(Direction direction, Action<Point> action)
        {
            foreach (int y in GetTraversableCoords(direction))
            {
                foreach (int x in GetTraversableCoords(direction))
                {
                    action(new Point(x, y));
                }
            }
        }

        private List<int> GetTraversableCoords(Direction direction)
        {
            List<int> coords = new List<int>(Enumerable.Range(0, Size));

            if (direction == Direction.Bottom || direction == Direction.Right)
                coords.Reverse();

            return coords;
        }

        public Point FindFarthestPosition(Point currentPos, Vector2 vector)
        {
            Point previous;

            do
            {
                previous = currentPos;
                currentPos = new Point(currentPos.X + (int)vector.X, currentPos.Y + (int)vector.Y);
            } while (IsPointWithinBoundary(currentPos) && Slots[currentPos].IsEmpty());

            return previous;
        }

        public Point GetNextPosition(Point currentPosition, Vector2 vector)
        {
            Point nextPosition = new Point(currentPosition.X + (int)vector.X, currentPosition.Y + (int)vector.Y);

            if (!IsPointWithinBoundary(nextPosition))
                return currentPosition;

            return nextPosition;
        }

        private bool IsPointWithinBoundary(Point point)
        {
            return point.X >= 0 && point.X <= this.Size - 1 && point.Y >= 0 && point.Y <= this.Size - 1;
        }

        public Vector2 GetDirectionVector(Direction direction)
        {
            if (direction == Direction.Left)
                return new Vector2(-1, 0);
            else if (direction == Direction.Right)
                return new Vector2(1, 0);
            else if (direction == Direction.Top)
                return new Vector2(0, -1);
            else 
                return new Vector2(0, 1);
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

        public static BoardSlotChangedEventArgs FromBoardSlot(BoardSlot slot)
        {
            return new BoardSlotChangedEventArgs() { Position = slot.GetPosition(), Slot = slot };
        }
    }
}
