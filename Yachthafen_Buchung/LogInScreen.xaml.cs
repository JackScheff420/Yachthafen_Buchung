using Microsoft.Data.SqlClient;
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


namespace Yachthafen_Buchung
{
    /// <summary>
    /// Interaktionslogik für LogInScreen.xaml
    /// </summary>
    public partial class LogInScreen : Window
    {
        public LogInScreen()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredUsername = UsernameTextBox.Text;
            string enteredPassword = PasswordBox.Password;

            MainWindow mainWindow = new MainWindow();
            string connectionString = mainWindow.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Benutzername, Passwort FROM Benutzer WHERE Benutzername = @Username AND Passwort = @Password";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", enteredUsername);
                    cmd.Parameters.AddWithValue("@Password", enteredPassword);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            MainWindow main = new MainWindow();
                            main.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ungültiger Benutzername oder Passwort.");
                        }
                    }
                }
            }
        }

        private void CustomButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;

            string buttonText = button.Content.ToString();

            button.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(68, 207, 110));
            button.BorderThickness = new Thickness(2);
           
        }

        private void CustomButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;

            string buttonText = button.Content.ToString();

            button.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(57, 154, 86));
            button.BorderThickness = new Thickness(1);

        }

        private void login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }
    }
}
