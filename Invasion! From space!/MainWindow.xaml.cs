using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using System.Xml;
using System.IO;


namespace Invasion__From_space_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int deadMorsoCounter  = 0;
        bool gameOver;

        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer nukeTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            nukeTimer.Start();
            nukeTimer.Tick += nukeTimer_Tick;
            nukeTimer.Interval = TimeSpan.FromMilliseconds(200);

        }
        // private void GenerateEnemies()
        // {
        //     double enemySpace = this.Width / enemyCount;
        //     for(int i=0;i<enemyCount; i++)
        //     {
        //         Image newEnemy = CloneElement(newEnemy);
        //         newEnemy.Visibility = Visibility.Visible;
        //         PlayerCanvas.Children.Add(newEnemy);
        //         Canvas.SetLeft(newEnemy, enemySpace * i);
        //     }
        // }

        //     private void WriteHighscores()
        // {
        //     //File.WriteAllLines("Scores.txt")
        //     string[] scores = new string[3];
        //     if (scoreCounter > highScore1)
        //     {
        //         highScore3 = highScore2;
        //         highScore2 = highScore1;
        //         highScore1 = scoreCounter;
        //     }
        //     else if(scoreCounter > highScore3)
        //     {
        //         highScore3 = scoreCounter;
        //     }
        // }
        //
        // private void ReadHighscores()
        // {
        //     try
        //     {
        //
        //
        //         List<string> fileLines = File.ReadLines("Scores.txt").ToList();
        //         highScore1 = Int32.Parse(fileLines[0]);
        //         highScore2 = Int32.Parse(fileLines[1]);
        //         highScore3 = Int32.Parse(fileLines[2]);
        //     }
        //     catch()
        //     {
        //
        //     }
        // }
        List<Morso> morsot = new List<Morso>();
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            double offset2 = this.Width / 5;
            if (morsot.Count <= 0)
            {
                for (int i=0;i<5;i++)
                {
                    Morso mso = new Morso(this, offset2 * i);
                    morsot.Add(mso);
                }   
            }
            for(int i=0; i< morsot.Count; i++)
            {
                if (Canvas.GetBottom(morsot[i].Invader) <= 0)
                {

                    gameOver = true;
                }
                if (Canvas.GetLeft(heroship) + (heroship.Width / 2) - (deathray.Width / 2) > Canvas.GetLeft(morsot[i].Invader))
                {
                    if (Canvas.GetLeft(heroship) + (heroship.Width / 2) - (deathray.Width / 2) < Canvas.GetLeft(morsot[i].Invader) + morsot[i].Invader.ActualWidth)
                    {
                        if (deathray.Visibility == Visibility.Visible)
                        {
                            morsot[i].deleteMorso();
                            morsot.Remove(morsot[i]);
                            deadMorsoCounter++;                    
                            i--;

                        }
                    }
                }
               
            }
            if (gameOver == true)
            {
                MessageBox.Show("Peli ohi. Pistesaldosi on: " + deadMorsoCounter.ToString());
                Close();
            }

        }

        private void nukeTimer_Tick(object sender, EventArgs e)
        {
                deathray.Visibility = Visibility.Hidden;
                nukeTimer.Stop();

        }

        private void heroship_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {

                case Key.Left:
                    if (deathray.Visibility == Visibility.Hidden)
                        if (Canvas.GetLeft(heroship) > 0)
                        Canvas.SetLeft(heroship, Canvas.GetLeft(heroship) - 45);
                    break;
                case Key.Right:
                    if (deathray.Visibility == Visibility.Hidden)
                        if (Canvas.GetLeft(heroship) < 1072)
                        Canvas.SetLeft(heroship, Canvas.GetLeft(heroship) + 45);
                    break;

                case Key.Space:
                    if(deathray.Visibility == Visibility.Hidden)
                    {
                        deathray.Visibility = Visibility.Visible;
                        Canvas.SetBottom(deathray, Canvas.GetTop(heroship) -120);
                        Canvas.SetLeft(deathray, Canvas.GetLeft(heroship) + (heroship.Width/2) - (deathray.Width/2));
                        nukeTimer.Start();
                     }

                    break;


            }
        }
    }
}
