using MSXWallet.Plugin.Nfse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSXWallet.Plugin.Nfse
{
    public static class XmlNFse
    {
        public const string MAIN_ELEMENT_GERARNFSEENVIO = "GerarNfseEnvio";
        public const string MAIN_ELEMENT_CABECALHO = "cabecalho ";

        public static string BuildXmlGerarNFseEnvio(DadosDeclaracaoPrestacaoServico rps, DadosAssinatura assinatura)
        {
            XmlNFseWriter writer = new XmlNFseWriter(MAIN_ELEMENT_GERARNFSEENVIO);
            string fluxochamadain = MAIN_ELEMENT_GERARNFSEENVIO;
            DeclaracaoPrestacaoServicoAssinada assinada = new DeclaracaoPrestacaoServicoAssinada() { Dados = rps, Assinatura = assinatura, };
            writer.WriteTcDeclaracaoPrestacaoServico("RPS", fluxochamadain, assinada);
            return writer.Build();
        }

        public static string BuildXmlCabecalho(int versao, int versaoDado)
        {
            XmlNFseWriter writer = new XmlNFseWriter(MAIN_ELEMENT_CABECALHO);
            writer.WriteCabecalho(versao, versaoDado);
            return writer.Build();
        }
    }
}
