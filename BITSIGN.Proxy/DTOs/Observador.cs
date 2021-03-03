// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Diagnostics;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Observador do processo de assinaturas.
    /// </summary>
    [DebuggerDisplay("{Email,nq}")]
    public class Observador : Base
    {
        /// <summary>
        /// Endereço de e-mail para onde a notificação será enviada.
        /// </summary>
        [XmlAttribute]
        public string Email { get; set; }

        /// <summary>
        /// Indica o tipo de observador (Global ou Pontual).
        /// </summary>
        [XmlAttribute]
        public string Tipo { get; set; }
    }
}