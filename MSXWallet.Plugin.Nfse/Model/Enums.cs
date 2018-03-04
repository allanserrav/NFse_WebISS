using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSXWallet.Plugin.Nfse.Model
{
    #region Enums

    /// <summary>
    /// Código de tipo de RPS
    /// </summary>
    public enum TipoRPSEnum
    {
        Indefinido = 0,
        RPS = 1,
        /// <summary>
        /// Nota Fiscal Conjugada (Mista)
        /// </summary>
        NotaFiscalConjugada = 2,
        Cupom = 3,
    }

    public enum StatusRPSEnum
    {
        Indefinido = 0,
        Normal = 1,
        Cancelado = 2,
    }

    public enum SimNaoEnum
    {
        Indefinido = 0,
        Sim = 1,
        Nao = 2,
    }

    public enum ResponsavelRetencaoEnum
    {
        Indefinido = 0,
        Tomador = 1,
        Intermediário = 2,
    }

    public enum ExigibilidadeIssEnum
    {
        Indefinido = 0,
        Exigivel = 1,
        NaoIncidencia = 2,
        Isencao = 3,
        Exportacao = 4,
        Imunidade = 5,
        ExigibilidadeSuspensaDecisaoJudicial = 6,
        ExigibilidadeSuspensaProcessoAdministrativo = 7,
    }

    /// <summary>
    /// Código de identificação do regime especial de tributação 
    /// </summary>
    public enum RegimeEspecialTributacaoEnum
    {
        Indefinido = 0,
        MicroempresaMunicipal = 1,
        Estimativa = 2,
        SociedadeProfissionais = 3,
        Cooperativa = 4,
        /// <summary>
        ///  Microempresário Individual (MEI)
        /// </summary>
        MicroempresarioIndividual = 5,
        /// <summary>
        /// Microempresário e Empresa de Pequeno Porte (ME EPP)
        /// </summary>
        MicroempresarioEmpresaPequenoPorte = 6,
    }

    #endregion
}
