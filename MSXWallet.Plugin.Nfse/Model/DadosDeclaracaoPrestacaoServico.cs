using System;

namespace MSXWallet.Plugin.Nfse.Model
{
    public class DadosDeclaracaoPrestacaoServico
    {
        public DadosInformativoRPS Rps { get; set; }
        public DateTime DataCompetencia { get; set; }
        public DadosValoresDeclaracaoServico ValoresDeclaracao { get; set; }
        public DadosServico Servico { get; set; }
        public IdentificacaoPessoaServico Prestador { get; set; }
        public DadosTomador TomadorServico { get; set; }
        public DadosConstrucaoCivil ConstrucaoCivil { get; set; }
        public RegimeEspecialTributacaoEnum RegimeEspecialTributacao { get; set; }
        public SimNaoEnum IsOptanteSimplesNacional { get; set; }
        public SimNaoEnum IsIncentivoFiscal { get; set; }
    }
}
