// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Detalhe do plano de assinaturas digitais.
    /// </summary>
    [DebuggerDisplay("{Nome,nq} - Faixa Inicial: {FaixaInicial} - Faixa Final: {FaixaFinal}")]
    public class Plano : Base
    {
        /// <summary>
        /// Nome do Plano.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição do Plano.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Base de Cobrança (por assinatura ou por documento).
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// Faixa inicial.
        /// </summary>
        public int FaixaInicial { get; set; }

        /// <summary>
        /// Faixa final.
        /// </summary>
        public int? FaixaFinal { get; set; }

        /// <summary>
        /// Valor por assinatura/documento.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Valor por assinatura/documento excedente.
        /// </summary>
        public decimal ValorExcedente { get; set; }

        /// <summary>
        /// Espaço de armazenamento disponível (em bytes).
        /// </summary>
        public long TamanhoDeArmazenamento { get; set; }

        /// <summary>
        /// Período de duração do armazenamento.
        /// </summary>
        public int AnosDeArmazenamento { get; set; }
    }
}