// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Perfil de assinatura.
    /// </summary>
    [DebuggerDisplay("{Perfil,nq}")]
    public class Assinatura : Base
    {
        /// <summary>
        /// Nome do perfil de assinatura.
        /// </summary>
        [XmlAttribute]
        public string Perfil { get; set; }

        /// <summary>
        /// Quantidade mínima necessária de assinantes para este perfil.
        /// </summary>
        [XmlAttribute]
        public int QtdeMinima { get; set; }

        /// <summary>
        /// Indica a ordem em que a coleta das respectivas assinaturas devem ser realizadas sobre o documento.
        /// </summary>
        [XmlAttribute]
        public int Ordem { get; set; }

        /// <summary>
        /// Indica o status atual da assinatura.
        /// </summary>
        [XmlAttribute]
        public string Status { get; set; }

        /// <summary>
        /// Data em que o status foi definido.
        /// </summary>
        [XmlAttribute]
        public DateTime DataDoStatus { get; set; }

        /// <summary>
        /// Coleção de assinantes associados à assinatura.
        /// </summary>
        public List<Assinante> Assinantes { get; set; }
    }
}