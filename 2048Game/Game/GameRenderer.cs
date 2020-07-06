using _2048GameLib.Model;
using _2048GameLib.Render;
using System.Collections.Generic;
using System.Drawing;
using Position = System.Drawing.Point;
using System.Windows;
using System.Windows.Controls;
using _2048Game.Controls;
using DomainScoreboard = _2048GameLib.Model.Scoreboard;
using GuiScoreboard = _2048Game.Controls.Scoreboard;
using _2048Game.Animation;
using System.Windows.Media;

namespace _2048Game
{
    public class GameRenderer : IGameRenderer
    {
        private int Size;
        private Grid GameGrid;
        private GuiScoreboard ScoreBoard;
        private FrameworkElement GameOverUi;

        public GameRenderer(Grid grid, GuiScoreboard scoreboard, FrameworkElement gameOverUi)
        {
            GameGrid = grid;
            ScoreBoard = scoreboard;
            GameOverUi = gameOverUi;
        }

        public void Init(int size, Dictionary<Position, BoardSlot> slots)
        {
            if (GameGrid.Children.Count > 0)
            {
                GameGrid.Children.Clear();
                FadeHelper.FadeOut(GameOverUi);
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

        public void UpdateBoardSlot(BoardSlot boardSlot, BoardSlotChangeOrigin origin)
        {
            BoardBlock bBlock = GetBoardBlock(boardSlot.GetPosition());

            if (boardSlot.IsEmpty())
            {
                bBlock.BlockColor = "";
                bBlock.BlockTextColor = "";
                bBlock.BlockValue = "";
            }
            else
            {
                Block block = boardSlot.GetBlock();

                if (origin == BoardSlotChangeOrigin.Spawned)
                    ScaleHelper.ScaleIn(bBlock);
                else if (origin == BoardSlotChangeOrigin.MergedInto)
                    ScaleHelper.Pulse(bBlock);

                bBlock.BlockColor = ColorTranslator.ToHtml(block.BackgroundColor);
                bBlock.BlockTextColor = ColorTranslator.ToHtml(block.Color);
                bBlock.BlockValue = block.Value.ToString();

            }
        }

        public void RenderGameEnded()
        {
            FadeHelper.FadeIn(GameOverUi);
        }

        public void UpdateScoreboard(DomainScoreboard domainScoreboard)
        {
            ScoreBoard.Score = domainScoreboard.Score.ToString();
            ScoreBoard.BestScore = domainScoreboard.BestScore.ToString();
        }
    }
}
