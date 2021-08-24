using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DataMatrix.net;
using CryptoGetter;

namespace CryptoGetterForNet45
{
    public partial class CryptoGetterForm : Form
    {
        //создаем объект бизнеслогики
        CryptoGetter.DataBaseCryptoExtractor dbc = new CryptoGetter.DataBaseCryptoExtractor();

        //Список серверов
        private readonly List<City> Cities;

        public CryptoGetterForm()
        {
            InitializeComponent();

            //Создаем список объектов городов
            Cities = new List<City>
            {
                new City {Name = "Иркутск", Server = "irk-m1-sql" },
                new City {Name = "Тюмень", Server = "tmn-m1-sql" },
                new City {Name = "Уссурийск", Server = "uss-m1-sql" },
                new City {Name = "Санкт-Петербург", Server = "spb-m1-sql" }
            };

            //Привязываем его к combobox
            CityComboBox.DataSource = Cities;
            //И сообщаем что показывать, а что выдавать
            CityComboBox.DisplayMember = "Name";
            CityComboBox.ValueMember = "Server";
            //Устанавливаем выбранный элемент
            CityComboBox.SelectedItem = 0;
        }

        // Метод при изменении SGTIN меняет поля GTIN и серийного номера
        private void SGTINTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SGTINTextBox.Text.Length == 27)
            {
                //странная переменная, но без неё не работает
                string text = SGTINTextBox.Text;
                GTINTextBox.Text = text.Substring(0, 14);
                SerialTextBox.Text = text.Substring(14, 13);
            }
        }

        // Метод при изменении поля GTIN меняет поле SGTIN
        private void GTINTextBox_TextChanged(object sender, EventArgs e)
        {
            if (GTINTextBox.Text.Length > 14) GTINTextBox.Text = GTINTextBox.Text.Substring(0, 13);
            if (GTINTextBox.Text.Length == 14)
            {
                SGTINTextBox.Text = GTINTextBox.Text + SerialTextBox.Text;
            }
        }

        // Метод при изменении поля мерийного номера меняет SGTIN
        private void SerialTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SerialTextBox.Text.Length > 13) SerialTextBox.Text = SerialTextBox.Text.Substring(0, 13);
            if (SerialTextBox.Text.Length == 13)
            {
                SGTINTextBox.Text = GTINTextBox.Text + SerialTextBox.Text;
            }
        }

        /// Основной метод работы с моделью
        private void GetDataButton_Click(object sender, EventArgs e)
        {
            ClearResultFields();
            try
            {
                Package package = new Package()
                {
                    GTIN = GTINTextBox.Text,
                    Serial = SerialTextBox.Text
                };
                dbc.Connect(CityComboBox.SelectedValue.ToString());
                Package result = dbc.GetCrypto(package);
                dbc.Disconnect();
                ShowResults(result);
            }
            catch (Exception ex)
            {
                BarCodeTextBox.Text = ex.Message;
            }
        }
    
        private void ShowResults(Package package)
        {
            BarCodeTextBox.Text = "01" + package.GTIN + "21" + package.Serial + "\\F91" + package.CryptoKey + "\\F92" + package.CryptoCode;
            DesignerTextBox.Text = "01" + package.GTIN + "21" + package.Serial + "<<GS1Separator>>91" + package.CryptoKey + "<<GS1Separator>>92" + package.CryptoCode;
            KeyTextBox.Text = package.CryptoKey;
            CodeTextBox.Text = package.CryptoCode;
            DMXcreator("01" + package.GTIN + "21" + package.Serial + char.ConvertFromUtf32(29) + "91" + package.CryptoKey + char.ConvertFromUtf32(29) + "92" + package.CryptoCode);
        }
            
        //Метод копирует содержимое поля BarCodeCopyButton в буфер обмена
        private void BarCodeCopyButton_Click(object sender, EventArgs e)
        {
            if (BarCodeTextBox.Text.Length > 0) Clipboard.SetText(BarCodeTextBox.Text);
        }

        //Метод копирует содержимое поля DesignerCopyButton в буфер обмена
        private void DesignerCopyButton_Click(object sender, EventArgs e)
        {
            if (DesignerTextBox.Text.Length > 0) Clipboard.SetText(DesignerTextBox.Text);
        }

        // Метод отчищает все поля формы
        private void ClearButton_Click(object sender, EventArgs e)
        {
            SGTINTextBox.Clear();
            GTINTextBox.Clear();
            SerialTextBox.Clear();
            ClearResultFields();
        }

        //Метод очищает поля результатов
        private void ClearResultFields()
        {
            BarCodeTextBox.Clear();
            DesignerTextBox.Clear();
            KeyTextBox.Clear();
            CodeTextBox.Clear();
            if (DMXPictureBox.Image != null) DMXPictureBox.Image.Dispose();
            DMXPictureBox.Image = null;
        }

        /// <summary>
        /// По входной строке метод рисует DatamatrixCode
        /// </summary>
        /// <param name="dataMatrixString">строка, которая будет закодирована в DMC </param>
        private void DMXcreator(string dataMatrixString)
        {
            DmtxImageEncoder encoder = new DmtxImageEncoder();
            DmtxImageEncoderOptions options = new DmtxImageEncoderOptions();

            options.ModuleSize = 5;
            options.MarginSize = 4;
            Bitmap encodedBitmap = encoder.EncodeImage(dataMatrixString,options);
            DMXPictureBox.Image = encodedBitmap;
        }

        //Метод сохраняет картинку в файл
        private void SaveImeageButton_Click(object sender, EventArgs e)
        {
            if (DMXPictureBox.Image == null) return;
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "bmp";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            
            string path = saveFileDialog.FileName;
            DMXPictureBox.Image.Save(path);
        }

        //копирует содержимое поля OneSTextBox в буфер обмена
        private void OneSCopyButton_Click(object sender, EventArgs e)
        {
            if (OneSTextBox.Text.Length > 0) Clipboard.SetText(OneSTextBox.Text);
        }

        //копирует содержимое поля WTSTextBox в буфер обмена
        private void WtsCopyButton_Click(object sender, EventArgs e)
        {
            if (WTSTextBox.Text.Length > 0) Clipboard.SetText(WTSTextBox.Text);
        }
    }

    //Класс объектов для привязки списка городов
    class City
    {
        public string Name { get; set; }
        public string Server { get; set; }
    }
}
