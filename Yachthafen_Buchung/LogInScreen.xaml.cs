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
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
