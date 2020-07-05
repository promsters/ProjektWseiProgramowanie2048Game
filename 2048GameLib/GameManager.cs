using _2048GameLib.Model;
using _2048GameLib.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Input;
using static _2048GameLib.Model.GameBoard;

namespace _2048GameLib
{
    public class GameManager
    {
        private Random Random;

        private GameBoard Board;
        private IGameRenderer Renderer;

        private GameState _state;
        private Scoreboard Score;

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
            Random = new Random();

            Board = new GameBoard(4);
            Board.BoardSlotChanged += OnBoardSlotChanged;

            Renderer = renderer;
            Renderer.Init(Board.Size, Board.Slots);

            Score = new Scoreboard();
            Score.ScoreChanged += OnScoreChanged;

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
            Score.ResetScore();
            Renderer.Init(Board.Size, Board.Slots);
            Start();

            State = GameState.IN_PROGRESS;
        }

        public void MoveBlocks(Direction direction)
        {
            List<Point> alreadyMerged = new List<Point>();

            bool wasAnyModification = false;

            Board.TraverseSlots(direction, (Point p) =>
            {
                if (Board.Slots[p].IsEmpty())
                    return;

                Vector2 vector = Board.GetDirectionVector(direction);
                Point newPos = Board.FindFarthestPosition(p, vector);
                Point nextPos = Board.GetNextPosition(newPos, vector);
                bool wasMerged = false;
                bool wasMoved = false;

                if (nextPos != newPos && !Board.Slots[nextPos].IsEmpty() && !alreadyMerged.Contains(nextPos))
                {
                    wasMerged = MergeBlocks(Board.Slots[p], Board.Slots[nextPos], false);

                    if (wasMerged)
                        alreadyMerged.Add(nextPos);
                }

                if (!wasMerged && p != newPos)
                {
                    Board.MoveBlock(Board.Slots[p], Board.Slots[newPos]);
                    wasMoved = true;
                }

                if (!wasAnyModification)
                    wasAnyModification = wasMoved || wasMerged;
            });

            if (wasAnyModification)
                SpawnBlockRandomly();

            if (!Board.IsAnyEmptySlot() && !IsAnyBlockMergeAvailable())
            {
                State = GameState.ENDED;
            }
        }

        private bool MergeBlocks(BoardSlot from, BoardSlot to, bool onlySimulate)
        {
            if (from.GetBlock().Value == to.GetBlock().Value)
            {
                if (!onlySimulate)
                {
                    int newValue = from.GetBlock().Value * 2;
                    Board.SpawnBlock(to.GetPosition(), new Block(newValue));
                    Board.RemoveBlock(from);

                    Score.Add(newValue);
                }

                return true;
            }

            return false;
        }

        private void SpawnBlockRandomly()
        {
            if (Random.Next(1000) < 40)
                Board.SpawnBlockRandomly(4);
            else
                Board.SpawnBlockRandomly(2);
        }

        private bool IsAnyBlockMergeAvailable()
        {
            bool wasAnyMerge = false;

            foreach (Direction direction in System.Enum.GetValues(typeof(Direction)))
            {
                if (wasAnyMerge)
                    break;

                Board.TraverseSlots(direction, (Point p) =>
                {
                    if (wasAnyMerge)
                        return;

                    if (Board.Slots[p].IsEmpty())
                        return;

                    Vector2 vector = Board.GetDirectionVector(direction);
                    Point newPos = Board.FindFarthestPosition(p, vector);
                    Point nextPos = Board.GetNextPosition(newPos, vector);


                    if (nextPos != newPos && !Board.Slots[nextPos].IsEmpty())
                        wasAnyMerge = MergeBlocks(Board.Slots[p], Board.Slots[nextPos], true);
                });
            }

            return wasAnyMerge;
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

        private void OnScoreChanged(object sender, EventArgs e)
        {
            Renderer.UpdateScoreboard(Score);
        }
    }


    public enum GameState
    {
        IN_PROGRESS = 0,
        ENDED
    }
}
