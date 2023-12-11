// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Xml.Serialization;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Classe com recursos comuns para os DTOs.
    /// </summary>
    public abstract class Base
    {
        /// <summary>
        /// Identificador único para o objeto.
        /// </summary>
        [XmlAttribute]
        public virtual Guid Id { get; set; }
    }
}