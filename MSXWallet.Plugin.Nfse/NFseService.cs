using MSXWallet.Plugin.Nfse.Model;
using MSXWallet.Plugin.Nfse.ProxyServiceNFse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSXWallet.Plugin.Nfse
{
    public class NFseService
    {
        private readonly NfseWSServiceSoapClient _client;

        public NFseService()
        {
            _client = new NfseWSServiceSoapClient();
        }

        public void GerarNFseNiteroi()
        {
            var declaracao = new DadosDeclaracaoPrestacaoServico
            {
                ConstrucaoCivil = new DadosConstrucaoCivil
                {
                    CodigoArt = "64724",
                    CodigoObra = "743779"
                },
                DataCompetencia = DateTime.Now,
                IsIncentivoFiscal = SimNaoEnum.Nao,
                IsOptanteSimplesNacional = SimNaoEnum.Sim,
                Prestador = new IdentificacaoPessoaServico
                {
                    CpfCnpj = "7847389423",
                    InscricaoMunicipal = "4803240",
                },
                RegimeEspecialTributacao = RegimeEspecialTributacaoEnum.MicroempresaMunicipal,
                Rps = new DadosInformativoRPS
                {
                    DataEmissao = DateTime.Now,
                    IdentificacaoRPS = new DadosIdentificaoRPS
                    {
                        Numero = 78247,
                        Serie = "474AS",
                        Tipo = TipoRPSEnum.Cupom,
                    },
                    RPSSubstituido = new DadosIdentificaoRPS
                    {
                        Numero = 843189,
                        Serie = "839IA",
                        Tipo = TipoRPSEnum.NotaFiscalConjugada,
                    },
                    Status = StatusRPSEnum.Normal,
                },
                Servico = new DadosServico
                {
                    CodigoCnae = 748378,
                    CodigoItemListaServico = "499",
                    CodigoMunicipio = 8938493,
                    CodigoPais = "55",
                    CodigoTributacaoMunicipio = "522",
                    Discriminacao = "5kfkd",
                    ExigibilidadeISS = ExigibilidadeIssEnum.Exigivel,
                    IsIssRetido = SimNaoEnum.Sim,
                    MunicipioIncidencia = 4948392,
                    NumeroProcesso = "594859485",
                    ResponsavelRetencao = ResponsavelRetencaoEnum.Intermediário,
                    Valores = new DadosValoresDeclaracaoServico
                    {
                        Aliquota = 19.9m,
                        DescontoCondicionado = 0.5m,
                        DescontoIncondicionado = 0.3m,
                        ValorCofins = 19,
                        ValorCsll = 100,
                        ValorDeducoes = 1999,
                        ValorInss = 73478,
                        ValorIr = 4728472,
                        ValorIss = 13233.7438m,
                        ValorOutrasRetencoes = 748.929m,
                        ValorPis = 478,
                        ValorServicos = 4388,
                    },
                },
                TomadorServico = new DadosTomador
                {
                    Contato = new DadosContato
                    {
                        Email = "teste@teste.com",
                        Telefone = "(21)99981-2388",
                    },
                    Endereco = new DadosEndereco
                    {
                        Endereco = "Rua Antonio Victor",
                        Numero = "61",
                        Complemento = "Casa",
                        Bairro = "Mutuá",
                        CodigoMunicipio = 12,
                        CodigoPais = "55",
                        Cep = "24461170",
                        Uf = "RJ",
                    },
                    IdentificacaoTomador = new IdentificacaoPessoaServico
                    {
                        CpfCnpj = "5453553",
                        InscricaoMunicipal = "74287428",
                    },
                    RazaoSocial = "Teste LTDA",
                },
                ValoresDeclaracao = new DadosValoresDeclaracaoServico
                {
                    Aliquota = 19.9m,
                    DescontoCondicionado = 0.5m,
                    DescontoIncondicionado = 0.3m,
                    ValorCofins = 19,
                    ValorCsll = 100,
                    ValorDeducoes = 1999,
                    ValorInss = 73478,
                    ValorIr = 4728472,
                    ValorIss = 13233.7438m,
                    ValorOutrasRetencoes = 1748.929m,
                    ValorPis = 478,
                    ValorServicos = 4388,
                },
            };
            DadosAssinatura assinatura = new DadosAssinatura();

            string dadosxml = XmlNFse.BuildXmlGerarNFseEnvio(declaracao, assinatura);
            string cabecalhoxml = XmlNFse.BuildXmlCabecalho(1, 1);

            string response = _client.GerarNfse(cabecalhoxml, dadosxml);
        }
    }
}
