using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                User user = await client.GetUserAsync(userIdTextBox.Text);
                if (user != null) users.Add(user);
                else
                {
                    users = (List<User>) await client.GetUsersAsync();
                    MessageBox.Show("UserId = " + userIdTextBox.Text + " not found!", this.Title, MessageBoxButton.OK, 
                        MessageBoxImage.Error);
                }
                usersDataGrid.ItemsSource = users;
            }
        }

        private async void getAllUsersButton_Click(object sender, RoutedEventArgs e)
        {
            usersDataGrid.ItemsSource = await GetAllUsersAsync();
        }

        private async void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = null;
            EditUserWindow editUserWindow;
            if (usersDataGrid.SelectedItem != null)
            {
                user = usersDataGrid.SelectedItem as User;
                editUserWindow = new EditUserWindow(user);
                if (editUserWindow.ShowDialog() ?? false)
                {
                    await client.EditUserAsync(user.Id, user);
                }
            }
            else
            {
                user = new User { Id = 0, MaxLevel = 0, MaxScore = 0 };
                editUserWindow = new EditUserWindow(user);
                if (editUserWindow.ShowDialog() ?? false)
                {
                    User newUser = await client.AddUserAsync(user);
                    //usersDataGrid.Items.Add(newUser);
                }
            }
        }

        private async void delUser_Click(object sender, RoutedEventArgs e)
        {
            if (usersDataGrid.SelectedItem != null)
            {
                await client.DeleteUserAsync((usersDataGrid.SelectedItem as User).Id.ToString());
                usersDataGrid.ItemsSource = await GetAllUsersAsync();
            }
        }

        private void usersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (usersDataGrid.SelectedItem != null)
            {
                userIdTextBox.Text = (usersDataGrid.SelectedItem as User).Id.ToString();
            }
        }

        private async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();
            users = (List<User>) await client.GetUsersAsync();
            return users;
        }
    }
}
