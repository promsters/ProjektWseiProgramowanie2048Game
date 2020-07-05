using System.Drawing;

namespace _2048GameLib.Enum
{
    public static class BlockStyle
    {
        public static Color GetBackgroundColorForValue(int value)
        {
            if (value == 2)
                return ColorTranslator.FromHtml("#eee4da");
            else if (value == 4)
                return ColorTranslator.FromHtml("#ede0c8");
            else if (value == 8)
                return ColorTranslator.FromHtml("#f2b179");
            else if (value == 16)
                return ColorTranslator.FromHtml("#f59563");
            else if (value == 32)
                return ColorTranslator.FromHtml("#f67c5f");
            else if (value == 64)
                return ColorTranslator.FromHtml("#f65e3b");
            else if (value == 128)
                return ColorTranslator.FromHtml("#edcf72");
            else if (value == 256)
                return ColorTranslator.FromHtml("#edcc61");
            else if (value == 512)
                return ColorTranslator.FromHtml("#edc850");
            else if (value == 1024)
                return ColorTranslator.FromHtml("#edc53f");
            else if (value == 2048)
                return ColorTranslator.FromHtml("#edc22e");
            else
                return ColorTranslator.FromHtml("#3c3a32");
        }

        public static Color GetColorForValue(int value)
        {
            if (value == 2)
                return ColorTranslator.FromHtml("#776e65");
            else if (value == 4)
                return ColorTranslator.FromHtml("#776e65");
            else
                return ColorTranslator.FromHtml("#f9f6f2");
        }
    }
}
