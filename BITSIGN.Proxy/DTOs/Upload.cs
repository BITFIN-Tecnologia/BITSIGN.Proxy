// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Informações sobre o arquivo armazenado.
    /// </summary>
    [DebuggerDisplay("{NomeDoArquivo,nq}")]
    public class Upload : Base
    {
        /// <summary>
        /// Nome do arquivo.
        /// </summary>
        public string NomeDoArquivo { get; set; }

        /// <summary>
        /// Formato do arquivo.
        /// </summary>
        public string FormatoDoArquivo { get; set; }

        /// <summary>
        /// Data do envio do arquivo.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Data limite em que o arquivo estará disponível.
        /// </summary>
        public DateTime DataDeExpiracao { get; set; }

        /// <summary>
        /// Local onde o arquivo está disponível para download.
        /// </summary>
        public string Url { get; set; }
    }
}