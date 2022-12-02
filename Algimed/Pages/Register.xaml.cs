using Algimed.Data;
using Algimed.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace Algimed
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        AuthMainWindow MW = Application.Current.Windows.OfType<AuthMainWindow>().FirstOrDefault();
        ApplicationDbContext db;
        public Register()
        {
            InitializeComponent();

            db = new ApplicationDbContext();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxEmail.Text.Length == 0)
            {
                ErrorMessage.Text = "Enter an Email.";
                TextBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(TextBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                ErrorMessage.Text = "Enter a valid Email.";
                TextBoxEmail.Select(0, TextBoxEmail.Text.Length);
                TextBoxEmail.Focus();
            }
            else
            {
                string email = TextBoxEmail.Text;
                if(db.Users.Where(x => x.Email == email).FirstOrDefault() != null)
                {
                    ErrorMessage.Text = "Email adress is already registered.";
                    TextBoxEmail.Focus();
                }
                else
                {
                    string name = TextBoxName.Text;
                    string password = PasswordBox.Password;
                    if (PasswordBox.Password.Length == 0)
                    {
                        ErrorMessage.Text = "Enter password.";
                        PasswordBox.Focus();
                    }
                    else if (PasswordConfirmBox.Password.Length == 0)
                    {
                        ErrorMessage.Text = "Enter Confirm password.";
                        PasswordConfirmBox.Focus();
                    }
                    else if (PasswordBox.Password != PasswordConfirmBox.Password)
                    {
                        ErrorMessage.Text = "Confirm password must be same as password.";
                        PasswordConfirmBox.Focus();
                    }
                    else
                    {
                        User user = new User()
                        {
                            Name = name,
                            Email = email,
                            Password = password
                        };

                        db.Users.Add(user);
                        db.SaveChanges();

                        MW.AuthMainFrame.NavigationService.Navigate(new Login());
                    }
                }
            }
        }
    }
}
