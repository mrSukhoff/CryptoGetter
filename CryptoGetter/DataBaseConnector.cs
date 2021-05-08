using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CryptoGetter
{
    class DataBaseConnector
    {
        //Строка подключения к "боевой" базе данных в Иркутске
        static readonly string connectionString = "Data Source=IRK-M1-SQL;Initial Catalog=AntaresTracking_PRD;Persist Security Info=True;User ID=tav;Password=tav";
        
        //Соединение с БД
        private SqlConnection connection;

        /// <summary>
        /// Конструктор класса. Инициализирует соединение с БД.
        /// </summary>
        
        public DataBaseConnector()
        {
            connection = new SqlConnection(connectionString);
        }
        
        /// <summary>
        /// Метод используется при уничтожении класса.
        /// </summary>
         ~DataBaseConnector()
        {
            if (connection.State == System.Data.ConnectionState.Open) connection.Close();
        }

        /// <summary>
        /// Метод возвращает строку содержащую все данные для генерации DataMatrix
        /// </summary>
        /// <param name="GTIN">GTIN</param>
        /// <param name="Serial">серийный номер пачки</param>
        /// <returns></returns>
        public string GetCrypto(string GTIN, string Serial)
        {
            //Получаем по GTIN его идентификатор.
            string GTINid = GetGtinId(GTIN);

            //Проверяем найден ли GTIN
            if (GTINid.Length != 4) return "GTIN не найден!";

            string CryptoKey, CryptoCode;
            // По идентификатору GTIN и серийному номеру пачки получаем крипто-данные.
            GetCryptoData(GTINid, Serial,out CryptoCode, out CryptoKey);

            //Проверяем найдены ли криптоданныу
            if (CryptoCode.Length == 0 ) return "Криптоданные не найдены!";
            if (CryptoKey.Length == 0) return "Криптоданные не найдены!";

            //Формируем строку с результатом
            string result = String.Format("01<<{0}>>21<<{1}>><<GS1Separator>>91<<{2}>><<GS1Separator>>92<<{3}>>{4}",GTIN,Serial,CryptoKey,CryptoCode,GTINid);
            
            return result;
        }

        /// <summary>
        /// Метод запрашивает в БД идентификатор GTINа
        /// </summary>
        /// <param name="GTIN">GTIN, для которого ищем идентификатор</param>
        /// <returns></returns>
        private string GetGtinId(string GTIN) 
        {
            //Результат по умолчанию
            string result = "";

            //Если соединение не открыто, то открываем его
            if (connection.State != System.Data.ConnectionState.Open) connection.Open();

            //Создаем запрос к БД
            string cmdString = String.Format("SELECT [Id] FROM [AntaresTracking_PRD].[dbo].[NtinDefinition] WHERE Ntin = '{0}'", GTIN);
            SqlCommand cmd = new SqlCommand( cmdString, connection);
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
            connection.Close();
            cmd.Dispose();
            //И проверяем
            if (!reader.IsClosed) throw new Exception();
                        
            return result;
        }

        /// <summary>
        /// Метод пытается получить для SGTIN криптоключ и криптохвост и БД 
        /// </summary>
        /// <param name="gtinId">Идентификатор GTIN</param>
        /// <param name="serial">Серийный номер</param>
        /// <param name="cryptoCode">Криптокод</param>
        /// <param name="cryptoKey">Криптоключь</param>
        private void GetCryptoData(string gtinId, string serial, out string cryptoCode, out string cryptoKey)
        {
            //Устанавливаем значения, возвращаемые по умолчанию.
            cryptoCode = "";
            cryptoKey = "";
            
            Dictionary<string, string> results = new Dictionary<string, string>();

            //Если соединения нет, открываем его
            if (connection.State != System.Data.ConnectionState.Open) connection.Open();

            //Формируем запрос
            string cmdString = String.Format("SELECT [VariableName] ,[VariableValue] FROM [AntaresTracking_PRD].[dbo].[ItemDetails] where Serial='{0}' and NtinId={1}",serial, gtinId);
            SqlCommand cmd = new SqlCommand(cmdString, connection);
            //И выполняем его
            SqlDataReader reader = cmd.ExecuteReader();

            //Читаем по порядку все ответы
            while (reader.Read())
            {
                string key = reader.GetValue(0).ToString();
                string value = reader.GetValue(1).ToString();
                results.Add(key, value); 
            }

            if (results.Count == 2)
            {
                cryptoCode = results["cryptocode"];
                cryptoKey = results["cryptokey"];
            }

            reader.Close();
            connection.Close();
            cmd.Dispose();
            if (!reader.IsClosed) throw new Exception();

        }


    }
}
