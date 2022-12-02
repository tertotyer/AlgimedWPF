using Algimed.Data;
using Algimed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Algimed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Page
    {
        AuthMainWindow MW = Application.Current.Windows.OfType<AuthMainWindow>().FirstOrDefault();
        ApplicationDbContext db;
        readonly List<User> users;

        public Login()
        {
            InitializeComponent();

            db = new ApplicationDbContext();
            users = db.Users.ToList();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxEmail.Text.Length == 0)
            {
                ErrorMessage.Text = "Enter an email.";
                TextBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(TextBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                ErrorMessage.Text = "Enter a valid email.";
                TextBoxEmail.Select(0, TextBoxEmail.Text.Length);
                TextBoxEmail.Focus();
            }
            else
            {
                string password = PasswordBox.Password;
                if (PasswordBox.Password.Length == 0)
                {
                    ErrorMessage.Text = "Enter password.";
                    PasswordBox.Focus();
                }
                else
                {
                    if (users.Where(x => x.Email == TextBoxEmail.Text && x.Password == PasswordBox.Password).FirstOrDefault() != null)
                    {
                        MainWindow main = new MainWindow();
                        main.Show();
                        MW.Close();
                    }
                    else
                    {
                        ErrorMessage.Text = "Invalid email or password.";
                        TextBoxEmail.Clear();
                        PasswordBox.Clear();
                    }
                }
            }

        }
    }
}
