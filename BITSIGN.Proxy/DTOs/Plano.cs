// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Detalhe do plano de assinaturas digitais.
    /// </summary>
    [DebuggerDisplay("{Nome,nq} - {Quantidade} {Base,nq}(s)")]
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
        /// Quantidade máxima do Plano.
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Valor por assinatura/documento.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Valor por assinatura/documento excedente.
        /// </summary>
        public decimal ValorExcedente { get; set; }

        /// <summary>
        /// Valor mínimo do Plano.
        /// </summary>
        public decimal ValorMinimo { get; set; }

        /// <summary>
        /// Valor cobrado pelo armazenamento (por gibabytes).
        /// </summary>
        public decimal ValorDoArmazenamento { get; set; }

        /// <summary>
        /// Valor cobrado por carimbo do tempo emitido.
        /// </summary>
        public decimal ValorDoCarimboDoTempo { get; set; }
    }
}