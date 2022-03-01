// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Diagnostics;
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
        /// Número do telefone celular associado à entidade.
        /// </summary>
        [XmlAttribute]
        public string Telefone { get; set; }
    }
}