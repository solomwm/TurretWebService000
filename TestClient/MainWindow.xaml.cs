using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TurretWebService.Models;

namespace TestClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TurretWebApiClient client;

        public MainWindow()
        {
            InitializeComponent();
            client = new TurretWebApiClient("http://localhost:50463/");
        }

        private async void getUserButton_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = new List<User>();
            if (userIdTextBox.Text != String.Empty)
            {
                User user = await client.GetUserAsync(client.BaseAddress + "api/Users/" + userIdTextBox.Text);
                if (user != null) users.Add(user);
                else
                {
                    users = (List<User>)await client.GetUsersAsync(client.BaseAddress + "api/Users");
                    MessageBox.Show("UserId = " + userIdTextBox.Text + " not found!", this.Title, MessageBoxButton.OK, 
                        MessageBoxImage.Error);
                }
                usersDataGrid.ItemsSource = users;
            }
        }

        private async void getAllUsersButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<User> users = await client.GetUsersAsync(client.BaseAddress + "api/Users");
            usersDataGrid.ItemsSource = users;
        }
    }
}
