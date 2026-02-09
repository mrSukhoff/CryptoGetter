namespace CryptogetterBlazorApp.CryptoGetter
{
    public class DataMinerFactory
    {
        public IDataMiner? TryGetDataMiner(Server server)
        {
			return server.Type switch
			{
				ServerType.Antares => new AntaresDataMiner(server),
				ServerType.Medtech => new MedtechDataMiner(server),
				_ => null
			};
		}
    }
}
