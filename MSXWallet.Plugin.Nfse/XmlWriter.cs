using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MSXWallet.Plugin.Nfse
{
    public class XmlWriter
    {
        private class XmlStartElement : IDisposable
        {
            private readonly System.Xml.XmlWriter writer;

            public XmlStartElement(System.Xml.XmlWriter writer, string name)
            {
                this.writer = writer;
                this.writer.WriteStartElement(name);
            }

            public void Dispose()
            {
                this.writer.WriteEndElement();
            }
        }

        protected class WriteInfo
        {
            public string CodigoCampo { get; set; }
            //public string DescricaoCampo { get; set; }
            public bool IsObrigatorio { get; set; }
            public int Maximo { get; set; }
            public int Padleft { get; set; }
            public char Chpadleft { get; set; }
        }

        private StringBuilder _sb;
        private System.Xml.XmlWriter _xml;
        private readonly string _startElement;

        // Format providers


        public XmlWriter(string startElement)
        {
            _sb = new StringBuilder();
            _xml = System.Xml.XmlWriter.Create(_sb);
            _startElement = startElement;
            _xml.WriteStartElement(startElement);
        }

        public IDisposable CreateStartElement(string name)
        {
            return new XmlStartElement(_xml, name);
        }

        protected void WriteDateElement(string localname, string fluxochamada, DateTime value, string format, WriteInfo info)
        {
            FluxoObrigatorio(localname, fluxochamada, value, info.CodigoCampo, info.IsObrigatorio);

            string stringFormatada = value.ToString(format);
            _xml.WriteStartElement(localname);
            _xml.WriteString(stringFormatada);
            _xml.WriteEndElement();
        }

        protected void WriteNumberElement(string localname, string fluxochamada, int value, WriteInfo info, int padleft = 0, char chpadleft = default(char))
        {
            FluxoObrigatorio(localname, fluxochamada, value, info.CodigoCampo, info.IsObrigatorio);

            string stringFormatada = value.ToString();
            if (info.Maximo > 0 && stringFormatada.Length > info.Maximo)
                throw GerarEx("Máximo", localname, fluxochamada, info.CodigoCampo, String.Empty, value);
            _xml.WriteStartElement(localname);
            _xml.WriteString(stringFormatada.PadLeft(padleft, chpadleft));
            _xml.WriteEndElement();
        }

        protected void WriteNumberElement(string localname, string fluxochamada, long value, WriteInfo info, int padleft = 0, char chpadleft = default(char))
        {
            FluxoObrigatorio(localname, fluxochamada, value, info.CodigoCampo, info.IsObrigatorio);

            string stringFormatada = value.ToString();
            if (info.Maximo > 0 && stringFormatada.Length > info.Maximo)
                throw GerarEx("Máximo", localname, fluxochamada, info.CodigoCampo, String.Empty, value);
            _xml.WriteStartElement(localname);
            _xml.WriteString(stringFormatada.PadLeft(padleft, chpadleft));
            _xml.WriteEndElement();
        }

        protected void WriteNumberElement(string localname, string fluxochamada, decimal value, NumberFormatInfo format, WriteInfo info)
        {
            FluxoObrigatorio(localname, fluxochamada, value, info.CodigoCampo, info.IsObrigatorio);

            string stringFormatada = value.ToString(format);

            _xml.WriteStartElement(localname);
            _xml.WriteString(stringFormatada.PadLeft(info.Padleft, info.Chpadleft));
            _xml.WriteEndElement();
        }

        protected void WriteTextElement(string localname, string fluxochamada, string value, WriteInfo info, int padleft = 0, char chpadleft = default(char))
        {
            string stringFormatada = value;

            FluxoObrigatorio(localname, fluxochamada, value, info.CodigoCampo, info.IsObrigatorio);

            if (info.Maximo > 0 && value.Length > info.Maximo)
                throw GerarEx("Máximo", localname, fluxochamada, info.CodigoCampo, String.Empty, value);

            _xml.WriteStartElement(localname);
            _xml.WriteString(stringFormatada.PadLeft(padleft, chpadleft));
            _xml.WriteEndElement();
        }

        protected void WriteEnumElement(string localname, string fluxochamada, Enum value, WriteInfo info, bool obrigatorio = false)
        {
            int valorEnum = Convert.ToInt32(value);
            if (obrigatorio && valorEnum > 0)
                throw GerarEx("Obrigatório", localname, fluxochamada, info.CodigoCampo, String.Empty, value);
            if (!obrigatorio && valorEnum == 0) return; // Não é obrigatório e não tem valor - não precisa escrever a tag
            string stringFormatada = valorEnum.ToString();

            _xml.WriteStartElement(localname);
            _xml.WriteString(stringFormatada);
            _xml.WriteEndElement();
        }

        protected void FluxoObrigatorio<TType>(string localname, string fluxochamada, TType value, string codigoCampo, bool obrigatorio)
        {
            Func<string, bool> defaultString = (v) => {
                return String.IsNullOrEmpty(v);
            };
            Func<DateTime, bool> defaultDatetime = (v) => {
                return v.Equals(DateTime.MinValue) || v.Equals(DateTime.MaxValue);
            };
            if (obrigatorio && value.Equals(default(TType)))
                throw GerarEx("Obrigatório", localname, fluxochamada, codigoCampo, String.Empty, value);
            if (!obrigatorio && value.Equals(default(TType))) return; // Não é obrigatório e não tem valor - não precisa escrever a tag

            if (typeof(TType) == typeof(string) && defaultString(value as string))
            {
                if (obrigatorio)
                    throw GerarEx("Obrigatório", localname, fluxochamada, codigoCampo, String.Empty, value);
                else
                    return;
            }
            else if(typeof(TType) == typeof(DateTime) && defaultDatetime(Convert.ToDateTime(value)))
            {
                if (obrigatorio)
                    throw GerarEx("Obrigatório", localname, fluxochamada, codigoCampo, String.Empty, value);
                else
                    return;
            }
        }

        public Exception GerarEx(string sufixMsg, string localname, string fluxochamada, string codigoCampo, string descricaoCampo, object value)
        {
            string msg = $"{sufixMsg} - Campo: {codigoCampo}/{descricaoCampo} - Fluxo: {fluxochamada}|{localname} - Valor: {value}";
            return new Exception(msg);
        }

        public string Build()
        {
            _xml.WriteEndElement();
            _xml.Close();
            return _sb.ToString();
        }

    }
}
