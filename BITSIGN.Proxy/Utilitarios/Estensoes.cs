// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Utilitarios
{
    internal static class Estensoes
    {
        internal static async Task<T> ReadAs<T>(this HttpContent content, CancellationToken cancellationToken = default) where T : class =>
            Serializador.Deserializar<T>(await content.ReadAsStringAsync(cancellationToken), content.Headers.ContentType.MediaType, "Lotes");

        internal static byte[] EmBytes(this string conteudo) => Encoding.UTF8.GetBytes(conteudo);

        internal static string EmBase64(this string conteudo) => Convert.ToBase64String(conteudo.EmBytes());
    }
}