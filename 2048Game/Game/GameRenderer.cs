using _2048GameLib.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Controls;

namespace _2048Game
{
    public class GameRenderer
    {
        private Grid GameGrid;
        private GameBoard GameBoard;

        public GameRenderer(Grid grid, GameBoard board)
        {
            GameGrid = grid;
            GameBoard = board;

            Init();
        }

        private void Init()
        {
            foreach(KeyValuePair<Point, BoardSlot> kv in GameBoard.Slots)
            {
                BoardBlock block = new BoardBlock();

                GameGrid.Children.Add(block);

                block.BlockValue = "8";

                Grid.SetColumn(block, kv.Key.Y);
                Grid.SetRow(block, kv.Key.X);
            }
        }
    }
}
