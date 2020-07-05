using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048GameLib.Model
{
    public class Scoreboard
    {
        public event EventHandler ScoreChanged;

        private int _score;
        public int Score 
        {
            get => _score;
            private set
            {
                _score = value;
                if (_score > BestScore)
                    BestScore = _score;

                OnScoreChanged();
            }
        }

        public int BestScore { get; private set; }

        public Scoreboard() { }

        public void Add(int score)
        {
            Score += score;
        }

        public void ResetScore()
        {
            Score = 0;
        }

        protected virtual void OnScoreChanged()
        {
            EventHandler handler = ScoreChanged;
            handler?.Invoke(this, new EventArgs());
        }
    }
}
