﻿// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
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
    /// Manipulação e informações sobre as notificações e callbacks gerados pelo sistema.
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
        /// Reenvia a notificação.
        /// </summary>
        /// <param name="id">Identificador da Notificação.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        public async Task Replay(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Put, $"notificacoes/{id}/replay"))
            {
                await Executar(requisicao, resposta =>
                {
                    resposta.EnsureSuccessStatusCode();
                }, cancellationToken);
            }
        }
    }
}