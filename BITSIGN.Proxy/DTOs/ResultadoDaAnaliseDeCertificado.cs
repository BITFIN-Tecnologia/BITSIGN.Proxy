// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Resultado da análise realizada sobre o certificado.
    /// </summary>
    [DebuggerDisplay("{Thumbprint,nq}")]
    public class ResultadoDaAnaliseDeCertificado
    {
        /// <summary>
        /// Data da análise.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Data de referência para validação da expiração.
        /// </summary>
        public DateTime DataDeReferencia { get; set; }

        /// <summary>
        /// Informações sobre o certificado digital.
        /// </summary>
        public Certificado Certificado { get; set; }

        /// <summary>
        /// Indica se atende as exigências para realização da assinatura digital.
        /// </summary>
        public bool Valido { get; set; }

        /// <summary>
        /// Mensagens geradas pela análise.
        /// </summary>
        public IEnumerable<string> Mensagens { get; set; }
    }
}