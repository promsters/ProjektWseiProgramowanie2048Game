using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace _2048GameLib.Model
{
    public class Block
    {
        public int Value { get; private set; }
        public Color Color { get; private set; }
    }
}
