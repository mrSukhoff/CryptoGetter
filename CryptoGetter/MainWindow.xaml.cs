using System.Windows;

namespace CryptoGetter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DataBaseConnector dbc;

        public MainWindow()
        {
            InitializeComponent();
            dbc = new DataBaseConnector();
        }

        private void GetButton_Click(object sender, RoutedEventArgs e)
        {
            //Проверяем размерность данных
            string gtin = GtinBox.Text;
            string serial = SerialBox.Text;
            ResultBox.Clear();
            if (gtin.Length != 14) ResultBox.Text += "Некорректная длина кода GTIN!";
            if (serial.Length != 13) ResultBox.Text += "Некорректная длина серийного номера!";
            if (gtin.Length == 14 & serial.Length == 13) ResultBox.Text =  dbc.GetCrypto(gtin, serial);
        }
    }
}
