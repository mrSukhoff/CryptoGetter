﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DataMatrix.net;
using CryptoGetter;

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
            //GTINTextBox_TextChanged(null, null);
            //SerialTextBox_TextChanged(null, null);
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
            ClearResultFields();
            try
            {
                Package package = new Package()
                {
                    GTIN = GTINTextBox.Text,
                    Serial = SerialTextBox.Text
                };
                dbc.Connect(CityComboBox.SelectedValue.ToString());
                dbc.GetCrypto(package);
                ShowResults(package);
                dbc.Disconnect();
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
            ClearResultFields();
        }

        private void ClearResultFields()
        {
                BarCodeTextBox.Clear();
                DesignerTextBox.Clear();
                KeyTextBox.Clear();
                CodeTextBox.Clear();
                if (DMXPictureBox.Image != null) DMXPictureBox.Image.Dispose();
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
