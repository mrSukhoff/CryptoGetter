namespace CryptogetterBlazorApp.CryptoGetter
{
    internal class DataMinerFactory
    {
        public static IDataMiner GetDataMiner(Server server)
        {
            if (server.Type == ServerType.Antares) return new AntaresDataMiner(server);
            if (server.Type == ServerType.Medtech) return new MedtechDataMiner(server);
            throw new Exception("Класс типа сервера не описан!");
        }
    }
}
