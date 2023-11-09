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
        public string ConnectionString { get; set; }
        public LogInScreen()
        {
            InitializeComponent();
            ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Yachthafen;Integrated Security=True;Trust Server Certificate=True";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredUsername = UsernameTextBox.Text;
            string enteredPassword = PasswordBox.Password;


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Benutzername, Passwort, IstAdmin FROM Benutzer WHERE Benutzername = @Username AND Passwort = @Password";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", enteredUsername);
                    cmd.Parameters.AddWithValue("@Password", enteredPassword);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string currentUser = Convert.ToString(reader["Benutzername"]);
                                MainWindow newMain = new MainWindow(currentUser, ConnectionString);
                                newMain.Show();
                                this.Close();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                LogInButton.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(250, 70, 76));
                                LogInButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(46, 33, 33));
                                await Task.Delay(100);

                                LogInButton.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(57, 154, 86));
                                LogInButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 43, 36));
                                await Task.Delay(100);
                            }
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
