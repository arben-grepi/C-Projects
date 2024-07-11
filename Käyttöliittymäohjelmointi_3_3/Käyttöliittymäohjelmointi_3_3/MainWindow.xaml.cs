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

namespace Käyttöliittymäohjelmointi_3_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            drawingCanvas.Focus();

            SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);

            Ellipse head = new Ellipse();
            head.Width = 50;
            head.Height = 50;
            head.Stroke = blackBrush;
            Canvas.SetLeft(head, 625);
            Canvas.SetTop(head, 200);

            Line body = new Line();
            body.Y2 = 100;
            body.Stroke = blackBrush;
            Canvas.SetLeft(body, 650);
            Canvas.SetTop(body, 250);

            Line leftArm = new Line();
            leftArm.Y2 = 100;
            leftArm.X2 = 50;
            leftArm.Stroke = blackBrush;
            Canvas.SetLeft(leftArm, 650);
            Canvas.SetTop(leftArm, 250);

            Line rightArm = new Line();
            rightArm.Y2 = 100;
            rightArm.X2 = -50;
            rightArm.Stroke = blackBrush;
            Canvas.SetLeft(rightArm, 650);
            Canvas.SetTop(rightArm, 250);

            Line rightLeg = new Line();
            rightLeg.Y2 = 100;
            rightLeg.X2 = -50;
            rightLeg.Stroke = blackBrush;
            Canvas.SetLeft(rightLeg, 650);
            Canvas.SetTop(rightLeg, 350);

            Line leftLeg = new Line();
            leftLeg.Y2 = 100;
            leftLeg.X2 = 50;
            leftLeg.Stroke = blackBrush;
            Canvas.SetLeft(leftLeg, 650);
            Canvas.SetTop(leftLeg, 350);

            drawingCanvas.Children.Add(head);
            drawingCanvas.Children.Add(body);
            drawingCanvas.Children.Add(leftArm);
            drawingCanvas.Children.Add(rightArm);
            drawingCanvas.Children.Add(rightLeg);
            drawingCanvas.Children.Add(leftLeg);

            Title = "Oikea sana: kayttoliittyma";

        }

        private int virheLaskuri = 0;
        private int keyLaskuri = 1;

        private void LisääElementtiHirttoPuuhun(int virhe)
        {
            if (virhe == 1)
            {
                Ellipse maa = new Ellipse();
                maa.Width = 400;
                maa.Height = 150;
                maa.Fill = Brushes.Green;
                Canvas.SetLeft(maa, 150);
                Canvas.SetTop(maa, 510);

                drawingCanvas.Children.Add(maa);

            }

            else if (virhe == 2)
            {
                Line Pole = new Line();
                Pole.Y2 = -407;
                Pole.Stroke = Brushes.Black;
                Pole.StrokeThickness = 8;
                Canvas.SetLeft(Pole, 350);
                Canvas.SetTop(Pole, 510);

                drawingCanvas.Children.Add(Pole);
            }

            else if (virhe == 3)
            {
                Line support = new Line();
                support.X2 = 80;
                support.Y2 = -100;
                support.Stroke = Brushes.Black;
                support.StrokeThickness = 8;
                Canvas.SetLeft(support, 340);
                Canvas.SetTop(support, 200);

                drawingCanvas.Children.Add(support);

            }

            else if (virhe == 4)
            {
                Line Stick = new Line();
                Stick.X2 = 300;
                Stick.Stroke = Brushes.Black;
                Stick.StrokeThickness = 8;
                Canvas.SetLeft(Stick, 350);
                Canvas.SetTop(Stick, 100);

                drawingCanvas.Children.Add(Stick);
            }

            else if (virhe == 5)
            {
                Line rope = new Line();
                rope.Y2 = 95;
                rope.Stroke = Brushes.Black;
                rope.StrokeThickness = 2;
                Canvas.SetLeft(rope, 650);
                Canvas.SetTop(rope, 100);

                drawingCanvas.Children.Add(rope);

                MessageBox.Show("Game over");

            }
        }


        private void canvasKeyDown(object sender, KeyEventArgs e)
        {
            if (keyLaskuri == 1 && e.Key == Key.O)
            {
                keyLaskuri++;

                label1.Content = e.Key.ToString();
            }
            else if (keyLaskuri == 2 && e.Key == Key.H)
            {
                keyLaskuri++;

                label2.Content = e.Key.ToString();

            }

            else if (keyLaskuri == 3 && e.Key == Key.J)
            {
                keyLaskuri++;

                label3.Content = e.Key.ToString();

            }

            else if (keyLaskuri == 4 && e.Key == Key.E)
            {
                keyLaskuri++;

                label4.Content = e.Key.ToString();

            }
            else if (keyLaskuri == 5 && e.Key == Key.L)
            {
                keyLaskuri++;

                label5.Content = e.Key.ToString();

            }
            else if (keyLaskuri == 6 && e.Key == Key.M)
            {
                keyLaskuri++;

                label6.Content = e.Key.ToString();

            }
            else if (keyLaskuri == 7 && e.Key == Key.O)
            {
                keyLaskuri++;

                label7.Content = e.Key.ToString();

            }
            else if (keyLaskuri == 8 && e.Key == Key.I)
            {
                keyLaskuri++;

                label8.Content = e.Key.ToString();

            }
            else if (keyLaskuri == 9 && e.Key == Key.N)
            {
                keyLaskuri++;

                label9.Content = e.Key.ToString();

            }
            else if (keyLaskuri == 10 && e.Key == Key.T)
            {
                keyLaskuri++;

                label10.Content = e.Key.ToString();

            }
            else if (keyLaskuri == 11 && e.Key == Key.I)
            {
                keyLaskuri++;

                label11.Content = e.Key.ToString();

                MessageBox.Show("Jihuu voitto");
            }       
            else
            {
                virheLaskuri++;

                LisääElementtiHirttoPuuhun(virheLaskuri);
            }
        }
    }
}

























