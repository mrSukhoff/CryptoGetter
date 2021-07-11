using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DataMatrix.net;

namespace CryptoGetterForNet45
{
    public partial class Form1 : Form
    {
        //создаем объект бизнеслогики
        CryptoGetter.DataBaseCryptoExtractor dbc = new CryptoGetter.DataBaseCryptoExtractor();

        //Список серверов
        private readonly List<City> Cities;

        public Form1()
        {
            InitializeComponent();

            //Создаем список объектов городов
            Cities = new List<City>
            {
                new City {Name = "Уссурийск", Server = "uss-m1-sql" },
                new City {Name = "Иркутск", Server = "irk-m1-sql" },
                new City {Name = "Тюмень", Server = "tmn-m1-sql" },
                new City {Name = "Санкт-Петербург", Server = "spb-m1-sql" }
            };

            //Привязываем его к combobox
            CityComboBox.DataSource = Cities;
            //И сообщаем что показывать, а что выдавать
            CityComboBox.DisplayMember = "Name";
            CityComboBox.ValueMember = "Server";
            //подписываем обработчик изменений
            CityComboBox.SelectedItem = 0;

            //инициализируем переменные            
            GTINTextBox_TextChanged(null, null);
            SerialTextBox_TextChanged(null, null);
        }
        /// <summary>
        /// Метод при изменении SGTIN меняет поля GTIN и серийного номера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Метод при изменении поля GTIN меняет поле SGTIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GTINTextBox_TextChanged(object sender, EventArgs e)
        {
            if (GTINTextBox.Text.Length > 14) GTINTextBox.Text = GTINTextBox.Text.Substring(0, 13);
            if (GTINTextBox.Text.Length == 14)
            {
                SGTINTextBox.Text = GTINTextBox.Text + SerialTextBox.Text;
            }
        }

        /// <summary>
        /// Метод при изменении поля мерийного номера меняет SGTIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SerialTextBox.Text.Length > 13) SerialTextBox.Text = SerialTextBox.Text.Substring(0, 13);
            if (SerialTextBox.Text.Length == 13)
            {
                SGTINTextBox.Text = GTINTextBox.Text + SerialTextBox.Text;
            }
        }

        /// <summary>
        /// Основной метод работы с моделью
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetDataButton_Click(object sender, EventArgs e)  
        {
            string server = CityComboBox.SelectedValue.ToString();
            string GTIN = GTINTextBox.Text;
            string Serial = SerialTextBox.Text;

            BarCodeTextBox.Clear();
            DesignerTextBox.Clear();
                    
            if (Serial.Length == 13 & GTIN.Length == 14)
            {
                if (dbc.GetCrypto(server, GTIN, Serial, out string cryptokey, out string cryptocode))
                {
                    BarCodeTextBox.Text = "01" + GTIN + "21" + Serial + "\\F91" + cryptokey + "\\F92" + cryptocode;
                    DesignerTextBox.Text = "01" + GTIN + "21" + Serial + "<<GS1Separator>>91" + cryptokey + "<<GS1Separator>>92" + cryptocode;
                    KeyTextBox.Text = cryptokey;
                    CodeTextBox.Text = cryptocode;
                    DMXcreator("01" + GTIN + "21" + Serial + char.ConvertFromUtf32(29)+"91" + cryptokey + char.ConvertFromUtf32(29) + "92" + cryptocode);
                }
                else
                {
                    BarCodeTextBox.Text = cryptocode;
                }
            } 
            else
            {
                if (GTIN.Length != 14) BarCodeTextBox.Text += "Неверная длина GTIN!\r\n";
                //if (!int.TryParse(GTIN, out int result)) BarCodeTextBox.Text += "GTIN содержит не числовые символы!\r\n";
                if (Serial.Length != 13) BarCodeTextBox.Text += "Неверная длина серийного номера!\r\n";
            }

        }

        /// <summary>
        ////Метод копирует содержимое поля BarCodeCopyButton в бувер обмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarCodeCopyButton_Click(object sender, EventArgs e)
        {
            if (BarCodeTextBox.Text.Length > 0) Clipboard.SetText(BarCodeTextBox.Text);
        }

        /// <summary>
        ////Метод копирует содержимое поля DesignerCopyButton в бувер обмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignerCopyButton_Click(object sender, EventArgs e)
        {
            if (DesignerTextBox.Text.Length > 0) Clipboard.SetText(DesignerTextBox.Text);
        }

        /// <summary>
        /// Метод отчищает все поля формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            SGTINTextBox.Clear();
            GTINTextBox.Clear();
            SerialTextBox.Clear();
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

        /// <summary>
        ////Метод сохраняет картинку в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveImeageButton_Click(object sender, EventArgs e)
        {
            if (DMXPictureBox.Image == null) return;
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "bmp";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            
            string path = saveFileDialog.FileName;
            DMXPictureBox.Image.Save(path);
        }
    }

    /// <summary>
    ////Класс объектов для привязки списка городов
    /// </summary>
    class City
    {
        public string Name { get; set; }
        public string Server { get; set; }
    }
}
