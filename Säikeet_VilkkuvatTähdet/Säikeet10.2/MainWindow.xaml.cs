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
using System.Threading;
using System.Diagnostics.Metrics;

namespace Säikeet10._2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random= new Random();
  
      
        public MainWindow()
        {
            InitializeComponent();

            CreateStars(200);

            Thread starBlink = new Thread(MakeAStarBlink);
            starBlink.Start();      
        }

        private Ellipse SetAColour(Ellipse ellipse)
        {
            Random rand = new Random();
            int i = rand.Next(5);

            switch (i)
            {
                case 0:
                    ellipse.Fill = Brushes.White; break;
                case 1:
                    ellipse.Fill = Brushes.WhiteSmoke; break;
                case 2:
                    ellipse.Fill = Brushes.AntiqueWhite; break;
                case 3:
                    ellipse.Fill = Brushes.FloralWhite; break;
                case 4:
                    ellipse.Fill = Brushes.GhostWhite; break;

                default:
                ellipse.Fill = Brushes.Blue; break;

            }

            return ellipse;


        }      
        private void CreateStars(int starAmount)
        {
            Random rand = new Random();

            for (int i = 0; i < starAmount; i++)
            {
                Ellipse star = new Ellipse();
                star.Width = 2; star.Height = 2;
                SetAColour(star);
                Canvas.SetTop(star, rand.Next(500));
                Canvas.SetRight(star, rand.Next(1000));
                sky.Children.Add(star);

            }        
        }

       
        private void MakeAStarBlink()
        {
            double size = 2;
            Ellipse ellipse = null;
            while (simulationOn)
            {
                    

                if (size == 2)
                {
                    Thread.Sleep(random.Next(500, 3000));
                    if (simulationOn== false)
                    {
                        return;

                    }
                    Dispatcher.Invoke(() =>
                    {
                        ellipse = (Ellipse)sky.Children[random.Next(200)];
                    });           

                }
           
       
                switch (size)
                {
                    case 2:
                       

                        while (size < 5.5)
                        {
                            size += 0.5;

                            Dispatcher.Invoke(() =>
                            {  
                                ellipse.Width = size;
                                ellipse.Height = size;

                            });
                            Thread.Sleep(20);
                           
                        }
                        size = 5;

                        Thread.Sleep(random.Next(100, 300));
                        if (simulationOn == false)
                        {
                            return;

                        }
                       
                        break;
                    case 5:
                        while (size > 1.5)
                        {
                            Dispatcher.Invoke(() =>
                            {      
                                ellipse.Width = size;
                                ellipse.Height = size;

                            });
                            Thread.Sleep(20);
                            if (simulationOn == false)
                            {
                                return;

                            }

                            size -= 0.5;

                        }
                        size = 2;
                        break;

                }
              
               
            }
            
           

        }

       private bool simulationOn = true;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            simulationOn = false;
        }
    }
  
}
