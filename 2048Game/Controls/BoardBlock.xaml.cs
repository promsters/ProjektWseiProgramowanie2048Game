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
            set { SetValue(BlockValueProperty, value); }
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

        public BoardBlock()
        {
            InitializeComponent();
        }
    }
}
