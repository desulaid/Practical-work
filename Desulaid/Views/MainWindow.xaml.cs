namespace Desulaid
{
    using System.Linq;
    using System.Windows;
    using Desulaid.Database;
    using Desulaid.Models;
    using Desulaid.Views;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Ligin_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DBContext())
            {
                var window = new LoginWindow();
                window.ShowDialog();

                var account = db.Account
                    .Where(x => x.Login == window.Login && x.Password == window.Password)
                    .FirstOrDefault();

                if (account == null)
                {
                    MessageBox.Show("Не приавильный логин или пароль", "Ошибка",
                                     MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Title = $"{account.LastName} {account.FirstName}, {account.GroupId.ToUpper()}";
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
            using (var db = new DBContext())
            {
                var students = db.Account
                    .Where(x => x.GroupId == "п-41");

                studentNameBox.Items.Clear();

                foreach (var i in students)
                {
                    studentNameBox.Items.Add($"{i.LastName} {i.FirstName}");
                }
            }
        }
    }
}
