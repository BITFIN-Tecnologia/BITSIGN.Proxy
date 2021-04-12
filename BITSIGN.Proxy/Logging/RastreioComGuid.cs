// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;

namespace BITSIGN.Proxy.Logging
{
    /// <summary>
    /// Gerador de código de rastreio com base no <see cref="Guid"/>.
    /// </summary>
    public sealed class RastreioComGuid : IRastreio
    {
        /// <summary>
        /// Ao gerar, o método <see cref="Guid.NewGuid()"/> é chamado.
        /// </summary>
        /// <returns>Retorna um <see cref="Guid"/> em formato <c>string</c>.</returns>
        public string Gerar() => Guid.NewGuid().ToString();
    }
}