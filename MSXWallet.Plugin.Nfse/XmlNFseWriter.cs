using MSXWallet.Plugin.Nfse.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSXWallet.Plugin.Nfse
{
    public class XmlNFseWriter : XmlWriter
    {
        /// <summary>
        /// Formato: AAAA-MM-DD 
        /// onde: 
        /// AAAA = ano com 4 caracteres 
        /// MM = mês com 2 caracteres 
        /// DD = dia com 2 caracteres 
        /// </summary>
        public const string DATA_FORMAT = "yyyy-MM-dd";

        /// <summary>
        /// Formato AAAA-MM-DDTHH:mm:ss 
        /// onde: 
        /// AAAA = ano com 4 caracteres 
        /// MM = mês com 2 caracteres 
        /// DD = dia com 2 caracteres 
        /// T = caractere de formatação que deve existir separando a data da hora 
        /// HH = hora com 2 caracteres 
        /// mm: minuto com 2 caracteres 
        /// ss: segundo com 2 caracteres
        /// </summary>
        public const string DATE_TIME_FORMAT = "yyyy-MM-ddTHH:mm:ss";

        public XmlNFseWriter(string startElement) : base(startElement)
        {
        }

        public void WriteCabecalho(int versao, int versaoDado)
        {
            //FluxoObrigatorio("", fluxochamada, versao, "tcDeclaracaoPrestacaoServico", obrigatorio);
            string fluxochamada = "Cabecalho";
            WriteNumberElement("Versão", fluxochamada, versao,
                new WriteInfo { CodigoCampo = "Versão", IsObrigatorio = true, });
            WriteNumberElement("versaoDados", fluxochamada, versaoDado,
                new WriteInfo { CodigoCampo = "versaoDados", IsObrigatorio = true, });
        }

        #region Servico

        /// <summary>
        /// Representa a estrutura da declaração da prestação do serviço assinada - tcDeclaracaoPrestacaoServico 
        /// </summary>
        public void WriteTcDeclaracaoPrestacaoServico(string localname, string fluxochamada, DeclaracaoPrestacaoServicoAssinada declaracao, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, declaracao, "tcDeclaracaoPrestacaoServico", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcDeclaracaoPrestacaoServico";
                WriteTcInfDeclaracaoPrestacaoServico("InfDeclaracaoPrestacaoServico", fluxochamadain, declaracao.Dados, true);
                WriteSignature("Signature", fluxochamadain, declaracao.Assinatura);
            }
        }

        /// <summary>
        /// Representa dados do da declaração do prestador do serviço - tcInfDeclaracaoPrestacaoServico 
        /// </summary>
        public void WriteTcInfDeclaracaoPrestacaoServico(string localname, string fluxochamada, DadosDeclaracaoPrestacaoServico dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcInfDeclaracaoPrestacaoServico", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcInfDeclaracaoPrestacaoServico";
                WriteTcInfRps("Rps", fluxochamadain, dados.Rps);
                WriteDate("Competencia", fluxochamadain, dados.DataCompetencia, true);
                WriteTcDadosServico("Servico", fluxochamadain, dados.Servico, true);
                WriteTcIdentificacaoPrestador("Prestador", fluxochamadain, dados.Prestador);
                WriteTcDadosTomador("TomadorServico", fluxochamadain, dados.TomadorServico);
                WriteTcDadosConstrucaoCivil("ConstrucaoCivil", fluxochamadain, dados.ConstrucaoCivil);
                WriteEnumElement("RegimeEspecialTributacao", fluxochamadain, dados.RegimeEspecialTributacao,
                    new WriteInfo { CodigoCampo = "tsRegimeEspecialTributacao", IsObrigatorio = false });
                WriteTsSimNao("OptanteSimplesNacional", fluxochamadain, dados.IsOptanteSimplesNacional, true);
                WriteTsSimNao("IncentivoFiscal", fluxochamadain, dados.IsIncentivoFiscal, true);
                WriteIdTag("Id", fluxochamadain);
            }
        }

        /// <summary>
        /// Representa dados para identificação de intermediário do serviço - tcDadosIntermediario 
        /// </summary>
        public void WriteTcDadosIntermediario(string localname, string fluxochamada, DadosIntermediario dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcDadosIntermediario", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcDadosIntermediario";
                WriteTcIdentificacaoIntermediario("IdentificacaoIntermediario", fluxochamadain, dados.Identificacao);
                WriteTsRazaoSocial("RazaoSocial", fluxochamadain, dados.RazaoSocial);
            }
        }

        /// <summary>
        /// Representa dados para identificação do tomador de serviço - tcIdentificacaoIntermediario
        /// </summary>
        public void WriteTcIdentificacaoIntermediario(string localname, string fluxochamada, IdentificacaoPessoaServico dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcIdentificacaoIntermediario", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcIdentificacaoIntermediario";
                WriteTcCpfCnpj("CpfCnpj", fluxochamadain, dados.CpfCnpj);
                WriteTsInscricaoMunicipal("InscricaoMunicipal", fluxochamadain, dados.InscricaoMunicipal);
            }
        }

        /// <summary>
        /// Representa dados para identificação do prestador de serviço - tcIdentificacaoPrestador 
        /// </summary>
        public void WriteTcIdentificacaoPrestador(string localname, string fluxochamada, IdentificacaoPessoaServico dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcIdentificacaoPrestador", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcIdentificacaoPrestador";
                WriteTcCpfCnpj("CpfCnpj", fluxochamadain, dados.CpfCnpj, true);
                WriteTsInscricaoMunicipal("InscricaoMunicipal", fluxochamadain, dados.InscricaoMunicipal);
            }
        }

        /// <summary>
        /// Representa dados para identificação do tomador de serviço - tcIdentificacaoTomador 
        /// </summary>
        public void WriteTcIdentificacaoTomador(string localname, string fluxochamada, IdentificacaoPessoaServico dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcIdentificacaoTomador", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcIdentificacaoTomador";
                WriteTcCpfCnpj("CpfCnpj", fluxochamadain, dados.CpfCnpj);
                WriteTsInscricaoMunicipal("InscricaoMunicipal", fluxochamadain, dados.InscricaoMunicipal);
            }
        }

        /// <summary>
        /// Representa dados do tomador de serviço - tcDadosTomador
        /// </summary>
        public void WriteTcDadosTomador(string localname, string fluxochamada, DadosTomador dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcDadosTomador", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcDadosTomador";
                WriteTcIdentificacaoTomador("IdentificacaoTomador", fluxochamadain, dados.IdentificacaoTomador);
                WriteTsRazaoSocial("RazaoSocial", fluxochamadain, dados.RazaoSocial);
                WriteTcEndereco("Endereco", fluxochamadain, dados.Endereco);
                WriteTcContato("Contato", fluxochamadain, dados.Contato);
            }
        }

        /// <summary>
        /// Representa dados que compõe o serviço prestado - tcDadosServico 
        /// </summary>
        public void WriteTcDadosServico(string localname, string fluxochamada, DadosServico dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcDadosServico", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcDadosServico";

                WriteTcValoresDeclaracaoServico("Valores", fluxochamadain, dados.Valores);
                WriteEnumElement("IssRetido", fluxochamadain, dados.IsIssRetido, 
                    new WriteInfo { CodigoCampo = "tsSimNao", IsObrigatorio = true, });
                WriteEnumElement("ResponsavelRetencao", fluxochamada, dados.ResponsavelRetencao,
                    new WriteInfo { CodigoCampo = "tsSimNao", IsObrigatorio = true, });
                WriteTsItemListaServico("ItemListaServico", fluxochamadain, dados.CodigoItemListaServico, true);
                WriteTsCodigoCnae("CodigoCnae", fluxochamadain, dados.CodigoCnae);
                WriteTsCodigoTributacao("CodigoTributacaoMunicipio", fluxochamadain, dados.CodigoTributacaoMunicipio);
                WriteTsDiscriminacao("Discriminacao", fluxochamadain, dados.Discriminacao, true);
                WriteTsCodigoMunicipioIbge("CodigoMunicipio", fluxochamadain, dados.CodigoMunicipio, true);
                WriteTsCodigoPaisBacen("CodigoPais", fluxochamadain, dados.CodigoPais);
                WriteTsExigibilidadeIss("ExigibilidadeISS", fluxochamadain, dados.ExigibilidadeISS, true);
                WriteTsCodigoMunicipioIbge("MunicipioIncidencia", fluxochamadain, dados.MunicipioIncidencia);
                WriteTsNumeroProcesso("NumeroProcesso", fluxochamadain, dados.NumeroProcesso);
            }
        }

        /// <summary>
        /// Representa um conjunto de valores que compõe a declaração do serviço  - tcValoresDeclaracaoServico 
        /// </summary>
        public void WriteTcValoresDeclaracaoServico(string localname, string fluxochamada, DadosValoresDeclaracaoServico dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcValoresDeclaracaoServico", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcValoresDeclaracaoServico";
                WriteTsValor("ValorServicos", fluxochamadain, dados.ValorServicos);
                WriteTsValor("ValorDeducoes", fluxochamadain, dados.ValorDeducoes);
                WriteTsValor("ValorPis", fluxochamadain, dados.ValorPis);
                WriteTsValor("ValorCofins", fluxochamadain, dados.ValorCofins);
                WriteTsValor("ValorInss", fluxochamadain, dados.ValorInss);
                WriteTsValor("ValorIr", fluxochamadain, dados.ValorIr);
                WriteTsValor("ValorCsll", fluxochamadain, dados.ValorCsll);
                WriteTsValor("OutrasRetencoes", fluxochamadain, dados.ValorOutrasRetencoes);
                WriteTsValor("ValorIss", fluxochamadain, dados.ValorIss);
                WriteTsAliquota("Aliquota", fluxochamadain, dados.Aliquota);
                WriteTsValor("DescontoIncondicionado", fluxochamadain, dados.DescontoIncondicionado);
                WriteTsValor("DescontoCondicionado", fluxochamadain, dados.DescontoCondicionado);
            }
        }

        #endregion

        #region RPS

        /// <summary>
        /// Representa dados informativos do Recibo Provisório de Serviço (RPS) - tcInfRps 
        /// </summary>
        public void WriteTcInfRps(string localname, string fluxochamada, DadosInformativoRPS dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcInfRps", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcInfRps";
                WriteTcIdentificacaoRps("IdentificacaoRps", fluxochamadain, dados.IdentificacaoRPS, true);
                WriteDate("DataEmissao", fluxochamadain, dados.DataEmissao, true);
                WriteTsStatusRps("Status", fluxochamadain, dados.Status, true);
                WriteTcIdentificacaoRps("RpsSubstituido", fluxochamadain, dados.RPSSubstituido);
                WriteIdTag("Id", fluxochamadain);
            }
        }

        /// <summary>
        /// Dados de identificação do RPS - tcIdentificacaoRps
        /// </summary>
        public void WriteTcIdentificacaoRps(string localname, string fluxochamada, DadosIdentificaoRPS dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcIdentificacaoRps", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcIdentificacaoRps";
                WriteTsNumeroRps("Numero", fluxochamadain, dados.Numero);
                WriteTsSerieRps("Serie", fluxochamadain, dados.Serie);
                WriteTsTipoRps("Tipo", fluxochamadain, dados.Tipo);
            }
        }

        /// <summary>
        /// Número do RPS - tsNumeroRps
        /// </summary>
        public void WriteTsNumeroRps(string localname, string fluxochamada, long numero, bool obrigatorio = false)
        {
            WriteNumberElement(localname, fluxochamada, numero,
                new WriteInfo { CodigoCampo = "tsNumeroRps", IsObrigatorio = obrigatorio, Maximo = 15 });
        }

        /// <summary>
        /// Número de série do RPS - tsSerieRps
        /// </summary>
        public void WriteTsSerieRps(string localname, string fluxochamada, string serie, bool obrigatorio = false)
        {
            WriteTextElement(localname, fluxochamada, serie, 
                new WriteInfo { CodigoCampo = "tsSerieRps", IsObrigatorio = obrigatorio, Maximo = 5 });
        }

        /// <summary>
        /// Código de tipo de RPS - tsTipoRps 
        /// </summary>
        public void WriteTsTipoRps(string localname, string fluxochamada, TipoRPSEnum tipo, bool obrigatorio = false)
        {
            WriteEnumElement(localname, fluxochamada, tipo, 
                new WriteInfo { CodigoCampo = "tsTipoRps", IsObrigatorio =  obrigatorio });
        }

        /// <summary>
        /// Código de status do RPS - tsStatusRps
        /// </summary>
        public void WriteTsStatusRps(string localname, string fluxochamada, StatusRPSEnum status, bool obrigatorio = false)
        {
            WriteEnumElement(localname, fluxochamada, status,
                new WriteInfo { CodigoCampo = "tsStatusRps", IsObrigatorio = obrigatorio });
        }

        #endregion

        /// <summary>
        /// Representa forma de contato com a pessoa (física/jurídica) - tcContato
        /// </summary>
        public void WriteSignature(string localname, string fluxochamada, DadosAssinatura assinatura)
        {
            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|Signature";
            }
        }

        /// <summary>
        /// Representa forma de contato com a pessoa (física/jurídica) - tcContato
        /// </summary>
        public void WriteTcContato(string localname, string fluxochamada, DadosContato dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcContato", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcContato";
                WriteTextElement("Telefone", fluxochamadain, dados.Telefone,
                    new WriteInfo { CodigoCampo = "tsTelefone", IsObrigatorio = false, Maximo = 20 });
                WriteTextElement("Email", fluxochamadain, dados.Email,
                    new WriteInfo { CodigoCampo = "tsEmail", IsObrigatorio = false, Maximo = 80 });
            }
        }

        /// <summary>
        /// Representação completa do endereço - tcEndereco
        /// </summary>
        public void WriteTcEndereco(string localname, string fluxochamada, DadosEndereco dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcEndereco", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcEndereco";
                WriteTextElement("Endereco", fluxochamadain, dados.Endereco,
                    new WriteInfo { CodigoCampo = "tsEndereco", IsObrigatorio = false, Maximo = 125 });
                WriteTextElement("Numero", fluxochamadain, dados.Numero,
                    new WriteInfo { CodigoCampo = "tsNumeroEndereco", IsObrigatorio = false, Maximo = 10 });
                WriteTextElement("Complemento", fluxochamadain, dados.Complemento,
                    new WriteInfo { CodigoCampo = "tsComplementoEndereco", IsObrigatorio = false, Maximo = 60 });
                WriteTextElement("Bairro", fluxochamadain, dados.Bairro,
                    new WriteInfo { CodigoCampo = "tsBairro", IsObrigatorio = false, Maximo = 60 });
                WriteTsCodigoMunicipioIbge("CodigoMunicipio", fluxochamadain, dados.CodigoMunicipio);
                WriteTextElement("Uf", fluxochamadain, dados.Uf,
                    new WriteInfo { CodigoCampo = "tsUf", IsObrigatorio = false, Maximo = 2 });
                WriteTsCodigoPaisBacen("CodigoPais", fluxochamadain, dados.CodigoPais);
                WriteTextElement("Cep", fluxochamadain, dados.Cep,
                    new WriteInfo { CodigoCampo = "tsCep", IsObrigatorio = false, Maximo = 8 });
            }
        }

        /// <summary>
        /// Número de CPF ou CNPJ - tcCpfCnpj
        /// </summary>
        public void WriteTcCpfCnpj(string localname, string fluxochamada, string valor, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, valor, "tcEndereco", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcCpfCnpj";
                if (valor.Length == 11)
                    WriteTsCpf("Cpf", fluxochamadain, valor, true);
                else if (valor.Length == 14)
                    WriteTsCnpj("Cnpj", fluxochamadain, valor, true);
            }
        }

        /// <summary>
        /// Representa dados para identificação de construção civil - tcDadosConstrucaoCivil
        /// </summary>
        public void WriteTcDadosConstrucaoCivil(string localname, string fluxochamada, DadosConstrucaoCivil dados, bool obrigatorio = false)
        {
            FluxoObrigatorio(localname, fluxochamada, dados, "tcDadosConstrucaoCivil", obrigatorio);

            using (CreateStartElement(localname))
            {
                string fluxochamadain = $"{fluxochamada}|tcDadosConstrucaoCivil";
                WriteTextElement("CodigoObra", fluxochamadain, dados.CodigoObra,
                    new WriteInfo { CodigoCampo = "tsCodigoObra ", IsObrigatorio = true, Maximo = 15 });
                WriteTextElement("Art", fluxochamadain, dados.CodigoArt,
                    new WriteInfo { CodigoCampo = "tsArt", IsObrigatorio = false, Maximo = 15 });
            }
        }

        /// <summary>
        /// Atributo de identificação da tag a ser assinada no documento XML - tsIdTag
        /// </summary>
        public void WriteIdTag(string localname, string fluxochamada)
        {
            string assinatura = "Assinatura ID TAG";
            WriteTextElement(localname, fluxochamada, assinatura,
                new WriteInfo { CodigoCampo = "tsIdTag", IsObrigatorio = true });
        }

        /// <summary>
        /// Formato: AAAA-MM-DD 
        /// onde: 
        /// AAAA = ano com 4 caracteres 
        /// MM = mês com 2 caracteres 
        /// DD = dia com 2 caracteres 
        /// </summary>
        public void WriteDate(string localname, string fluxochamada, DateTime valor, bool obrigatorio = false)
        {
            WriteDateElement(localname, fluxochamada, valor, DATA_FORMAT,
                new WriteInfo { CodigoCampo = "Date", IsObrigatorio = obrigatorio });
        }

        /// <summary>
        /// Valor monetário. - tsValor 
        /// Formato: 0.00 (ponto separando casa decimal) 
        /// </summary>
        /// <example>Ex: 1.234,56 = 1234.56       1.000,00 = 1000.00       1.000,00 = 1000</example>
        public void WriteTsValor(string localname, string fluxochamada, decimal valor, bool obrigatorio = false)
        {
            var format = CultureInfo.CurrentCulture.NumberFormat.Clone() as NumberFormatInfo;
            format.NumberDecimalSeparator = ".";
            format.NumberGroupSeparator = "";
            WriteNumberElement(localname, fluxochamada, valor, format,
                new WriteInfo { CodigoCampo = "tsValor", IsObrigatorio = obrigatorio });
        }

        /// <summary>
        /// Alíquota. Valor percentual.  - tsAliquota
        /// Formato: 00.00
        /// </summary>
        /// <example>Ex: 1% = 1       25,5% = 25.5       10% = 10</example>
        public void WriteTsAliquota(string localname, string fluxochamada, decimal valor, bool obrigatorio = false)
        {
            var format = CultureInfo.CurrentCulture.NumberFormat.Clone() as NumberFormatInfo;
            format.NumberDecimalSeparator = ".";
            format.NumberGroupSeparator = "";
            WriteNumberElement(localname, fluxochamada, valor, format,
                new WriteInfo { CodigoCampo = "tsAliquota", IsObrigatorio = obrigatorio });
        }

        /// <summary>
        /// Código de item da lista de serviço - tsItemListaServico
        /// Tipo: C
        /// </summary>
        public void WriteTsItemListaServico(string localname, string fluxochamada, string valor, bool obrigatorio = false)
        {
            WriteTextElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsItemListaServico", IsObrigatorio = obrigatorio, Maximo = 5 });
        }

        /// <summary>
        /// Código CNAE - tsCodigoCnae 
        /// Tipo: N
        /// </summary>
        public void WriteTsCodigoCnae(string localname, string fluxochamada, int valor, bool obrigatorio = false)
        {
            WriteNumberElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsCodigoCnae", IsObrigatorio = obrigatorio, Maximo = 7 });
        }

        /// <summary>
        /// Código de Tributação - tsCodigoTributacao 
        /// Tipo: C
        /// </summary>
        public void WriteTsCodigoTributacao(string localname, string fluxochamada, string valor, bool obrigatorio = false)
        {
            WriteTextElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsCodigoTributacao", IsObrigatorio = obrigatorio, Maximo = 20 });
        }

        /// <summary>
        /// Discriminação do conteúdo da NFS-e - tsDiscriminacao  
        /// Tipo: C
        /// </summary>
        public void WriteTsDiscriminacao(string localname, string fluxochamada, string valor, bool obrigatorio = false)
        {
            WriteTextElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsDiscriminacao", IsObrigatorio = obrigatorio, Maximo = 2000 });
        }

        /// <summary>
        /// Código de identificação do município conforme tabela do IBGE - tsCodigoMunicipioIbge  
        /// Tipo: N
        /// </summary>
        public void WriteTsCodigoMunicipioIbge(string localname, string fluxochamada, int valor, bool obrigatorio = false)
        {
            WriteNumberElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsCodigoMunicipioIbge", IsObrigatorio = obrigatorio, Maximo = 7 });
        }

        /// <summary>
        /// Código de identificação do município conforme tabela do BACEN - tsCodigoPaisBacen  
        /// Tipo: C
        /// </summary>
        public void WriteTsCodigoPaisBacen(string localname, string fluxochamada, string valor, bool obrigatorio = false)
        {
            WriteTextElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsCodigoPaisBacen", IsObrigatorio = obrigatorio, Maximo = 4 });
        }

        /// <summary>
        /// Código de natureza da operação - tsExigibilidadeIss  
        /// Tipo: N
        /// </summary>
        public void WriteTsExigibilidadeIss(string localname, string fluxochamada, ExigibilidadeIssEnum valor, bool obrigatorio = false)
        {
            WriteEnumElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsExigibilidadeIss", IsObrigatorio = obrigatorio, Maximo = 2 });
        }

        /// <summary>
        /// Número do processo judicial ou administrativo de suspensão da exigibilidade - tsNumeroProcesso  
        /// Tipo: C
        /// </summary>
        public void WriteTsNumeroProcesso(string localname, string fluxochamada, string valor, bool obrigatorio = false)
        {
            WriteTextElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsNumeroProcesso", IsObrigatorio = obrigatorio, Maximo = 30 });
        }

        /// <summary>
        /// Número de inscrição municipal - tsInscricaoMunicipal
        /// Tipo: C
        /// </summary>
        public void WriteTsInscricaoMunicipal(string localname, string fluxochamada, string valor, bool obrigatorio = false)
        {
            WriteTextElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsInscricaoMunicipal", IsObrigatorio = obrigatorio, Maximo = 15 });
        }

        /// <summary>
        /// Número de CPF - tsCpf
        /// Tipo: C
        /// </summary>
        public void WriteTsCpf(string localname, string fluxochamada, string valor, bool obrigatorio = false)
        {
            WriteTextElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsCpf", IsObrigatorio = obrigatorio, Maximo = 11 });
        }

        /// <summary>
        /// Número CNPJ - tsCnpj 
        /// Tipo: C
        /// </summary>
        public void WriteTsCnpj(string localname, string fluxochamada, string valor, bool obrigatorio = false)
        {
            WriteTextElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsCnpj", IsObrigatorio = obrigatorio, Maximo = 11 });
        }

        /// <summary>
        /// Razão Social do contribuinte - tsRazaoSocial 
        /// Tipo: C
        /// </summary>
        public void WriteTsRazaoSocial(string localname, string fluxochamada, string valor, bool obrigatorio = false)
        {
            WriteTextElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsRazaoSocial", IsObrigatorio = obrigatorio, Maximo = 150 });
        }

        /// <summary>
        /// Identificação de Sim/Não - tsSimNao 
        /// </summary>
        public void WriteTsSimNao(string localname, string fluxochamada, SimNaoEnum valor, bool obrigatorio = false)
        {
            WriteEnumElement(localname, fluxochamada, valor,
                new WriteInfo { CodigoCampo = "tsSimNao", IsObrigatorio = obrigatorio });
        }
    }
}
