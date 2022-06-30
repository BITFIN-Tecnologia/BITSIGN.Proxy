// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
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
    public class ResultadoDaAnalise
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
        /// Nome (ou razão social) do proprietário.
        /// </summary>
        public string NomeDoProprietario { get; set; }

        /// <summary>
        /// CNPJ/CPF do proprietário.
        /// </summary>
        public string DocumentoDoProprietario { get; set; }

        /// <summary>
        /// A1, A2, A3 ou A4.
        /// </summary>
        public string Serie { get; set; }

        /// <summary>
        /// Nome completo do certificado.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Entidade emissora.
        /// </summary>
        public string Emissor { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Número de Série.
        /// </summary>
        public string NumeroDeSerie { get; set; }

        /// <summary>
        /// Início da validade.
        /// </summary>
        public DateTime DataDeValidadeInicial { get; set; }

        /// <summary>
        /// Final da validade.
        /// </summary>
        public DateTime DataDeValidadeFinal { get; set; }

        /// <summary>
        /// Versão.
        /// </summary>
        public int Versao { get; set; }

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