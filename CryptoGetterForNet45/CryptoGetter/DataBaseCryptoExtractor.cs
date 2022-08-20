using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
            Disconnect();
            string connectionString = "Data Source=" + servername + ";Initial Catalog=AntaresTracking_PRD;Persist Security Info=True;User ID=tav;Password=tav";
            connection = new SqlConnection(connectionString);
            connection.Open();
            IsConnected = true;
        }

        /// <summary>
        /// Отключеие от БД
        /// </summary>
        public void Disconnect()
        {
            if (IsConnected)
            {
                if (connection != null) 
                {
                    connection.Close();
                    IsConnected = false;
                }
            }

        }

        /// <summary>
        /// Возвращает пакет со всеми заполнеными полями.
        /// </summary>
        /// <param name="package">В пакете должны быть заполнены GTIN и серийный номер</param>
        /// <returns></returns>
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
            return GetCryptoData(package, GTINid);
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
        ////Метод по идентификатору GTIN и серийному номеру находит криптоданные и возвращает пакет со всеми даннфми
        /// </summary>
        /// <param name="package">пакет с заполненым GTIN и серийным номером</param>
        /// <param name="gtinId">Идентификатор GTIN</param>
        /// <returns></returns>
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
                if (value.Length == 14)
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
                if (value.Length == 13)
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
