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

namespace _2048Game.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy Scoreboard.xaml
    /// </summary>
    public partial class Scoreboard : UserControl
    {
        public static readonly DependencyProperty ScoreProperty = DependencyProperty.Register("Score", typeof(string), typeof(Scoreboard));
        public static readonly DependencyProperty BestScoreProperty = DependencyProperty.Register("BestScore", typeof(string), typeof(Scoreboard));

        public string Score
        {
            get { return GetValue(ScoreProperty) as string; }
            set { SetValue(ScoreProperty, value); }
        }

        public string BestScore
        {
            get { return GetValue(BestScoreProperty) as string; }
            set { SetValue(BestScoreProperty, value); }
        }

        public Scoreboard()
        {
            InitializeComponent();
            Score = "0";
            BestScore = "0";
        }
    }
}
