using _2048GameLib.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            GameBoard.BoardSlotChanged += OnBoardSlotChanged;

            Init();
        }

        private void Init()
        {
            GameGrid.Children.Capacity = GameBoard.Size * GameBoard.Size;

            foreach (KeyValuePair<Point, BoardSlot> kv in GameBoard.Slots)
            {
                BoardBlock block = new BoardBlock();

                GameGrid.Children.Add(block);

                Grid.SetColumn(block, kv.Key.X);
                Grid.SetRow(block, kv.Key.Y);
            }
        }

        private BoardBlock GetBoardBlock(Point point)
        {
            return (BoardBlock)GameGrid.Children[(point.Y * GameBoard.Size) + point.X];
        }

        public void OnBoardSlotChanged(object sender, BoardSlotChangedEventArgs e)
        {
            BoardBlock bBlock = GetBoardBlock(e.Position);
            
            if( e.Slot.IsEmpty() )
            {
                bBlock.BlockColor = "";
                bBlock.BlockValue = "";
            }
            else
            {
                Block block = e.Slot.GetBlock();

                bBlock.BlockColor = ColorTranslator.ToHtml(block.BackgroundColor);
                bBlock.BlockTextColor = ColorTranslator.ToHtml(block.Color);
                bBlock.BlockValue = block.Value.ToString();
            }
        }
    }
}
