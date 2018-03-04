using MSXWallet.Plugin.Nfse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebISS_NFse
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new NFseService();
            service.GerarNFseNiteroi();
            //xb.NumeroRPS
        }
    }
}
