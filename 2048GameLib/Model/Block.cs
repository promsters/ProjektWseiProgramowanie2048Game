using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using _2048GameLib.Enum;

namespace _2048GameLib.Model
{
    public class Block
    {
        public Block(int value)
        {
            this.Value = value;
            this.Color = BlockStyle.GetColorForValue(value);
            this.BackgroundColor = BlockStyle.GetBackgroundColorForValue(value);
        }

        public int Value { get; private set; }
        public Color Color { get; private set; }

        public Color BackgroundColor { get; private set; }
    }
}
