namespace Desulaid
{
    using System;
    using System.Linq;
    using System.Windows;
    using Desulaid.Database;
    using Desulaid.Models;
    using Desulaid.Views;

    public partial class MainWindow : Window
    {
        private Account account;

        public MainWindow()
        {
            InitializeComponent();

            AddStudentToList.IsEnabled =
            SaveListToDb.IsEnabled =
            studentNameBox.IsEnabled =
            studentDateBox.IsEnabled =
            studentTimeBox.IsEnabled =
            studentDiscriptionBox.IsEnabled = false;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DBContext())
            {
                var window = new LoginWindow();
                window.ShowDialog();

                account = db.Account
                    .Where(x => x.Login == window.Login && x.Password == window.Password)
                    .FirstOrDefault();

                if (account == null)
                {
                    MessageBox.Show("Не приавильный логин или пароль", "Ошибка",
                                     MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Title = $"{account.LastName} {account.FirstName}, {account.GroupId.ToUpper()} ({account.Status})";
                loginButton.IsEnabled = false;

                if (account.Status == "ответственный" || account.Status == "админ")
                {
                    AddStudentToList.IsEnabled =
                    SaveListToDb.IsEnabled =
                    studentNameBox.IsEnabled =
                    studentDateBox.IsEnabled =
                    studentTimeBox.IsEnabled =
                    studentDiscriptionBox.IsEnabled = true;
                }                                
            }
        }

        private void AddStudentToList_Click(object sender, RoutedEventArgs e)
        {
            if (studentNameBox.Text.Length == 0
                || studentDateBox.Text.Length == 0
                || studentTimeBox.Text.Length == 0
                || studentDiscriptionBox.Text.Length == 0)
            {
                MessageBox.Show("Вы не заполнили все обязательные поля", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            studentList.Items.Add(new StudentModel
            {
                Name = studentNameBox.Text,
                Date = studentDateBox.Text,
                Time = studentTimeBox.Text,
                Description = studentDiscriptionBox.Text
            });

            studentNameBox.Text =
            studentDateBox.Text =
            studentTimeBox.Text =
            studentDiscriptionBox.Text = null;
        }

        private void SaveListToDb_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DBContext())
            {
                foreach (StudentModel i in studentList.Items)
                {
                    int space = i.Name.IndexOf(' ');
                    string lastName = i.Name.Substring(0, space);
                    string firstName = i.Name.Substring(space + 1);

                    db.Attendance.Add(new Attendance
                    {
                        Reason = i.Description,
                        StudentId = db.Account
                            .Where(x => x.FirstName == firstName 
                                     && x.LastName == lastName 
                                     && x.GroupId == account.GroupId)
                            .Select(x => x.Id)
                            .FirstOrDefault(),
                        Date = i.Date,
                        Time = i.Time
                    });
                }
                db.SaveChanges();
            }
        }

        private void studentNameBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            using (var db = new DBContext())
            {
                var students = db.Account
                    .Where(x => x.GroupId == account.GroupId);

                studentNameBox.Items.Clear();

                foreach (var i in students)
                {
                    studentNameBox.Items.Add($"{i.LastName} {i.FirstName}");
                }
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            studentList.Items.Clear();

            using (var db = new DBContext())
            {
                switch (searchTypeBox.Text)
                {
                   case "Отобразить все за определенную дату":
                        var students = db.Attendance
                                         .Where(x => x.Date == searchItemBox.Text)
                                         .Select(x => new StudentModel
                                         {
                                             Date = x.Date,
                                             Time = x.Time,
                                             Description = x.Reason,
                                             Name = db.Account
                                                     .Where(y => y.Id == x.StudentId)
                                                     .Select(y => y.LastName + " " + y.FirstName)
                                                     .FirstOrDefault()
                                         })
                                         .ToList();

                        foreach (var i in students)
                        {
                            studentList.Items.Add(i);
                        }
                        break;
                }
            }
        }
    }
}
