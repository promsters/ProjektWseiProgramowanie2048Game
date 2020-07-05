using _2048GameLib.Model;
using _2048GameLib.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2048GameLib
{
    public class GameManager
    {
        private GameBoard Board;
        private IGameRenderer Renderer;

        public GameState State { get; private set; }

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

        public void RegisterKeyPress(Key key)
        {
            if (key == Key.Left)
                Board.MoveBlocks(GameBoard.Direction.Left);
            else if (key == Key.Right)
                Board.MoveBlocks(GameBoard.Direction.Right);
            else if (key == Key.Up)
                Board.MoveBlocks(GameBoard.Direction.Top);
            else if (key == Key.Down)
                Board.MoveBlocks(GameBoard.Direction.Bottom);
        }

        private void OnBoardSlotChanged(object sender, BoardSlotChangedEventArgs e)
        {
            Renderer.UpdateBoardSlot(e.Slot);
        }
    }


    public enum GameState
    {
        IN_PROGRESS = 0,
        ENDED
    }
}
