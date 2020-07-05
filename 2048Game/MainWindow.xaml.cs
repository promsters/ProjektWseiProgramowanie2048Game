﻿using _2048GameLib;
using _2048GameLib.Model;
using System.Windows;
using System.Windows.Input;

namespace _2048Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameManager GameMngr;

        public MainWindow()
        {
            InitializeComponent();

            GameMngr = new GameManager(new GameRenderer(GamePanel, ScoreDisplayBoard));
            GameMngr.Start();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GameMngr.RegisterKeyPress(e.Key);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GameMngr.Restart();
        }
    }
}
