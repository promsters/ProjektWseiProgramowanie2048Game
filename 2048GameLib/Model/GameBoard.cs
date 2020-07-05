using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

        private bool IsAnyEmptySlot()
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

        private void SpawnBlock(int value, Point point)
        {
            Slots[point].PutBlock(new Block(value));
            OnBoardSlotChanged(BoardSlotChangedEventArgs.FromBoardSlot(Slots[point]));
        }

        private void MoveBlock(BoardSlot from, BoardSlot to)
        {
            to.PutBlock(from.GetBlock());
            from.PutBlock(null);

            OnBoardSlotChanged(BoardSlotChangedEventArgs.FromBoardSlot(from));
            OnBoardSlotChanged(BoardSlotChangedEventArgs.FromBoardSlot(to));
        }

        public void MoveBlocks(Direction direction)
        {
            // TODO: Refactor this, game end should be checked after merging and if there is no room we should end the game
            if (!IsAnyEmptySlot())
                return;

            List<Point> alreadyMerged = new List<Point>();
            for (int y = direction == Direction.Bottom ? Size - 1 : 0; direction == Direction.Bottom ? y >= 0 : y < Size; y += (direction == Direction.Bottom ? -1 : 1))
            {
                for (int x = direction == Direction.Right ? Size-1 : 0; direction == Direction.Right ? x >= 0 : x < Size; x += (direction == Direction.Right ? -1 : 1))
                {
                    Point p = new Point(x, y);

                    if (Slots[p].IsEmpty())
                        continue;

                    Vector2 vector = GetDirectionVector(direction);
                    Point newPos = FindFarthestPosition(p, vector);
                    Point nextPos = GetNextPosition(newPos, vector);
                    bool wasMerged = false;

                    if( nextPos != newPos && !Slots[nextPos].IsEmpty() && !alreadyMerged.Contains(nextPos) )
                    {
                        wasMerged = MergeBlocks(Slots[p], Slots[nextPos]);

                        if (wasMerged)
                            alreadyMerged.Add(nextPos);
                    }
                    
                    if (!wasMerged && p != newPos)
                    {
                        MoveBlock(Slots[p], Slots[newPos]);
                    }
                }
            }

            SpawnBlockRandomly(2);
        }

        private bool MergeBlocks(BoardSlot from, BoardSlot to)
        {
            // Check if can be merged
            if (from.GetBlock().Value == to.GetBlock().Value)
            {
                to.PutBlock(new Block(from.GetBlock().Value * 2));
                from.RemoveBlock();

                OnBoardSlotChanged(BoardSlotChangedEventArgs.FromBoardSlot(from));
                OnBoardSlotChanged(BoardSlotChangedEventArgs.FromBoardSlot(to));

                return true;
            }

            return false;
        }

        private Point FindFarthestPosition(Point currentPos, Vector2 vector)
        {
            Point previous;

            do
            {
                previous = currentPos;
                currentPos = new Point(currentPos.X + (int)vector.X, currentPos.Y + (int)vector.Y);
            } while (IsPointWithinBoundary(currentPos) && Slots[currentPos].IsEmpty());

            return previous;
        }

        private Point GetNextPosition(Point currentPosition, Vector2 vector)
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

        private Vector2 GetDirectionVector(Direction direction)
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
