// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Valores vigentes do plano contratatado.
    /// </summary>
    public class ValoresVigentes
    {
        /// <summary>
        /// Valor por assinatura/documento.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Valor por item que exceda a quantidade do Plano.
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