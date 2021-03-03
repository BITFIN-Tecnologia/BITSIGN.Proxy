// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Informações do plano contratado.
    /// </summary>
    public class PlanoContratado
    {
        /// <summary>
        /// Plano selecionado na contratação.
        /// </summary>
        public Plano Plano { get; set; }

        /// <summary>
        /// Data de contratação.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Dia estipulado para pagamento das faturas.
        /// </summary>
        public int DiaParaPagamento { get; set; }

        /// <summary>
        /// Indica o status atual da contratação.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Total atual de armazenamento utilizado (em bytes).
        /// </summary>
        public long TotalDeArmazenamento { get; set; }
    }
}