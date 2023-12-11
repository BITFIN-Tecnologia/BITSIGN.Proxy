// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Diagnostics;

namespace BITSIGN.Proxy.DTOs
{
    /// <summary>
    /// Representa uma falha ocorrida durante a execução do serviço.
    /// </summary>
    [DebuggerDisplay("{Mensagem,nq}")]
    public class Falha
    {
        /// <summary>
        /// Mensagem resumida sobre o problema ocorrido.
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Descreve de forma mais detalhada o motivo da falha.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Opcionalmente informa um endereço com informações complementares sobre a falha.
        /// </summary>
        public string Link { get; set; }
    }
}