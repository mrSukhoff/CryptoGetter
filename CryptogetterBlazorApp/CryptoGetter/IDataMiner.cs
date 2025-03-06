namespace CryptogetterBlazorApp.CryptoGetter;

	public interface IDataMiner
	{
		Task<(string, string)> GetCrypto(string sGTIN);
	}
