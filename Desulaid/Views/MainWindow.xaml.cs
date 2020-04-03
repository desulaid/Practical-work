namespace Desulaid
{
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

                Title = $"{account.LastName} {account.FirstName}, {account.GroupId.ToUpper()}";
                loginButton.IsEnabled = false;

                AddStudentToList.IsEnabled =
                SaveListToDb.IsEnabled =
                studentNameBox.IsEnabled =
                studentDateBox.IsEnabled =
                studentTimeBox.IsEnabled =
                studentDiscriptionBox.IsEnabled = true;
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

        }

        private void studentNameBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (account == null)
            {
                return;
            }

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
    }
}
