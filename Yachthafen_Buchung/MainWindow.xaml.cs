using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Yachthafen_Buchung
{
    public partial class MainWindow : Window
    {
        public string ConnectionString { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Yachthafen;Integrated Security=True;Trust Server Certificate=True";
            PopulateStackPanels();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void PopulateStackPanels()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT DockID, Dockname, Status, Betreiber, Verantwortlicher FROM Dock";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dockID = reader["DockID"].ToString();
                            string dockName = reader["Dockname"].ToString();
                            string status = reader["Status"].ToString();
                            string betreiber = reader["Betreiber"].ToString();
                            string veratnwortlicher = reader["Verantwortlicher"].ToString();

                            StackPanel dockStackPanel = new StackPanel
                            {
                                Orientation = Orientation.Horizontal,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                Tag = $"{dockName}"
                            };

                            StackPanel buttonStackPanelLinks = new StackPanel { Name = "buttonStackPanelLinks" };
                            StackPanel buttonStackPanelRechts = new StackPanel { Name = "buttonStackPanelRechts" };

                            dockStackPanel.Children.Add(buttonStackPanelLinks);

                            Rectangle rectangle = new Rectangle
                            {
                                Width = 35,
                                Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(40, 37, 46)),
                                Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(127, 100, 188)),
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Height = 200,
                                Margin = new Thickness(5, 0, 5, 0),
                                ToolTip = new ToolTip
                                {
                                    Content = $"Dockinfos für {dockName}: \n - Status: {status} \n - Betreiber: {betreiber} \n - Verantwortlicher: {veratnwortlicher}",
                                    Style = (Style)FindResource("CustomToolTipStyle")
                                },
                            };
                            rectangle.MouseEnter += (sender, e) =>
                            {
                                rectangle.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(168, 130, 255));
                                rectangle.StrokeThickness = 2;

                            };

                            rectangle.MouseLeave += (sender, e) =>
                            {
                                rectangle.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(127, 100, 188));
                                rectangle.StrokeThickness = 1;
                            };
                            dockStackPanel.Children.Add(rectangle);

                            dockStackPanel.Children.Add(buttonStackPanelRechts);

                            dockWrapPanel.Children.Add(dockStackPanel);

                            PopulateButtons(buttonStackPanelLinks, buttonStackPanelRechts, dockID);
                        }
                    }
                }
            }
        }

        private void PopulateButtons(StackPanel buttonStackPanelLinks, StackPanel buttonStackPanelRechts, string dockID)
        {
            buttonStackPanelLinks.Children.Clear();
            buttonStackPanelRechts.Children.Clear();
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Yachthafen;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT DockID, LiegeplatzID, Liegeplatzstatus, Liegeplatzgröße, Liegeplatztyp, Preis, MaximaleBootslänge, MaximaleBootsbreite FROM Liegeplatz WHERE DockID = @DockID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@DockID", dockID);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        int liegeplatzID = Convert.ToInt32(row["LiegeplatzID"]);
                        int liegeplatzStatus = Convert.ToInt32(row["Liegeplatzstatus"]);
                        string liegeplatzgröße = Convert.ToString(row["Liegeplatzgröße"]);
                        string liegeplatztyp = Convert.ToString(row["Liegeplatztyp"]);
                        int preis = Convert.ToInt32(row["Preis"]);
                        decimal maximaleBootslänge = Convert.ToDecimal(row["MaximaleBootslänge"]);
                        decimal maximaleBootsbreite = Convert.ToDecimal(row["MaximaleBootsbreite"]);

                        Button button = new Button
                        {
                            Width = 110,
                            Height = 30,
                            FontSize = 16,
                            Content = liegeplatzStatus == 1 ? $"{dockID}{liegeplatzID} Buchen" : $"{dockID}{liegeplatzID} Stornieren",
                            HorizontalAlignment = HorizontalAlignment.Center,
                            BorderBrush = liegeplatzStatus == 1 ? new SolidColorBrush(System.Windows.Media.Color.FromRgb(57, 154, 86)) : new SolidColorBrush(System.Windows.Media.Color.FromRgb(250, 70, 76)),
                            Background = liegeplatzStatus == 1 ? new SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 43, 36)) : new SolidColorBrush(System.Windows.Media.Color.FromRgb(46, 33, 33)),
                            Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(218, 218, 218)),
                            ToolTip = new ToolTip
                            {
                                Content = $"Liegeplatzinfos für {dockID}{liegeplatzID}: \n - Liegeplatzgöße {liegeplatzgröße}m \n - Liegeplatztyp {liegeplatztyp} \n - Preis {preis}€\n - Maximale Bootslänge {maximaleBootslänge}m \n - Maximale Bootsbreite {maximaleBootsbreite}m",
                                Style = (Style)FindResource("CustomToolTipStyle")
                            },
                            Style = (Style)FindResource("CustomButtonStyle")
                        };

                        button.MouseEnter += (sender, e) =>
                        {
                            button.BorderBrush = liegeplatzStatus == 1 ? new SolidColorBrush(System.Windows.Media.Color.FromRgb(68, 207, 110)) : new SolidColorBrush(System.Windows.Media.Color.FromRgb(234, 67, 72));
                            button.BorderThickness = new Thickness(2);
                        };

                        button.MouseLeave += (sender, e) =>
                        {
                            button.BorderBrush = liegeplatzStatus == 1 ? new SolidColorBrush(System.Windows.Media.Color.FromRgb(57, 154, 86)) : new SolidColorBrush(System.Windows.Media.Color.FromRgb(250, 70, 76));
                            button.BorderThickness = new Thickness(1);
                        };

                        button.Click += (sender, e) =>
                        {
                            if (liegeplatzStatus == 1)
                            {
                                PopUp buchenWindow = new PopUp();
                                buchenWindow.Owner = this;

                                buchenWindow.PopUpTitel.Content = "Möchten Sie diesen Platz buchen?";

                                bool? dialogResult = buchenWindow.ShowDialog();

                                if (dialogResult == true)
                                {
                                    string updateQuery = "UPDATE Liegeplatz SET Liegeplatzstatus = 0 WHERE DockID = @DockID AND LiegeplatzID = @LiegeplatzID";
                                    using (SqlConnection updateConnection = new SqlConnection(connectionString))
                                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, updateConnection))
                                    {
                                        updateCommand.Parameters.AddWithValue("@DockID", dockID);
                                        updateCommand.Parameters.AddWithValue("@LiegeplatzID", liegeplatzID);
                                        updateConnection.Open();
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    PopulateButtons(buttonStackPanelLinks, buttonStackPanelRechts, dockID);
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                PopUp stornierenWindow = new PopUp();
                                stornierenWindow.Owner = this;

                                stornierenWindow.PopUpTitel.Content = "Möchten Sie diesen Platz stornieren?";

                                bool? dialogResult = stornierenWindow.ShowDialog();

                                if (dialogResult == true)
                                {
                                    string updateQuery = "UPDATE Liegeplatz SET Liegeplatzstatus = 1 WHERE DockID = @DockID AND LiegeplatzID = @LiegeplatzID";
                                    using (SqlConnection updateConnection = new SqlConnection(connectionString))
                                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, updateConnection))
                                    {
                                        updateCommand.Parameters.AddWithValue("@DockID", dockID);
                                        updateCommand.Parameters.AddWithValue("@LiegeplatzID", liegeplatzID);
                                        updateConnection.Open();
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    PopulateButtons(buttonStackPanelLinks, buttonStackPanelRechts, dockID);
                                }
                                else
                                {

                                }
                            }
                        };
                       

                        StackPanel buttonPanel = new StackPanel
                        {
                            Height = double.NaN,
                            Width = double.NaN,
                            Margin = new Thickness(5),
                        };

                        buttonPanel.Children.Add(button);

                        if (liegeplatzID <= 5)
                        {
                            buttonStackPanelLinks.Children.Add(buttonPanel);
                        }
                        else
                        {
                            buttonStackPanelRechts.Children.Add(buttonPanel);
                        }
                    }
                }
            }
        }
    }
}
