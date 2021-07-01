// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Dados da nota fiscal emitida referente aos serviços prestados de um mês/ano.
    /// </summary>
    [DebuggerDisplay("Valor: {Valor} - Número: {Numero,nq}")]
    public class NotaFiscal : Base
    {
        /// <summary>
        /// Data de geração da nota fiscal.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Data da emissão/cancelamento (status) da nota fiscal.
        /// </summary>
        public DateTime DataDoStatus { get; set; }

        /// <summary>
        /// Status da nota fiscal (Gerada, Emitida ou Cancelada).
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Valor total da nota fiscal.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Número atribuído à nota fiscal.
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// Código de verificação para validar a autenticidade da nota fiscal junto a prefeitura.
        /// </summary>
        public string CodigoDeVerificacao { get; set; }

        /// <summary>
        /// Link para visualização e validação da nota fiscal emitida.
        /// </summary>
        public string Link { get; set; }
    }
}