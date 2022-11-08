// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Unidade Organizacional (OU) para emissão de certificados de cadeia privada.
    /// </summary>
    [DebuggerDisplay("{Alias,nq}")]
    public class Organizacao : Base
    {
        /// <summary>
        /// Contrante proprietário da organização.
        /// </summary>
        public Contratante Contratante { get; set; }

        /// <summary>
        /// Alias que identifica unicamente a organização.
        /// </summary>
        [XmlAttribute]
        public string Alias { get; set; }

        /// <summary>
        /// Descrição de uso e finalidade da organização.
        /// </summary>
        [XmlAttribute]
        public string Descricao { get; set; }

        /// <summary>
        /// Sigla do país em que a organização está vinculada (ISO 3166-1 alfa-2).
        /// </summary>
        [XmlAttribute]
        public string Pais { get; set; }

        /// <summary>
        /// Data de criação da organização.
        /// </summary>
        [XmlAttribute]
        public DateTime Data { get; set; }

        /// <summary>
        /// Dados do certificado emitido para a organização.
        /// </summary>
        public Certificado Certificado { get; set; }

        /// <summary>
        /// Relação de certificados emitidos nesta organização.
        /// </summary>
        public List<Emissao> Emissoes { get; set; }
    }
}