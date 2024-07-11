using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace StudentRegister2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        private ObservableCollection<Student> students;


        private int index = 0;

        public MainWindow()
        {
            InitializeComponent();

            students = StudentList.GetAllStudents();

            comGender.ItemsSource = Gender.Genders;

            for (int i = 2015; i <= 2023; i++)
            {
                comYears.Items.Add(i);
            }


            this.DataContext = students[index];


        }

        private void VersionClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Opiskelijarekisteri v2.0 (c)Arben Grepi", "Tiedot");
        }
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            students[index].Credits++;

        }

        private void clickAlkuun(object sender, RoutedEventArgs e)
        {
            index = 0;
            this.DataContext = students[index];
        }

        private void clickTaakse(object sender, RoutedEventArgs e)
        {
            index--;
            if (index < 0)
            {
                index = 0;

            }
            this.DataContext = students[index];
        }

        private void clickEteenpäin(object sender, RoutedEventArgs e)
        {
            index++;
            if (index >= students.Count)
            {
                index = students.Count - 1;

            }
            this.DataContext = students[index];
        }

        private void clickLoppuun(object sender, RoutedEventArgs e)
        {
            index = students.Count - 1;
            this.DataContext = students[index];
        }

        private void txtNumber_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            bool ok = int.TryParse(txtNumber.Text, out int i);
            if (ok)
            {
                students[index].Number = i;

            }
        }
        private void txtName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            students[index].Name = txtName.Text;
        }


        public class Student : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            private int number;
            public int Number
            {
                get
                {
                    return number;
                }
                set
                {
                    number = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Number"));
                }
            }

            private string? name;
            public string? Name 
            {
                get 
                {
                    return name;
                }

                set
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
          

            public int StartYear { get; set; }

            public int Gender { get; set; }

            private int credits = 0;
            public int Credits
            {
                get
                {
                    return credits;
                }
                set
                {
                    credits = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Credits"));
                }
            }


        }
        public class Gender
        {
            public int ID { get; set; }
            public string Description { get; set; }
            public Gender(int iD, string description)
            {
                ID = iD;
                Description = description;

            }
            public static List<Gender> Genders
            {
                get
                {
                    var genders = new List<Gender>();
                    genders.Add(new Gender(1, "Mies"));
                    genders.Add(new Gender(2, "Nainen"));
                    genders.Add(new Gender(3, "Muu"));
                    return genders;

                }
            }
        }

        public class StudentList
        {


            public static ObservableCollection<Student> GetAllStudents()
            {
                //tässä haetaan tieto tietokannasta
                var stud1 = new Student();
                stud1.Number = 1;
                stud1.Name = "Mika";
                stud1.StartYear = 2020;
                stud1.Credits = 0;
                stud1.Gender = 1;

                var stud2 = new Student();
                stud2.Number = 2;
                stud2.Name = "Eero";
                stud2.StartYear = 2022;
                stud2.Credits = 0;
                stud2.Gender = 2;

                var stud3 = new Student();
                stud3.Number = 3;
                stud3.Name = "Allan";
                stud3.StartYear = 2021;
                stud3.Credits = 0;
                stud3.Gender = 1;

                var students = new ObservableCollection<Student>();
                students.Add(stud1);
                students.Add(stud2);
                students.Add(stud3);

                return students;

            }
        }

        private void ShowStudentList(object sender, RoutedEventArgs e)
        {
            var studentListView = new StudentListView(students);
            studentListView.ShowDialog();
            clickAlkuun(null, null);


        }
    }
}



