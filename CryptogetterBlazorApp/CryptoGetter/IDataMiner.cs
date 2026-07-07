namespace CryptogetterBlazorApp.CryptoGetter
{
	public interface IDataMiner
	{
		Task<DataMinerResult> GetCodeAsync(string sgtin);
	}
}