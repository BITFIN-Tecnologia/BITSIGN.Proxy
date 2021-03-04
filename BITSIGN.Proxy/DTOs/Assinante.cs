// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Informações do assinante.
    /// </summary>
    [DebuggerDisplay("{Entidade.Nome,nq}")]
    public class Assinante : Base
    {
        /// <summary>
        /// Pessoa física ou jurídica definida como assinante.
        /// </summary>
        public Entidade Entidade { get; set; }

        /// <summary>
        /// Indica o status atual do assinante.
        /// </summary>
        [XmlAttribute]
        public string Status { get; set; }

        /// <summary>
        /// Data em que o status foi definido.
        /// </summary>
        [XmlAttribute]
        public DateTime DataDoStatus { get; set; }

        /// <summary>
        /// Indica quem forneceu a data/hora para a assinatura.
        /// </summary>
        [XmlAttribute]
        public string ProvedorDoTempo { get; set; }

        /// <summary>
        /// Indica se o assinante é ou não obrigatório.
        /// </summary>
        [XmlAttribute]
        public bool Obrigatorio { get; set; }

        /// <summary>
        /// Observações complementares do assinante. Serve para indicar, por exemplo, o motivo de rejeição.
        /// </summary>
        [XmlAttribute]
        public string Observacoes { get; set; }

        /// <summary>
        /// Indica se o assinante deve ser notificado de que há um novo documento disponível para assinatura.
        /// </summary>
        [XmlAttribute]
        public bool Notificar { get; set; }
    }
}