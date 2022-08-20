using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGetter
{
    internal interface IDataMiner
    {
        (string, string) GetCrypto(String sGTIN);
        void Close();
    }
}
