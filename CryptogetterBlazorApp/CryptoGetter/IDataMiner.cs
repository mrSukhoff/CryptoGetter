namespace CryptogetterBlazorApp.CryptoGetter;

    interface IDataMiner
    {
        (string, string) GetCrypto(String sGTIN);
    }
