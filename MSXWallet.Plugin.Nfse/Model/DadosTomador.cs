namespace MSXWallet.Plugin.Nfse.Model
{
    public class DadosTomador
    {
        public IdentificacaoPessoaServico IdentificacaoTomador { get; set; }
        public string RazaoSocial { get; set; }
        public DadosEndereco Endereco { get; set; }
        public DadosContato Contato { get; set; }
    }
}
