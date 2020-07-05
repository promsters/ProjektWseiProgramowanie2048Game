using _2048GameLib.Model;
using _2048GameLib.Render;
using System.Collections.Generic;
using System.Drawing;
using Position = System.Drawing.Point;
using System.Windows;
using System.Windows.Controls;

namespace _2048Game
{
    public class GameRenderer : IGameRenderer
    {
        private int Size;
        private Grid GameGrid;

        public GameRenderer(Grid grid)
        {
            GameGrid = grid;
        }

        public void Init(int size, Dictionary<Position, BoardSlot> slots)
        {
            if (GameGrid.Children.Count > 0)
            {
                GameGrid.Children.Clear();
            }

            Size = size;
            GameGrid.Children.Capacity = size * size;

            foreach (KeyValuePair<Position, BoardSlot> kv in slots)
            {
                BoardBlock block = new BoardBlock();

                GameGrid.Children.Add(block);

                Grid.SetColumn(block, kv.Key.X);
                Grid.SetRow(block, kv.Key.Y);
            }
        }

        private BoardBlock GetBoardBlock(Position point)
        {
            return (BoardBlock)GameGrid.Children[(point.Y * Size) + point.X];
        }

        public void UpdateBoardSlot(BoardSlot boardSlot)
        {
            BoardBlock bBlock = GetBoardBlock(boardSlot.GetPosition());

            if (boardSlot.IsEmpty())
            {
                bBlock.BlockColor = "";
                bBlock.BlockValue = "";
            }
            else
            {
                Block block = boardSlot.GetBlock();

                bBlock.BlockColor = ColorTranslator.ToHtml(block.BackgroundColor);
                bBlock.BlockTextColor = ColorTranslator.ToHtml(block.Color);
                bBlock.BlockValue = block.Value.ToString();
            }
        }

        public void RenderGameEnded()
        {
            MessageBox.Show("Game over!");
        }
    }
}
