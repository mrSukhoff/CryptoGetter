using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
//using System.Windows;

namespace CryptoGetter
{
    class DataBaseCryptoExtractor
    {
        //Соединение с БД
        private SqlConnection connection;

        /// <summary>
        /// Метод возвращает истину если все данные получены и имеют правильную длину, в выходных переменных сохраняет криптоключ и криптокод.
        /// В случае ошибки возвращает ложь и сообщение об ошибке в переменной CryptoCode
        /// </summary>
        /// <param name="GTIN">GTIN</param>
        /// <param name="Serial">серийный номер пачки</param>
        /// <returns></returns>
        public bool GetCrypto (string servername, string GTIN, string Serial, out string CryptoKey, out string CryptoCode)
        {
            string connectionString = "Data Source=" + servername + ";Initial Catalog=AntaresTracking_PRD;Persist Security Info=True;User ID=tav;Password=tav";
            connection = new SqlConnection(connectionString);
            connection.Open();

            CryptoKey = "";
            CryptoCode = "";

            //Получаем по GTIN его идентификатор.
            string GTINid = GetGtinId(GTIN);

            //MessageBox.Show(servername + GTIN + Serial);
            //Проверяем найден ли GTIN
            if (GTINid.Length != 4)
            { 
                CryptoCode = "GTIN не найден!";
                return false;
            }
            
            // По идентификатору GTIN и серийному номеру пачки получаем крипто-данные.
            GetCryptoData(GTINid, Serial, out string cCode, out string cKey);

            connection.Close();
            connection.Dispose();
            
            //Проверяем найдены ли криптоданные
            if (cCode.Length != 44) 
            {
                CryptoCode = "Неверная длина криптокода!";
                return false;
            }
                
            if (cKey.Length !=4)
            {
                CryptoCode = "Неверная длина криптоключа!";
                return false;
            }
            
            CryptoKey = cKey;
            CryptoCode = cCode;
            return true;
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
        private void GetCryptoData(string gtinId, string serial, out string cryptoCode, out string cryptoKey)
        {
            //Устанавливаем значения, возвращаемые по умолчанию.
            cryptoCode = "Криптокод не найден!";
            cryptoKey = "Криптоключь не найден!";

            Dictionary<string, string> results = new Dictionary<string, string>();

            //Если соединения нет, открываем его
            if (connection.State != System.Data.ConnectionState.Open) connection.Open();

            //Формируем запрос
            string cmdString = String.Format("SELECT [VariableName] ,[VariableValue] FROM [AntaresTracking_PRD].[dbo].[ItemDetails] where Serial='{0}' and NtinId={1}", serial, gtinId);
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

            if (results.Count >= 2)
            {
                cryptoCode = results["cryptocode"];
                cryptoKey = results["cryptokey"];
            }

            reader.Close();
            cmd.Dispose();
        }
    }
}
