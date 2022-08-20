using System;
using System.Drawing;
using System.Windows.Forms;
using DataMatrix.net;
using CryptoGetter;
using System.Linq;

namespace CryptoGetterForNet45
{
    public partial class CryptoGetterForm : Form
    {
        private readonly ServerList _serverList = new ServerList();
        private DataMinerFactory _dataMinerFactory = new DataMinerFactory();

        public CryptoGetterForm()
        {
            InitializeComponent();
            foreach (ServerList.Server server in _serverList.ListOfServers)
                ServerListComboBox.Items.Add(server.Name);
        }

        // При изменении SGTIN меняет поля GTIN и серийного номера
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

        // При изменении поля GTIN меняет поле SGTIN
        private void GTINTextBox_TextChanged(object sender, EventArgs e)
        {
            if (GTINTextBox.Text.Length > 14) GTINTextBox.Text = GTINTextBox.Text.Substring(0, 13);
            if (GTINTextBox.Text.Length == 14)
            {
                SGTINTextBox.Text = GTINTextBox.Text + SerialTextBox.Text;
            }
        }

        // При изменении поля cерийного номера меняет SGTIN
        private void SerialTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SerialTextBox.Text.Length > 13) SerialTextBox.Text = SerialTextBox.Text.Substring(0, 13);
            if (SerialTextBox.Text.Length == 13)
            {
                SGTINTextBox.Text = GTINTextBox.Text + SerialTextBox.Text;
            }
        }

        /// Получаем криптоданные
        private void GetDataButton_Click(object sender, EventArgs e)
        {
            ClearResultFields();

            ServerList.Server selectedServer = _serverList.ListOfServers.First(s => s.Name == ServerListComboBox.SelectedItem.ToString());
            try
            {
                IDataMiner DM = _dataMinerFactory.GetDataMiner(selectedServer.Type);
                (string cryptoCode, string cryptoKey) = DM.GetCrypto(SGTINTextBox.Text);
                ShowResults(GTINTextBox.Text, SerialTextBox.Text, cryptoCode, cryptoKey);
                DM.Close();
            }
            catch (Exception ex)
            {
                BarCodeTextBox.Text = ex.Message;
            }
        }
    
        //заполняет поля формы по имеющимся данным
        private void ShowResults(string GTIN, string Serial, string CryptoCode, string CryptoKey)
        {
            BarCodeTextBox.Text = $"01{GTIN}21{Serial}\\F91{CryptoKey}\\F92{CryptoCode}";
            DesignerTextBox.Text = $"01{GTIN}21{Serial}<<GS1Separator>>91{CryptoKey}<<GS1Separator>>92{ CryptoCode}";
            KeyTextBox.Text = CryptoKey;
            CodeTextBox.Text = CryptoCode;
            DMXcreator($"01{GTIN}21{Serial}{char.ConvertFromUtf32(29)}91{CryptoKey}{char.ConvertFromUtf32(29)}92{CryptoCode}");
            OneSTextBox.Text = $"{Serial} {CryptoKey} {CryptoCode}";
            WTSTextBox.Text = $"01{GTIN}21{Serial}§91{CryptoKey}§92{CryptoCode}";
        }
            
        //копирует содержимое поля BarCodeCopyButton в буфер обмена
        private void BarCodeCopyButton_Click(object sender, EventArgs e)
        {
            if (BarCodeTextBox.Text.Length > 0) Clipboard.SetText(BarCodeTextBox.Text);
        }

        //копирует содержимое поля DesignerCopyButton в буфер обмена
        private void DesignerCopyButton_Click(object sender, EventArgs e)
        {
            if (DesignerTextBox.Text.Length > 0) Clipboard.SetText(DesignerTextBox.Text);
        }

        //очищает все поля формы
        private void ClearButton_Click(object sender, EventArgs e)
        {
            SGTINTextBox.Clear();
            GTINTextBox.Clear();
            SerialTextBox.Clear();
            ClearResultFields();
        }

        //очищает поля результатов
        private void ClearResultFields()
        {
            BarCodeTextBox.Clear();
            DesignerTextBox.Clear();
            KeyTextBox.Clear();
            CodeTextBox.Clear();
            if (DMXPictureBox.Image != null) DMXPictureBox.Image.Dispose();
            DMXPictureBox.Image = null;
            OneSTextBox.Clear();
            WTSTextBox.Clear();
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

        //охраняет картинку в файл
        private void SaveImeageButton_Click(object sender, EventArgs e)
        {
            if (DMXPictureBox.Image == null) return;
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "bmp";
            saveFileDialog.FileName = SGTINTextBox.Text;
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

}
