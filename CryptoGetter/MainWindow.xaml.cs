using System.Windows;

namespace CryptoGetter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly DataBaseConnector dbc;

        public MainWindow()
        {
            InitializeComponent();
            dbc = new DataBaseConnector();
        }

        /// <summary>
        /// Метод по нажатию кнопки читает данные из формы, проверяет размерность и делает запрос в модели.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetButton_Click(object sender, RoutedEventArgs e)
        {
            //Проверяем размерность данных
            string gtin = GtinBox.Text;
            string serial = SerialBox.Text;
            ResultBox.Clear();
            //Если что - ругаемся
            if (gtin.Length != 14) ResultBox.Text += "Некорректная длина кода GTIN!";
            if (serial.Length != 13) ResultBox.Text += "Некорректная длина серийного номера!";
            //А если длина нормальная - отправляем запрос
            if (gtin.Length == 14 & serial.Length == 13) ResultBox.Text = dbc.GetCrypto(gtin, serial);
        }


        /// <summary>
        /// Метод копирует в буффер обмена данные из формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ResultBox.Text);
        }
    }
}
