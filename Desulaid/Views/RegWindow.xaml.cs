namespace Desulaid.Views
{
    using Desulaid.Database;
    using System;
    using System.Linq;
    using System.Windows;

    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == null
                || passwordBox.Password == null
                || nameBox.Text == null
                || lastNameBox.Text == null
                || middleNameBox.Text == null
                || groupBox.Text == null)
            {
                MessageBox.Show("Заполните все поля", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (passwordBox.Password != passwordBoxCheck.Password)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var db = new DBContext())
            {
                var account = db.Account
                    .Where(x => x.Login == loginBox.Text)
                    .FirstOrDefault();

                if (account != null)
                {
                    MessageBox.Show("Аккаунт с этим логином уже зарегистрирован", "Ошибка",
                                     MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                db.Account.Add(new Account
                    {
                        Login = loginBox.Text,
                        Password = passwordBoxCheck.Password,
                        FirstName = nameBox.Text,
                        LastName = lastNameBox.Text,
                        MiddleNam = middleNameBox.Text,
                        GroupId = groupBox.Text,
                        Registration = DateTime.Now,
                        Status = "студент"
                    });

                db.SaveChanges();
            }

            loginBox.Text =
            passwordBoxCheck.Password =
            nameBox.Text =
            lastNameBox.Text =
            middleNameBox.Text =
            groupBox.Text = null;

            DialogResult = true;
        }
    }
}
