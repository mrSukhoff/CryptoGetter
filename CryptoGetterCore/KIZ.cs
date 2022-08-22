using System;
using DataMatrix.net;
using System.Drawing;

namespace CryptoGetterCore
{
    public class KIZ
    {
        private readonly ServerList _serverList;
        private readonly string _gtin;
        private readonly string _serial;
        private string _cryptoKey;
        private string _cryptoCode;

        public string GTIN { get => _gtin; }
        public string Serial { get => _serial; }
        public string CryptoKey { get => _cryptoKey; }
        public string CryptoCode { get => _cryptoCode; }

        internal KIZ(string gtin, string serial, ServerList serverList)
        {
            if (gtin.Length != 14) { throw new ArgumentException("Неверная длина GTIN!"); }
            if (serial.Length != 13) { throw new ArgumentException("Неверная длина серийного номера!"); }
            _gtin = gtin;
            _serial = serial;
            _serverList = serverList;
        }

        public bool GetCrypto()
        {
            DataMiner dm = new DataMiner();

            string db = _serverList.SelectedServerDBName;
            dm.Connect(_serverList.SelectedServerFQN, db);

            // Получаем по GTIN его идентификатор.
            var cmdString = $"SELECT [Id] FROM [{db}].[dbo].[NtinDefinition] WHERE Ntin = '{_gtin}'";
            string gTINid = dm.SelectValuesFromDb(cmdString)[0];

            // Проверяем найден ли GTIN
            if (gTINid.Length != 4)
            {
                throw new Exception("GTIN не найден!");
            }

            cmdString = $"SELECT [VariableValue] FROM [{db}].[dbo].[ItemDetails] " +
                $"where Serial='{_serial}' and NtinId={gTINid} and VariableName='cryptocode'";
            string cryptoCode = dm.SelectValuesFromDb(cmdString)[0]; ;

            cmdString = $"SELECT [VariableValue] FROM [{db}].[dbo].[ItemDetails] " +
                $"where Serial='{_serial}'and NtinId={gTINid} and VariableName='cryptokey'";
            string cryptoKey = dm.SelectValuesFromDb(cmdString)[0]; ;

            if (string.IsNullOrEmpty(cryptoKey) & string.IsNullOrEmpty(cryptoCode))
            {
                throw new Exception("Криптоданные не найдены");
            }
            if (cryptoKey.Length != 4)
            {
                throw new ArgumentException("Неверная длина криптоключа!");
            }
            if (cryptoCode.Length != 44)
            {
                throw new ArgumentException("Неверная длина криптокода!");
            }

            _cryptoCode = cryptoCode;
            _cryptoKey = cryptoKey;

            return true;
        }

        public Bitmap GetDataMatrix()
        {
            DmtxImageEncoder encoder = new DmtxImageEncoder();
            DmtxImageEncoderOptions options = new DmtxImageEncoderOptions
            {
                ModuleSize = 5,
                MarginSize = 4
            };
            var separator = char.ConvertFromUtf32(29);
            var dataMatrixString = $"01{_gtin}21{_serial}{separator}91{_cryptoKey}{separator}92{_cryptoCode}";
            return encoder.EncodeImage(dataMatrixString, options);
        }
    }
}


