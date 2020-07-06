using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2048Game
{
    public partial class BoardBlock : UserControl
    {
        public static readonly DependencyProperty BlockColorProperty = DependencyProperty.Register("BlockColor", typeof(string), typeof(BoardBlock));
        public static readonly DependencyProperty BlockTextColorProperty = DependencyProperty.Register("BlockTextColor", typeof(string), typeof(BoardBlock));
        public static readonly DependencyProperty BlockValueProperty = DependencyProperty.Register("BlockValue", typeof(string), typeof(BoardBlock));
        public static readonly DependencyProperty BlockFontSizeProperty = DependencyProperty.Register("BlockFontSize", typeof(int), typeof(BoardBlock));


        public string BlockColor
        {
            get { return GetValue(BlockColorProperty) as string; }
            set 
            {
                if (value.Length == 0)
                    value = "#00FFFFFF";
                SetValue(BlockColorProperty, value);
            }
        }

        public string BlockValue
        {
            get { return GetValue(BlockValueProperty) as string; }
            set { 
                SetValue(BlockValueProperty, value);
                BlockFontSize = 55 - (value.Length < 2 ? 0 : value.Length - 2) * 10;
            }
        }

        public string BlockTextColor
        {
            get { return GetValue(BlockTextColorProperty) as string; }
            set
            {
                if (value.Length == 0)
                    value = "#00FFFFFF";
                SetValue(BlockTextColorProperty, value);
            }
        }

        public int BlockFontSize
        {
            get { return (int)GetValue(BlockFontSizeProperty); }
            set { SetValue(BlockFontSizeProperty, value); }
        }

        public BoardBlock()
        {
            InitializeComponent();


            this.RenderTransform = new TransformGroup();
            (this.RenderTransform as TransformGroup).Children.Add(new ScaleTransform(1, 1));
            (this.RenderTransform as TransformGroup).Children.Add(new TranslateTransform(0, 0));
        }
    }
}
