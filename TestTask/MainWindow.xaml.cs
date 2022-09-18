using System.Windows;
using System.Windows.Controls;
using TestTask.Handlers;
using TestTask.Export;
using Microsoft.Win32;
using TestTask.Interfaces;

namespace TestTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserData tableInfo;

        private DataReader users;

        private int fileNumber = 0;

        private string[] formats = new string[] { "csv", "xml", "json", };

        public MainWindow()
        {
            InitializeComponent();
            users = new DataReader();
            tableInfo = new UserData(users.UserSteps);
            NamesTable.ItemsSource = tableInfo.Resources;
            Graph.Plot.XLabel("Дни");
            Graph.Plot.YLabel("Шаги");
        }

        private void SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Graph.Plot.Clear();
            var currentPerson = tableInfo.Resources[NamesTable.SelectedIndex];
            PrintGraph(currentPerson.Steps);
            for (int i = 0; i < currentPerson.Steps.Length; i++)
            {
                if (currentPerson.Steps[i] == currentPerson.WorstResult)
                {
                    Graph.Plot.AddPoint(i + 1, currentPerson.Steps[i].Value, System.Drawing.Color.Red, 9);
                }
                else if (currentPerson.Steps[i] == currentPerson.BestResult)
                {
                    Graph.Plot.AddPoint(i + 1, currentPerson.Steps[i].Value, System.Drawing.Color.Green, 9);
                }
            }
            Graph.Refresh();
        }

        private void PrintGraph(double?[] dataY)
        {
            for (int i = 0; i < dataY.Length - 1; i++)
            {
                if (dataY[i].HasValue && dataY[i+1].HasValue)
                {
                    Graph.Plot.AddPoint(i + 1, dataY[i].Value, System.Drawing.Color.Blue);
                    Graph.Plot.AddPoint(i + 2, dataY[i + 1].Value, System.Drawing.Color.Blue);
                    Graph.Plot.AddLine(i + 1, dataY[i].Value, i + 2, dataY[i + 1].Value, System.Drawing.Color.Blue);
                }
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            if (NamesTable.SelectedIndex < 0 || NamesTable.SelectedIndex > tableInfo.Resources.Length)
            {
                MessageBox.Show("Для экспорта данных следует выбрать пользователя.");
                return;
            }
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.FileName = $"PersonInfo{++fileNumber}";
            dialog.Filter = "Csv file (*.csv)|*.csv|Xml file (*.xml)|*.xml|Json file (*.json)|*.json";
            IUserSave user = null;
            if (dialog.ShowDialog() == true)
            {
                switch (formats[dialog.FilterIndex - 1])
                {
                    case "csv":
                        user = new CsvUser();
                        break;
                    case "xml":
                        user = new XmlUser();
                        break;
                    case "json":
                        user = new JsonUser();
                        break;
                }
                if (user != null)
                {
                    user.SaveUser(dialog.FileName, tableInfo.Resources[NamesTable.SelectedIndex]);
                    MessageBox.Show("Файл успешно сохранен.");
                }
            }
            else
            {
                fileNumber--;
            }
        }
    }
}
