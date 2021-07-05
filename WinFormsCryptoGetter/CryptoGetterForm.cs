using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsCryptoGetter
{
    public partial class CryptoGetterForm : Form
    {
        CryptoGetter.DataBaseConnector dbc;
        private string GTIN;
        private string Serial;
        private string SGTIN;
        private Dictionary<string, string> Cities;

        public CryptoGetterForm()
        {
            InitializeComponent();

            Cities = new Dictionary<string, string>();
            Cities.Add("Уссурийск", "uss-m1-sql");
            Cities.Add("Иркутск", "irk-m1-sql");
            Cities.Add("Тюмень", "tmn-m1-sql");
            Cities.Add("Санкт-Петербург", "spb-m1-sql");
            FillCityComboBox();

            dbc = new CryptoGetter.DataBaseConnector();
        }

        private void FillCityComboBox()
        {
            foreach (KeyValuePair<string, string> c in Cities) { cbCity.Items.Add(c.Key); };
            cbCity.SelectedIndex = 0;
        }

        private void GET_button_Click(object sender, EventArgs e)
        {
            string server, cyptokey, cryptocode;
            Cities.TryGetValue(cbCity.SelectedItem.ToString(), out server);
            GTIN = tbGTIN.Text;
            Serial = tbSerial.Text;
            
            dbc.GetCrypto(server, GTIN, Serial,out cyptokey,out cryptocode);
            tbBarCode.Text = GTIN + Serial +cyptokey + cryptocode;
           
        }

        private void tbGTIN_TextChanged(object sender, EventArgs e)
        {
            if (tbGTIN.Text.Length == 14) UpdtateSGTIN();
        }

        private void UpdtateSGTIN()
        {
            SGTIN = GTIN.ToString() + Serial;
            tbSGTIN.Text = SGTIN;
        }


    }
}
