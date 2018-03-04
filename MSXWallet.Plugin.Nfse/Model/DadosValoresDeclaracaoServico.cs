namespace MSXWallet.Plugin.Nfse.Model
{
    public class DadosValoresDeclaracaoServico
    {
        public decimal ValorServicos { get; set; }
        public decimal ValorDeducoes { get; set; }
        public decimal ValorPis { get; set; }
        public decimal ValorCofins { get; set; }
        public decimal ValorInss { get; set; }
        public decimal ValorIr { get; set; }
        public decimal ValorCsll { get; set; }
        public decimal ValorOutrasRetencoes { get; set; }
        public decimal ValorIss { get; set; }
        public decimal Aliquota { get; set; }
        public decimal DescontoIncondicionado { get; set; }
        public decimal DescontoCondicionado { get; set; }
    }
}
