// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Utilitarios
{
    internal static class Estensoes
    {
        internal static async Task<T> ReadAs<T>(this HttpContent content, CancellationToken cancellationToken = default) where T : class =>
            Serializador.Deserializar<T>(await content.ReadAsStringAsync(cancellationToken), content.Headers.ContentType.MediaType, "Lotes");
    }
}