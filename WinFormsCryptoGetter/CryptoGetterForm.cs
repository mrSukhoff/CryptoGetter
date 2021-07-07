using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace WinFormsCryptoGetter
{
    public partial class CryptoGetterForm : Form
    {
        //создаем объект бизнеслогики
        CryptoGetter.DataBaseConnector dbc = new CryptoGetter.DataBaseConnector();

        //Выбранный сервер
        private string SelectedServer;
        
        //текщий GTIN
        private string GTIN;

        //текущий серийный номер
        private string Serial;
        private string SGTIN; 
        //Список серверов
        private List<City> Cities;

        public CryptoGetterForm()
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
            cbCity.DataSource = Cities;
            //И сообщаем что показывать, а что выдавать
            cbCity.DisplayMember = "Name";
            cbCity.ValueMember = "Server";
            //подписываем обработчик изменений
            cbCity.SelectedIndexChanged += cbCity_SelectedIndexChanged;
            cbCity.SelectedItem = 0;

            //инициализируем переменные            
            tbGTIN_TextChanged(null, null);
            tbSerial_TextChanged(null, null);
            cbCity_SelectedIndexChanged(null,null);
        }

        private void cbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedServer = cbCity.SelectedValue.ToString();
        }

        private void tbGTIN_TextChanged(object sender, EventArgs e)
        {
            if (tbGTIN.Text.Length > 14) tbGTIN.Text = tbGTIN.Text.Substring(0, 13);
            if (tbGTIN.Text.Length == 14)
            {
                GTIN = tbGTIN.Text;
                SGTIN = GTIN + Serial;
                tbSGTIN.Text = SGTIN;
            }
        }
        
        private void tbSerial_TextChanged(object sender, EventArgs e)
        {
            if(tbSerial.Text.Length > 13) tbSerial.Text = tbSerial.Text.Substring(0, 13);
            if (tbSerial.Text.Length == 13)
            {
                Serial = tbSerial.Text;
                SGTIN = GTIN + Serial;
                tbSGTIN.Text = SGTIN;
            }
        }

        private void tbSGTIN_TextChanged(object sender, EventArgs e)
        {
            if (tbSGTIN.Text.Length == 27)
            {
                SGTIN = tbSGTIN.Text;
                GTIN = SGTIN.Substring(0, 14);
                Serial = SGTIN.Substring(14, 13);
                tbSerial.Text = Serial;
                tbGTIN.Text = GTIN;
            }
        }

        /// <summary>
        /// Основной метод работы с моделью
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GET_button_Click(object sender, EventArgs e)
        {
            string cyptokey, cryptocode;
            if (Serial.Length != 13) tbBarCode.Text += "Неверная длина серийного номера!";
            if (GTIN.Length != 14) tbBarCode.Text += "Неверная длина GTIN!";
            if (Serial.Length == 13 & GTIN.Length == 14)
            {
                if (dbc.GetCrypto(SelectedServer, GTIN, Serial, out cyptokey, out cryptocode))
                {
                    tbBarCode.Text = "01" + GTIN + "21" + Serial + "\\F91" + cyptokey + "\\F92" + cryptocode;
                    tbLayoutDesigner.Text = "01" + GTIN + "21" + Serial + "<<GS1Separator>>91" + cyptokey + "<<GS1Separator>>92" + cryptocode;
                }
                else
                {
                    tbBarCode.Text = cryptocode;
                    tbLayoutDesigner.Text = "";
                }
            }
            
        }

        private void CopyToBarCodeButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText (tbBarCode.Text);
        }

        private void CopyToLayoutDesignerButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbLayoutDesigner.Text);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            tbSGTIN.Clear();
            tbGTIN.Clear();
            tbSerial.Clear();
            tbLayoutDesigner.Clear();
            tbBarCode.Clear();
            SGTIN = "";
            GTIN = "";
            Serial = "";
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
