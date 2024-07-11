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

namespace SWOT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                Rectangle rec = new Rectangle();
                rec.Height = 100;
                rec.Width = 100;
                rec.Stroke = Brushes.Black;
                rec.StrokeThickness = 5;
                rec.Margin = new Thickness(0,0, 5, 0);
                rec.Fill = Brushes.Pink;

                myStack.Children.Add(rec); 

            }
        }
    }
}
