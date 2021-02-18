// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Notificação de assinantes e callbacks.
    /// </summary>
    [DebuggerDisplay("{Protocolo,nq}: {Destinatario,nq} - ({Assunto,nq})")]
    public class Notificacao : Base
    {
        /// <summary>
        /// Data em que a notificação foi gerada pela sistema.
        /// </summary>
        [XmlAttribute]
        public DateTime Data { get; set; }

        /// <summary>
        /// Indica o status atual da notificação.
        /// </summary>
        [XmlAttribute]
        public string Status { get; set; }

        /// <summary>
        /// Data em que o status foi definido.
        /// </summary>
        [XmlAttribute]
        public DateTime DataDoStatus { get; set; }

        /// <summary>
        /// Protocolo utilizado para envio da notificação.
        /// </summary>
        [XmlAttribute]
        public string Protocolo { get; set; }

        /// <summary>
        /// Endereço de destino para onde a notificação foi enviada.
        /// </summary>
        [XmlAttribute]
        public string Destinatario { get; set; }

        /// <summary>
        /// O assunto sobre do que se trata a notificação.
        /// </summary>
        [XmlAttribute]
        public string Assunto { get; set; }
    }
}