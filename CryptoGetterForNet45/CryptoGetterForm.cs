using CryptoGetter;
using DataMatrix.net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CryptoGetterForNet45
{
    public partial class CryptoGetterForm : Form
    {
        private readonly ServerList _serverList = new ServerList();
        private readonly DataMinerFactory _dataMinerFactory = new DataMinerFactory();

        private List<string> _sgtins = new List<string>();
        private string _savePath;

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
            KeyTextBox.Text = CryptoKey;
            CodeTextBox.Text = CryptoCode;
            DesignerTextBox.Text = $"01{GTIN}21{Serial}<<GS1Separator>>91{CryptoKey}<<GS1Separator>>92{CryptoCode}";
            WTSTextBox.Text = $"01{GTIN}21{Serial}§91{CryptoKey}§92{CryptoCode}";
            SUZTextBox.Text = $"01{GTIN}21{Serial}{char.ConvertFromUtf32(29)}91{CryptoKey}{char.ConvertFromUtf32(29)}92{CryptoCode}";
            DtmxPictureBox.Image = DtmxCreator($"01{GTIN}21{Serial}{char.ConvertFromUtf32(29)}91{CryptoKey}{char.ConvertFromUtf32(29)}92{CryptoCode}");
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
            DtmxPictureBox.Image?.Dispose();
            DtmxPictureBox.Image = null;
            WTSTextBox.Clear();
            SUZTextBox.Clear();
        }

        // По входной строке метод возвращает картинку DatamatrixCode
        private Bitmap DtmxCreator(string dataMatrixString)
        {
            DmtxImageEncoder encoder = new DmtxImageEncoder();
            DmtxImageEncoderOptions options = new DmtxImageEncoderOptions
            {
                ModuleSize = 5,
                MarginSize = 4
            };
            Bitmap encodedBitmap = encoder.EncodeImage(dataMatrixString, options);
            DtmxPictureBox.Image = encodedBitmap;
            return encodedBitmap;
        }

        //сохраняет картинку в файл
        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            if (DtmxPictureBox.Image == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = "bmp",
                FileName = SGTINTextBox.Text
            };
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;

            string path = saveFileDialog.FileName;
            DtmxPictureBox.Image.Save(path);
        }

        //копирует содержимое поля WTSTextBox в буфер обмена
        private void WtsCopyButton_Click(object sender, EventArgs e)
        {
            if (WTSTextBox.Text.Length > 0) Clipboard.SetText(WTSTextBox.Text);
        }

        //копирует в буфер обмена код в формате СУЗ
        private void SUZCopyButton_Click(object sender, EventArgs e)
        {
            if (SUZTextBox.Text.Length > 0) Clipboard.SetText(SUZTextBox.Text);
        }

        //копирует в буфер обмена содержимое поля GTIN
        private void GtinCopyButton_Click(object sender, EventArgs e)
        {
            if (GTINTextBox.Text.Length > 0) Clipboard.SetText(GTINTextBox.Text);
        }

        //копирует в буфер обмена поле серийного номера
        private void SerialCopyButton_Click(object sender, EventArgs e)
        {
            if (SerialTextBox.Text.Length > 0) Clipboard.SetText(SerialTextBox.Text);
        }

        //Копирует в буфер обмена изображение
        private void CopyImageButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(DtmxPictureBox.Image);
        }

        //*****************************************************************************************************************************************************************************

        //открывает файл и загружает построчно SGTIN в список
        private void OpenSgtinButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                DefaultExt = "txt"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SginFileLabel.Text = dialog.FileName;
            }
            else return;


            _sgtins.Clear();
            using (StreamReader reader = new StreamReader(dialog.FileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length == 27)
                    {
                        _sgtins.Add((line));
                    }
                    else OutputTexBox.Text += $"Неверные даннные в строке {line} \r\n";
                }
                OutputTexBox.Text += $"Загружено {_sgtins.Count} строк \r\n";
            }
        }

        //выбор папки куда будут записаны файлы картинок
        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                SelectedPath = Application.StartupPath
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                OutFolderPathLabel.Text = dialog.SelectedPath;
                _savePath = dialog.SelectedPath;
            }
        }

        //метод генерации кодов. Для каждого кода из списка запрашиваются криптоданные, генерируется код и записывается в папку в формате bmp
        private void GenerateButton_Click(object sender, EventArgs e)
        {

            Server selectedServer = _serverList.ListOfServers.First(s => s.Name == GroupServerListComboBox.SelectedItem.ToString());
            IDataMiner dataMiner = _dataMinerFactory.GetDataMiner(selectedServer);

            string cryptoKey, cryptoCode;
            int counter = 0;
            int total = _sgtins.Count;
            string dataForMatrix;
            Bitmap anotherDtmx;
            List<string> codes = new List<string>();  
			try
            {
                foreach (string sgtin in _sgtins)
                {
                    (cryptoKey, cryptoCode) = dataMiner.GetCrypto(sgtin);
                    counter++;
                    dataForMatrix = $"01{sgtin.Substring(0, 14)}21{sgtin.Substring(14, 13)}{char.ConvertFromUtf32(29)}91{cryptoKey}{char.ConvertFromUtf32(29)}92{cryptoCode}";
					anotherDtmx = DtmxCreator(dataForMatrix);
                    anotherDtmx.Save(_savePath + "\\" + sgtin.ToString() + ".bmp");
                    codes.Add (dataForMatrix);
                    OutputTexBox.Text += $"Сохранено {counter} из {total} кодов \r\n";
                }
                SaveCodesToFile(codes);
            }
            catch (Exception exp)
            {
                OutputTexBox.Text += exp.Message + "\r\n";
            }
        }
        
        private void SaveCodesToFile(List<string> codes)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_savePath + "\\all_codes.txt", false))
                {
                    foreach (string code in codes) writer.WriteLine(code);
                }
			}
			catch (Exception exp)
			{
				OutputTexBox.Text += exp.Message + "\r\n";
			}
		}

		//очистка полей вкладки групповой обработки
		private void ClearGroupProcessingFields()
        {
            _sgtins.Clear();
            _savePath = "";
            GroupServerListComboBox.SelectedIndex = 0;
            SginFileLabel.Text = "Выберете файл с SGTIN";
            OutFolderPathLabel.Text = "Выберете папку";
            OutputTexBox.Clear();
        }

        //При изменении выбраной вкладки очищает её содержимое
        private void ModeTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (ModeTabControl.SelectedIndex == 0) ClearButton_Click(null, null);
            if (ModeTabControl.SelectedIndex == 1) ClearGroupProcessingFields();
        }
    }
}
