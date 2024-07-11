using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace SäikeetDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NoextraThreadClick(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(3000);
        }

        private void WithExtraThreadClick(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(DoWork);
            thread.Start();
                
        }
        private void DoWork()
        {
            Thread.Sleep(3000);
            Debug.WriteLine("Säie valmis");
            Dispatcher.Invoke(() =>
            {
                Title = "Valmis";

            });
        }

        private async void AsyncAwaitClick(object sender, RoutedEventArgs e)
        {
           await DoWorkAsync();

        }
        private async Task DoWorkAsync()
        {
            await Task.Delay(3000);
            Debug.WriteLine("Await valmis");
            Title = "Await valmis";


        }

        private void LoadFileAsyncClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
