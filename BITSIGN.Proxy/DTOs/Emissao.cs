// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Informações sobre a emissão do certificado.
    /// </summary>
    [DebuggerDisplay("{Certificado,nq}")]
    public class Emissao
    {
        /// <summary>
        /// Data em que foi emitido o certificado.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Dados do certificado (final) emitido para as entidades subordinadas.
        /// </summary>
        public Certificado Certificado { get; set; }
    }
}