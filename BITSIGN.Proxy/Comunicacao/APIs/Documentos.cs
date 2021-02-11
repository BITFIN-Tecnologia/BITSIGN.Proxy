// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Logging;
using BITSIGN.Proxy.Utilitarios;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao.APIs
{
    /// <summary>
    /// Download dos documentos enviados, assinados e manifestos de assinaturas.
    /// </summary>
    public class Documentos : API
    {
        /// <summary>
        /// Inicializa a API de documentos.
        /// </summary>
        /// <param name="proxy">Instânca da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        /// <param name="logger">Implementação da interface <see cref="ILogger"/> para gestão e armazenamento de logs gerados pelo proxy.</param>
        /// <param name="rastreioDeRequisicao">Gerador de códigos de rastreio de requisições.</param>
        public Documentos(HttpClient proxy, ILogger logger, IGeradorDeRastreio rastreioDeRequisicao)
            : base(proxy, logger, rastreioDeRequisicao) { }

        /// <summary>
        /// Detalhes do Documento.
        /// </summary>
        /// <param name="id">Identificador do Documento.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Retorna todas as informações do documento que foi submetido para assinatura.</returns>
        public async Task<DTOs.Documento> Detalhes(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"documentos/{id}"))
            {
                return await base.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        resposta.EnsureSuccessStatusCode();

                        return await resposta.Content.ReadAs<DTOs.Documento>(cancellationToken);
                    }
                    catch (HttpRequestException ex)
                    {
                        if (ex.StatusCode == HttpStatusCode.NotFound)
                            return null;

                        throw;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Documento Original.
        /// </summary>
        /// <param name="id">Identificador do Documento.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Um <c>array</c> de <see cref="byte"/>s que corresponde ao documento original enviado.</returns>
        public async Task<byte[]> Original(Guid id, CancellationToken cancellationToken = default) =>
            await Executar(id, "original", cancellationToken);

        /// <summary>
        /// Documento Assinado.
        /// </summary>
        /// <param name="id">Identificador do Documento.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Um <c>array</c> de <see cref="byte"/>s que corresponde ao documento assinado.</returns>
        /// <remarks>Este arquivo é aquele que possui validade jurídica.</remarks>
        public async Task<byte[]> Assinado(Guid id, CancellationToken cancellationToken = default) =>
            await Executar(id, "assinado", cancellationToken);

        /// <summary>
        /// Manifesto de Assinaturas.
        /// </summary>
        /// <param name="id">Identificador do Documento.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Um <c>array</c> de <see cref="byte"/>s que corresponde à um arquivo PDF com os detalhes dos assinantes e as características das assinaturas.</returns>
        public async Task<byte[]> Manifesto(Guid id, CancellationToken cancellationToken = default) =>
            await Executar(id, "manifesto", cancellationToken);

        private async Task<byte[]> Executar(Guid id, string path, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"documentos/{id}/{path}"))
            {
                return await base.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        resposta.EnsureSuccessStatusCode();

                        return await resposta.Content.ReadAsByteArrayAsync(cancellationToken);
                    }
                    catch (HttpRequestException ex)
                    {
                        if (ex.StatusCode == HttpStatusCode.NotFound)
                            return null;

                        throw;
                    }
                }, cancellationToken);
            }
        }
    }
}