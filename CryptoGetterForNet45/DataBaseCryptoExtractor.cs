using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CryptoGetter
{
    class DataBaseCryptoExtractor
    {
        //Соединение с БД
        private SqlConnection connection;
        public bool IsConnected { get; private set; }

        /// <summary>
        ////Соединение с БД 
        /// </summary>
        /// <param name="servername"></param>
        public void Connect (string servername)
        {
            string connectionString = "Data Source=" + servername + ";Initial Catalog=AntaresTracking_PRD;Persist Security Info=True;User ID=tav;Password=tav";
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                IsConnected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Package GetCrypto (Package package)
        {
            //Получаем по GTIN его идентификатор.
            string GTINid = GetGtinId(package.GTIN);

            //Проверяем найден ли GTIN
            if (GTINid.Length != 4)
            { 
                throw new Exception("GTIN не найден!");
            }
            
            // По идентификатору GTIN и серийному номеру пачки получаем крипто-данные.
            Package result = GetCryptoData(package, GTINid);

            //Проверяем найдены ли криптоданные
            if (result.CryptoCode.Length != 44) 
            {
                throw new Exception( "Неверная длина криптокода!");
            }
                
            if (result.CryptoCode.Length !=4)
            {
                throw new Exception("Неверная длина криптоключа!");
            }
            
            return result;
        }
         

        /// <summary>
        /// Метод запрашивает в БД идентификатор GTINа
        /// </summary>
        /// <param name="GTIN">GTIN, для которого ищем идентификатор</param>
        /// <returns></returns>
        private string GetGtinId(string gtin)
        {
            //Результат по умолчанию
            string result = "";

            //Создаем запрос к БД
            string cmdString = String.Format("SELECT [Id] FROM [AntaresTracking_PRD].[dbo].[NtinDefinition] WHERE Ntin = '{0}'", gtin);
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            // И выполняем его
            SqlDataReader reader = cmd.ExecuteReader();

            //Читаем все результаты
            while (reader.Read())
            {
                //но запоминаем последний :)
                result = reader.GetValue(0).ToString();
            }

            //Всё закрываем
            reader.Close();
            cmd.Dispose();

            return result;
        }

        /// <summary>
        /// Метод пытается получить для SGTIN криптоключ и криптохвост из БД 
        /// </summary>
        /// <param name="gtinId">Идентификатор GTIN</param>
        /// <param name="serial">Серийный номер</param>
        /// <param name="cryptoCode">Криптокод</param>
        /// <param name="cryptoKey">Криптоключь</param>
        private Package GetCryptoData(Package package,string gtinId)
        {
            Package result = new Package() { GTIN = package.GTIN, Serial = package.Serial};
            Dictionary<string, string> results = new Dictionary<string, string>();
            
            //Формируем запрос
            string cmdString = String.Format("SELECT [VariableName] ,[VariableValue] FROM [AntaresTracking_PRD].[dbo].[ItemDetails] where Serial='{0}' and NtinId={1}", package.Serial, gtinId);
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            cmd.CommandTimeout = 300;
            //И выполняем его
            SqlDataReader reader = cmd.ExecuteReader();

            //Читаем по порядку все ответы
            while (reader.Read())
            {
                string key = reader.GetValue(0).ToString();
                string value = reader.GetValue(1).ToString();
                results.Add(key, value);
            }
            reader.Close();
            cmd.Dispose();

            if (results.Count >= 2)
            {
                result.CryptoCode = results["cryptocode"];
                result.CryptoKey = results["cryptokey"];
            }
            else
            {
                throw new Exception("Криптоданные не найдены");
            }
            return result;
        }
    }

    public struct Package
    {
        private string _gtin;
        private string _serial;
        private string _cryptoKey;
        private string _cryptoCode;
        public string GTIN 
        { 
            get 
            {
                return _gtin;
            }
            set 
            {
                if (value.Length == 13)
                {
                    _gtin = value;
                }
                else
                { 
                    throw new ArgumentException("Неверная длина GTIN!"); 
                }
            } 
        }
        public string Serial 
        {
            get
            {
                return _serial;
            }
            set
            {
                if (value.Length == 14)
                { 
                    _serial = value; 
                }
                else
                { 
                    throw new ArgumentException("Неверная длина серийного номера!"); 
                }
            }
        }
        public string CryptoKey
        {
            get
            {
                return _cryptoKey;
            }
            set
            {
                if (value.Length == 4)
                {
                    _cryptoKey = value; 
                }
                else
                {
                    throw new ArgumentException("Неверная длина криптоключа"); 
                }
            }
        }
        public string CryptoCode
        {
            get
            {
                return _cryptoCode;
            }
            set
            {
                if (value.Length == 44)
                {
                    _cryptoCode = value; }
                else
                { 
                    throw new ArgumentException("Неверная длина криптокода!"); 
                }
            }
        }
    }
}
