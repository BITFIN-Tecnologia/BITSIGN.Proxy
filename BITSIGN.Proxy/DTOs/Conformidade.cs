// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Análise de Conformidade ao padrão PDF/A.
    /// </summary>
    [DebuggerDisplay("{Padrao,nq}")]
    public class Conformidade
    {
        /// <summary>
        /// Nome do arquivo analisado.
        /// </summary>
        public string NomeDoArquivo { get; set; }

        /// <summary>
        /// Padrão de Conformidade.
        /// </summary>
        public string Padrao { get; set; }

        /// <summary>
        /// Indica se o arquivo é compatível com o padrão PDF/A.
        /// </summary>
        public bool Compativel { get; set; }
    }
}