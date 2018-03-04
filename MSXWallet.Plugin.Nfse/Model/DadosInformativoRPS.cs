using System;

namespace MSXWallet.Plugin.Nfse.Model
{
    public class DadosInformativoRPS
    {
        public DadosIdentificaoRPS IdentificacaoRPS { get; set; }
        public DateTime DataEmissao { get; set; }
        public StatusRPSEnum Status { get; set; }
        public DadosIdentificaoRPS RPSSubstituido { get; set; }
    }
}
