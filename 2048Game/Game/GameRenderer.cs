using _2048GameLib.Model;
using _2048GameLib.Render;
using System.Collections.Generic;
using System.Drawing;
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

        public void Init(int size, Dictionary<Point, BoardSlot> slots)
        {
            Size = size;
            GameGrid.Children.Capacity = size * size;

            foreach (KeyValuePair<Point, BoardSlot> kv in slots)
            {
                BoardBlock block = new BoardBlock();

                GameGrid.Children.Add(block);

                Grid.SetColumn(block, kv.Key.X);
                Grid.SetRow(block, kv.Key.Y);
            }
        }

        private BoardBlock GetBoardBlock(Point point)
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
    }
}
