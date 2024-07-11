using MySqlConnector;
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

namespace Tietokanta_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetDataFromMariaDB();
            GetDataFromSQLServerDb(); ;
        }

        private void GetDataClick(object sender, RoutedEventArgs e)
        {


        }
        private void GetDataFromMariaDB()
        {

            List<Student> students = new List<Student>();

            MySqlConnection conn = new MySqlConnection(@"Server=127.0.0.1; Port=3306; 
User ID=Test, Pwd=TestSS; Database=WpfDemo");
            conn.Open();

            using (MySqlCommand cmd = new MySqlCommand("Select number, name FROM students", conn))
            {
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var student = new Student();
                    student.Number = dr.GetInt32("number");
                    student.Name = dr.GetString("name");

                    students.Add(student);
                }
            }

    

            comStudents1.ItemsSource = students;



        }
        private void GetDataFromSQLServerDb()
        {

        }
    }
}
