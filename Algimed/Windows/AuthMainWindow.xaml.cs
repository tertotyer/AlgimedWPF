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
    public partial class AuthMainWindow : Window
    {
        public AuthMainWindow()
        {
            InitializeComponent();

            AuthMainFrame.Content = new Login();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            AuthMainFrame.NavigationService.Navigate(new Register());
        }
        
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            AuthMainFrame.NavigationService.Navigate(new Login());
        }
    }
}
