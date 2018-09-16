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
using System.Windows.Shapes;
using TurretWebService.Models;

namespace TestClient
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public EditUserWindow(User user)
        {
            InitializeComponent();
            DataContext = user;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression binding = NameTextBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
            binding = MaxLevelTextBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
            binding = MaxScoreTextBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
