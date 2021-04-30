// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao.APIs
{
    /// <summary>
    /// Informações e manipulação de notificações e callbacks.
    /// </summary>
    public class Notificacoes : API
    {
        /// <summary>
        /// Inicializa a API de notificações.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        public Notificacoes(HttpClient proxy)
            : base(proxy) { }

        /// <summary>
        /// Detalhes da Notificação.
        /// </summary>
        /// <param name="id">Identificador da Notificação.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Retorna as informações sobre a notificação e seu status de envio.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<DTOs.Notificacao> Detalhes(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"notificacoes/{id}"))
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        resposta.EnsureSuccessStatusCode();

                        return await resposta.Content.ReadAs<DTOs.Notificacao>(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Reenvia a notificação.
        /// </summary>
        /// <param name="id">Identificador da Notificação.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task Replay(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Patch, $"notificacoes/{id}/replay"))
                await Executar(requisicao, resposta => resposta.EnsureSuccessStatusCode(), cancellationToken);
        }
    }
}