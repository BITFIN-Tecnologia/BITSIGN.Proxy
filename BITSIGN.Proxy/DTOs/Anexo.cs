// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Arquivo anexo ao lote de assinaturas.
    /// </summary>
    [DebuggerDisplay("{NomeDoArquivo,nq}")]
    public class Anexo : Base
    {
        /// <summary>
        /// Nome do arquivo anexo.
        /// </summary>
        public string NomeDoArquivo { get; set; }

        /// <summary>
        /// Breve descrição da finalidade do anexo.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Conteúdo do arquivo anexo.
        /// </summary>
        public byte[] Conteudo { get; set; }
    }
}