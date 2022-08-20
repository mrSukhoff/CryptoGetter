using System;

namespace CryptoGetter
{
    internal interface IDataMiner
    {
        (string, string) GetCrypto(String sGTIN);
    }
}
