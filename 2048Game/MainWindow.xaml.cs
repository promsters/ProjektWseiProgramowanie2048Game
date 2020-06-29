using _2048GameLib.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameBoard Board;
        public GameRenderer Renderer;

        public MainWindow()
        {
            InitializeComponent();

            Board = new GameBoard(4);
            Renderer = new GameRenderer(GamePanel, Board);

            Board.StartRound();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if( e.Key == Key.Left )
                Board.MergeBlocks(GameBoard.Direction.Left);
            else if( e.Key == Key.Right )
                Board.MergeBlocks(GameBoard.Direction.Right);
            else if( e.Key == Key.Up )
                Board.MergeBlocks(GameBoard.Direction.Top);
            else if( e.Key == Key.Down )
                Board.MergeBlocks(GameBoard.Direction.Bottom);
        }
    }
}
