using System;

namespace CryptoGetterLibrary
{
    public interface IDataMiner
    {
        (string, string) GetCrypto(String sGTIN);
    }
}
