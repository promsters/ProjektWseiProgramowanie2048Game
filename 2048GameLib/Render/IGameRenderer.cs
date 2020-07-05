using _2048GameLib.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048GameLib.Render
{
    public interface IGameRenderer
    {
        void Init(int size, Dictionary<Point, BoardSlot> slots);
        void UpdateBoardSlot(BoardSlot boardSlot);
        void RenderGameEnded();
        void UpdateScoreboard(Scoreboard scoreboard);
    }
}
