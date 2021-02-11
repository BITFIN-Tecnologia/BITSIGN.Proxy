﻿// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Logging;
using BITSIGN.Proxy.Utilitarios;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao.APIs
{
    /// <summary>
    /// Envio e gestão dos lotes de documentos para assinatura digital.
    /// </summary>
    public class Lotes : API
    {
        /// <summary>
        /// Inicializa a API de lotes.
        /// </summary>
        /// <param name="proxy">Instânca da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        /// <param name="logger">Implementação da interface <see cref="ILogger"/> para gestão e armazenamento de logs gerados pelo proxy.</param>
        /// <param name="rastreioDeRequisicao">Gerador de códigos de rastreio de requisições.</param>
        public Lotes(HttpClient proxy, ILogger logger, IGeradorDeRastreio rastreioDeRequisicao)
            : base(proxy, logger, rastreioDeRequisicao) { }

        /// <summary>
        /// Upload de Documentos.
        /// </summary>
        /// <param name="pacote">O pacote contendo o lote e os respectivos documentos que devem ser encaminhados para assinatura.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        public async Task<Uri> Upload(DTOs.Pacote pacote, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, "lotes")
            {
                Content = new ByteArrayContent(Compactador.Compactar(pacote.Arquivos))
            })
            {
                 return await this.Executar(requisicao, async resposta =>
                 {
                     resposta.EnsureSuccessStatusCode();

                     pacote.Lote = await resposta.Content.ReadAs<DTOs.Lote>(cancellationToken);

                     return resposta.Headers.Location;
                 }, cancellationToken);
            }
        }

        /// <summary>
        /// Detalhes do Lote.
        /// </summary>
        /// <param name="id">Identificador do Lote.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Retorna todas as informações do lote que foi submetido para o serviço.</returns>
        public async Task<DTOs.Lote> Detalhes(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"lotes/{id}"))
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        resposta.EnsureSuccessStatusCode();

                        return await resposta.Content.ReadAs<DTOs.Lote>(cancellationToken);
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
        /// Exclusão do Lote.
        /// </summary>
        /// <param name="id">Identificador do Lote.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        public async Task<bool> Excluir(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Delete, $"lotes/{id}"))
            {
                return await this.Executar(requisicao, resposta =>
                {
                    try
                    {
                        return Task.FromResult(resposta.IsSuccessStatusCode);
                    }
                    catch (HttpRequestException ex)
                    {
                        if (ex.StatusCode == HttpStatusCode.NotFound)
                            return Task.FromResult(false);

                        throw;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Notificações do Lote.
        /// </summary>
        /// <param name="id">Identificador do Lote.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Coleção com as notificações enviadas à todos os assinantes envolvidos no lote.</returns>
        public async Task<IEnumerable<DTOs.Notificacao>> Notificacoes(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"lotes/{id}/notificacoes"))
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        resposta.EnsureSuccessStatusCode();

                        return await resposta.Content.ReadAs<IEnumerable<DTOs.Notificacao>>(cancellationToken);
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
        /// Download do Pacote.
        /// </summary>
        /// <param name="id">Identificador do Lote.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Um objeto que representa o lote enviado com seus documentos e os arquivos associados.</returns>
        public async Task<DTOs.Pacote> Pacote(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"lotes/{id}"))
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        resposta.EnsureSuccessStatusCode();

                        return new DTOs.Pacote(await resposta.Content.ReadAsByteArrayAsync(cancellationToken));
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