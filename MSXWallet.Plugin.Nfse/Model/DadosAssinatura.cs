using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSXWallet.Plugin.Nfse.Model
{
    public class DadosAssinatura
    {
        public string Id { get; set; }
        public string AlgorithmSignedInfo { get; set; }
        public string AlgorithmSignedMethod { get; set; }
        public string AlgorithmTransform { get; set; }
        public string AlgorithmDigestMethod { get; set; }
        public string Uri { get; set; }
        public string XPath { get; set; }
        public string DigestValue { get; set; }
        public string X509Certificate { get; set; }
    }
}
