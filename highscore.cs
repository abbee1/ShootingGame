using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingGame
{
    class highscore
    {
        public int ReadScore()
        {
            StreamReader reader = new StreamReader("score.txt");
            string score = reader.ReadLine();
            reader.Close();
            return Convert.ToInt32(score);
        }
        public void PrintScore(int score)
        {
            StreamWriter writer = new StreamWriter("score.txt", false);
            writer.WriteLine(score);
            writer.Close();
        }
    }
}
