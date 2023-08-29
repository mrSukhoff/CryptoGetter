﻿using System;
using System.Drawing;
using System.Windows.Forms;
using DataMatrix.net;
using CryptoGetter;
using System.Linq;
using CryptoGetterForNet45.CryptoGetter;

namespace CryptoGetterForNet45
{
    public partial class CryptoGetterForm : Form
    {
        private readonly ServerList _serverList = new ServerList();
        private DataMinerFactory _dataMinerFactory = new DataMinerFactory();

        public CryptoGetterForm()
        {
            InitializeComponent();
            foreach (Server server in _serverList.ListOfServers)
            {
                ServerListComboBox.Items.Add(server.Name);
                GroupServerListComboBox.Items.Add(server.Name);
            }
            ServerListComboBox.SelectedIndex = 0;
            GroupServerListComboBox.SelectedIndex = 0;
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
            if (SGTINTextBox.Text.Length != 27) return;

            Server selectedServer = _serverList.ListOfServers.First(s => s.Name == ServerListComboBox.SelectedItem.ToString());
            try
            {
                IDataMiner dataMiner = _dataMinerFactory.GetDataMiner(selectedServer);
                (string cryptoKey, string cryptoCode) = dataMiner.GetCrypto(SGTINTextBox.Text);
                ShowResults(GTINTextBox.Text, SerialTextBox.Text, cryptoCode, cryptoKey);
            }
            catch (Exception ex)
            {
                DesignerTextBox.Text = ex.Message;
            }
        }
    
        //заполняет поля формы по имеющимся данным
        private void ShowResults(string GTIN, string Serial, string CryptoCode, string CryptoKey)
        {
            DesignerTextBox.Text = $"01{GTIN}21{Serial}<<GS1Separator>>91{CryptoKey}<<GS1Separator>>92{ CryptoCode}";
            KeyTextBox.Text = CryptoKey;
            CodeTextBox.Text = CryptoCode;
            DtmxCreator($"01{GTIN}21{Serial}{char.ConvertFromUtf32(29)}91{CryptoKey}{char.ConvertFromUtf32(29)}92{CryptoCode}");
            WTSTextBox.Text = $"01{GTIN}21{Serial}§91{CryptoKey}§92{CryptoCode}";
            SUZTextBox.Text = $"01{GTIN}21{Serial}{char.ConvertFromUtf32(29)}91{CryptoKey}{char.ConvertFromUtf32(29)}92{CryptoCode}";
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
            DesignerTextBox.Clear();
            KeyTextBox.Clear();
            CodeTextBox.Clear();
            if (DtmxPictureBox.Image != null) DtmxPictureBox.Image.Dispose();
            DtmxPictureBox.Image = null;
            WTSTextBox.Clear();
            SUZTextBox.Clear();
        }

        /// <summary>
        /// По входной строке метод рисует DatamatrixCode
        /// </summary>
        /// <param name="dataMatrixString">строка, которая будет закодирована в DMC </param>
        private void DtmxCreator(string dataMatrixString)
        {
            DmtxImageEncoder encoder = new DmtxImageEncoder();
            DmtxImageEncoderOptions options = new DmtxImageEncoderOptions();

            options.ModuleSize = 5;
            options.MarginSize = 4;
            Bitmap encodedBitmap = encoder.EncodeImage(dataMatrixString,options);
            DtmxPictureBox.Image = encodedBitmap;
        }

        //сохраняет картинку в файл
        private void SaveImeageButton_Click(object sender, EventArgs e)
        {
            if (DtmxPictureBox.Image == null) return;
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "bmp";
            saveFileDialog.FileName = SGTINTextBox.Text;
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            
            string path = saveFileDialog.FileName;
            DtmxPictureBox.Image.Save(path);
        }

        //копирует содержимое поля WTSTextBox в буфер обмена
        private void WtsCopyButton_Click(object sender, EventArgs e)
        {
            if (WTSTextBox.Text.Length > 0) Clipboard.SetText(WTSTextBox.Text);
        }

        private void SUZCopyButton_Click(object sender, EventArgs e)
        {
            if (SUZTextBox.Text.Length > 0) Clipboard.SetText(SUZTextBox.Text);
        }

        private void GtinCopyButton_Click(object sender, EventArgs e)
        {
            if (GTINTextBox.Text.Length > 0) Clipboard.SetText(GTINTextBox.Text);
        }

        private void SerialCopyButton_Click(object sender, EventArgs e)
        {
            if(SerialTextBox.Text.Length > 0) Clipboard.SetText(SerialTextBox.Text);
        }

        private void OpenSgtinButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = "txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SginFileLabel.Text = dialog.FileName;
            }
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog(); 
            dialog.ShowNewFolderButton = true;
            dialog.SelectedPath = Application.StartupPath;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                OutFolderPathLabel.Text = dialog.SelectedPath;
            }
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {

            Server selectedServer = _serverList.ListOfServers.First(s => s.Name == ServerListComboBox.SelectedItem.ToString());
            
            try
            {
                IDataMiner dataMiner = _dataMinerFactory.GetDataMiner(selectedServer);
                var mm = new MultiMiner(dataMiner);
               // mm.GenerateXmlFiles(xmlPath, sgtinPath, outerPath, lot, expiredto);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            
        
        
        }
    }

}
