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
        CryptoGetter.DataBaseConnector dbc = new CryptoGetter.DataBaseConnector();

        //Список серверов
        private List<City> Cities;

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

        private void SGTINTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SGTINTextBox.Text.Length == 27)
            {
                string text = SGTINTextBox.Text;
                GTINTextBox.Text = text.Substring(0, 14); 
                SerialTextBox.Text = text.Substring(14, 13);
            }
        }

        private void GTINTextBox_TextChanged(object sender, EventArgs e)
        {
            if (GTINTextBox.Text.Length > 14) GTINTextBox.Text = GTINTextBox.Text.Substring(0, 13);
            if (GTINTextBox.Text.Length == 14)
            {
                SGTINTextBox.Text = GTINTextBox.Text + SerialTextBox.Text;
            }
        }

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
            string cryptokey, cryptocode;
            string server = CityComboBox.SelectedValue.ToString();
            string GTIN = GTINTextBox.Text;
            string Serial = SerialTextBox.Text;

            BarCodeTextBox.Clear();
            DesignerTextBox.Clear();

            if (Serial.Length != 13) BarCodeTextBox.Text += "Неверная длина серийного номера!\r\n";
            if (GTIN.Length != 14) BarCodeTextBox.Text += "Неверная длина GTIN!\r\n";
            if (Serial.Length == 13 & GTIN.Length == 14)
            {
                if (dbc.GetCrypto(server, GTIN, Serial, out cryptokey, out cryptocode))
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
                    DesignerTextBox.Text = "";
                }
            }

        }

        private void BarCodeCopyButton_Click(object sender, EventArgs e)
        {
            if (BarCodeTextBox.Text.Length > 0) Clipboard.SetText(BarCodeTextBox.Text);
        }

        private void DesignerCopyButton_Click(object sender, EventArgs e)
        {
            if (DesignerTextBox.Text.Length > 0) Clipboard.SetText(DesignerTextBox.Text);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            SGTINTextBox.Clear();
            GTINTextBox.Clear();
            SerialTextBox.Clear();
            BarCodeTextBox.Clear();
            DesignerTextBox.Clear();
        }

        private void DMXcreator(string dataMatrixString)
        {
            DmtxImageEncoder encoder = new DmtxImageEncoder();
            DmtxImageEncoderOptions options = new DmtxImageEncoderOptions();
            options.ModuleSize = 5;
            options.MarginSize = 4;
            Bitmap encodedBitmap = encoder.EncodeImage(dataMatrixString,options);
            DMXPictureBox.Image = encodedBitmap;
        }

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
    ////Класс объектов для привязки списка городо
    /// </summary>
    class City
    {
        public string Name { get; set; }
        public string Server { get; set; }
    }
}
