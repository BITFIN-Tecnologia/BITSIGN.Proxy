// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Threading;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Entidade que define os dados de uma pessoa física ou jurídica.
    /// </summary>
    [DebuggerDisplay("{Nome,nq}")]
    public class Entidade : Base
    {
        /// <summary>
        /// Formato de serialização da data de nascimento ou fundação da entidade.
        /// </summary>
        public const string FormatoDaData = "yyyy-MM-dd";

        /// <summary>
        /// Nome completo da entidade.
        /// </summary>
        [XmlAttribute]
        public string Nome { get; set; }

        /// <summary>
        /// Documento (CNPJ/CPF) da entidade.
        /// </summary>
        [XmlAttribute]
        public string Documento { get; set; }

        /// <summary>
        /// Endereço de e-mail da entidade.
        /// </summary>
        [XmlAttribute]
        public string Email { get; set; }

        /// <summary>
        /// Número do telefone celular associado à entidade. Formato: (##) #.####-####. Exemplo: (19) 9.1111-2222.
        /// </summary>
        [XmlAttribute]
        public string Telefone { get; set; }

        /// <summary>
        /// Data de Nascimento (para Pessoa Física) ou Data de Fundação (para Pessoa Jurídica) em formato yyyy-MM-dd.
        /// </summary>
        [XmlIgnore, JsonIgnore]
        public DateTime? DataDeNascimentoOuFundacao
        {
            get =>
                string.IsNullOrWhiteSpace(this.NascimentoOuFundacao) ?
                null :
                DateTime.TryParseExact(
                    this.NascimentoOuFundacao,
                    FormatoDaData,
                    Thread.CurrentThread.CurrentCulture,
                    DateTimeStyles.None,
                    out var data) ? data :
                null;
            set => this.NascimentoOuFundacao = value?.ToString(FormatoDaData);
        }

        /// <summary>
        /// Data de Nascimento (para Pessoa Física) ou Data de Fundação (para Pessoa Jurídica) em formato yyyy-MM-dd.
        /// </summary>
        [XmlAttribute]
        public string NascimentoOuFundacao { get; set; }

        /// <summary>
        /// Idioma em que o assinador (Web ou Móvel) será apresentado. Opções Disponíveis: "pt-br".
        /// </summary>
        [XmlAttribute]
        public string Idioma { get; set; }
    }
}