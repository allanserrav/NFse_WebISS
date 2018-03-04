namespace MSXWallet.Plugin.Nfse.Model
{
    public class DadosServico
    {
        public DadosValoresDeclaracaoServico Valores { get; set; }
        public SimNaoEnum IsIssRetido { get; set; }
        public ResponsavelRetencaoEnum ResponsavelRetencao { get; set; }
        public int CodigoCnae { get; set; }
        public string CodigoItemListaServico { get; set; }
        public string CodigoTributacaoMunicipio { get; set; }
        public string Discriminacao { get; set; }
        public int CodigoMunicipio { get; set; }
        public string CodigoPais { get; set; }
        public ExigibilidadeIssEnum ExigibilidadeISS { get; set; }
        public int MunicipioIncidencia { get; set; }
        public string NumeroProcesso { get; set; }
    }
}
