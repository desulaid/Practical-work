namespace Desulaid.Views
{
    using System.Windows;

    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public string Login
        {
            get { return loginBox.Text; }
        }

        public string Password
        {
            get { return passwordBox.Password; }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            var window = new RegWindow();
            window.ShowDialog();
        }
    }
}
