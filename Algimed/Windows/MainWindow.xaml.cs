using Algimed.Data;
using Algimed.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Algimed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationDbContext db;
        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationDbContext();
            result.DataContext = db.Results.ToList();
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files |*.csv;*.xlsx";
            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                var listResults = FileHelperService.ReadFile(openFileDialog.FileName);
                result.DataContext = listResults;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var authMain = new AuthMainWindow();
            authMain.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var authMain = new AuthMainWindow();
            authMain.AuthMainFrame.NavigationService.Navigate(new Register());
            authMain.Show();
            this.Close();
        }
    }
}
