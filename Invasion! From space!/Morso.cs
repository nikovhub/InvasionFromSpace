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
using System.ComponentModel;

namespace Invasion__From_space_
{
    public class Morso
    {
        //property
        public Image Invader;
        MainWindow parent;



        DispatcherTimer morsoTimer = new DispatcherTimer();

        public Morso(MainWindow _parent, double offset) //constructor
        {

            Invader = new Image();
            Invader.Source = ToBitmapSource(Resource1.Spaceinvader);
            parent = _parent;
            parent.PlayerCanvas.Children.Add(Invader);
            Canvas.SetLeft(Invader, offset);
            Canvas.SetBottom(Invader, parent.PlayerCanvas.Height - Resource1.Spaceinvader.Height);
            morsoTimer.Tick += MorsoTimer_Tick;
            morsoTimer.Interval = TimeSpan.FromMilliseconds(200);
            morsoTimer.Start();


        }
        public void deleteMorso()
        {
            parent.PlayerCanvas.Children.Remove(Invader);
        }

        public BitmapSource ToBitmapSource(System.Drawing.Bitmap source)
        {
            BitmapSource bitSrc = null;

            var hBitmap = source.GetHbitmap();

            try
            {
                bitSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Win32Exception)
            {
                bitSrc = null;
            }
            finally
            {

            }

            return bitSrc;
        }

        public void MorsoTimer_Tick(object sender, EventArgs e)
        {
            Random Rng = new Random();
            int dodge = Rng.Next(1, 11);
            if(parent.deadMorsoCounter == 0)
            {
                Canvas.SetBottom(Invader, Canvas.GetBottom(Invader) - 1);
            }
            Canvas.SetBottom(Invader, Canvas.GetBottom(Invader) - parent.deadMorsoCounter);
            if (parent.deadMorsoCounter > 16)
            {
                switch (dodge)
                {
                    case 1:
                        if (Canvas.GetLeft(Invader) > 0)
                            Canvas.SetLeft(Invader, Canvas.GetLeft(Invader) - 25);
                        break;

                    case 2:
                        if (Canvas.GetLeft(Invader) < 1072)
                            Canvas.SetLeft(Invader, Canvas.GetLeft(Invader) + 25);
                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:
                        if (Canvas.GetLeft(Invader) > 0)
                            Canvas.SetLeft(Invader, Canvas.GetLeft(Invader) - 25);
                        break;

                    case 6:

                        break;

                    case 7:

                        break;

                    case 8:
                        if (Canvas.GetLeft(Invader) < 1072)
                            Canvas.SetLeft(Invader, Canvas.GetLeft(Invader) + 25);
                        break;

                    case 9:

                        break;

                    case 10:

                        break;
                }
            }
        }
    }
}
