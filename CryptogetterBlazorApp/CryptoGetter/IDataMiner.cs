namespace CryptogetterBlazorApp.CryptoGetter;

    internal interface IDataMiner
    {
        (string, string) GetCrypto(String sGTIN);
    }
