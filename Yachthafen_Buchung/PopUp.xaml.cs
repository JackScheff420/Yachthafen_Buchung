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
    /// Interaktionslogik für PopUp.xaml
    /// </summary>
    public partial class PopUp : Window
    {
        public PopUp()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void StornierenButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void AbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        private void CustomButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;

            string buttonText = button.Content.ToString();

            if (buttonText.ToLower() == "ja")
            {
                button.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(68, 207, 110));
                button.BorderThickness = new Thickness(2);
            }
            else if (buttonText.ToLower() == "nein")
            {
                button.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(234, 67, 72));
                button.BorderThickness = new Thickness(2);
            }
        }

        private void CustomButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;

            string buttonText = button.Content.ToString();

            if (buttonText.ToLower() == "ja")
            {
                button.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(57, 154, 86));
                button.BorderThickness = new Thickness(1);
            }
            else if (buttonText.ToLower() == "nein")
            {
                button.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(250, 70, 76));
                button.BorderThickness = new Thickness(1);
            }
        }
    }
}
