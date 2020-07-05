using _2048GameLib.Model;
using _2048GameLib.Render;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Input;
using static _2048GameLib.Model.GameBoard;

namespace _2048GameLib
{
    public class GameManager
    {
        private GameBoard Board;
        private IGameRenderer Renderer;

        private GameState _state;
        public GameState State 
        { 
            get { return _state; }
            private set
            {
                GameState oldState = _state;
                _state = value;

                OnGameStateChange(oldState, _state);
            }
        }

        public GameManager(IGameRenderer renderer)
        {
            Board = new GameBoard(4);
            Board.BoardSlotChanged += OnBoardSlotChanged;

            Renderer = renderer;
            Renderer.Init(Board.Size, Board.Slots);

            State = GameState.IN_PROGRESS;
        }

        public void Start()
        {
            Board.SpawnBlockRandomly(2);
            Board.SpawnBlockRandomly(2);
        }

        public void Restart()
        {
            Board.Clear();
            Renderer.Init(Board.Size, Board.Slots);
            Start();
        }

        public void MoveBlocks(Direction direction)
        {
            List<Point> alreadyMerged = new List<Point>();

            Board.TraverseSlots(direction, (Point p) =>
            {
                if (Board.Slots[p].IsEmpty())
                    return;

                Vector2 vector = Board.GetDirectionVector(direction);
                Point newPos = Board.FindFarthestPosition(p, vector);
                Point nextPos = Board.GetNextPosition(newPos, vector);
                bool wasMerged = false;

                if (nextPos != newPos && !Board.Slots[nextPos].IsEmpty() && !alreadyMerged.Contains(nextPos))
                {
                    wasMerged = MergeBlocks(Board.Slots[p], Board.Slots[nextPos]);

                    if (wasMerged)
                        alreadyMerged.Add(nextPos);
                }

                if (!wasMerged && p != newPos)
                {
                    Board.MoveBlock(Board.Slots[p], Board.Slots[newPos]);
                }
            });

            Board.SpawnBlockRandomly(2);

            if (!Board.IsAnyEmptySlot() && !IsAnyBlockMergeAvailable())
            {
                State = GameState.ENDED;
            }
        }

        private bool MergeBlocks(BoardSlot from, BoardSlot to)
        {
            if (from.GetBlock().Value == to.GetBlock().Value)
            {
                Board.SpawnBlock(to.GetPosition(), new Block(from.GetBlock().Value * 2));
                Board.RemoveBlock(from);

                return true;
            }

            return false;
        }

        private bool IsAnyBlockMergeAvailable()
        {
            return false;
        }

        public void RegisterKeyPress(Key key)
        {
            if (State == GameState.IN_PROGRESS)
            {
                if (key == Key.Left)
                    MoveBlocks(GameBoard.Direction.Left);
                else if (key == Key.Right)
                    MoveBlocks(GameBoard.Direction.Right);
                else if (key == Key.Up)
                    MoveBlocks(GameBoard.Direction.Top);
                else if (key == Key.Down)
                    MoveBlocks(GameBoard.Direction.Bottom);
            }
        }

        private void OnBoardSlotChanged(object sender, BoardSlotChangedEventArgs e)
        {
            Renderer.UpdateBoardSlot(e.Slot);
        }

        private void OnGameStateChange(GameState oldState, GameState newState)
        {
            if (newState == GameState.ENDED && oldState == GameState.IN_PROGRESS)
                Renderer.RenderGameEnded();
        }
    }


    public enum GameState
    {
        IN_PROGRESS = 0,
        ENDED
    }
}
