// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Aplicação para segregar virtualmente os lotes e documentos.
    /// </summary>
    [DebuggerDisplay("{Nome,nq}")]
    public class Aplicacao : Base
    {
        /// <summary>
        /// Contrante proprietário da aplicação.
        /// </summary>
        public Contratante Contratante { get; set; }

        /// <summary>
        /// Nome da aplicação.
        /// </summary>
        [XmlAttribute]
        public string Nome { get; set; }

        /// <summary>
        /// Breve descrição da aplicação.
        /// </summary>
        [XmlAttribute]
        public string Descricao { get; set; }

        /// <summary>
        /// Indica se a aplicação está ou não ativa.
        /// </summary>
        [XmlAttribute]
        public bool Ativa { get; set; }

        /// <summary>
        /// Data de criação da aplicação.
        /// </summary>
        [XmlAttribute]
        public DateTime Data { get; set; }

        /// <summary>
        /// Configurações do processo operacional das assinaturas e de integração.
        /// </summary>
        public Configuracao Configuracao { get; set; }
    }
}