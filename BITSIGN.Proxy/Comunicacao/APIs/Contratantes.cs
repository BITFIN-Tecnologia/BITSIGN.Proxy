﻿// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

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
    /// Informações cadastrais e configurações do processo operacional das assinaturas.
    /// </summary>
    public class Contratantes : API
    {
        /// <summary>
        /// Inicializa a API de contratantes.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        internal Contratantes(HttpClient proxy)
            : base(proxy) { }

        /// <summary>
        /// Detalhes do Contratante.
        /// </summary>
        /// <param name="id">Identificador do Contratante.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Retorna todas as informações cadastrais e configurações.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<DTOs.Contratante> Detalhes(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"contratantes/{id}"))
            {
                return await base.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return await resposta.Content.ReadAs<DTOs.Contratante>(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Aplicações do Contratante.
        /// </summary>
        /// <param name="id">Identificador do Contratante.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Relação de Aplicações criadas pelo contratante.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<IEnumerable<DTOs.Aplicacao>> Aplicacoes(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"contratantes/{id}/aplicacoes"))
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return await resposta.Content.ReadAs<IEnumerable<DTOs.Aplicacao>>(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Organizações do Contratante.
        /// </summary>
        /// <param name="id">Identificador do Contratante.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Relação de Organizações criadas pelo contratante.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<IEnumerable<DTOs.Organizacao>> Organizacoes(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"contratantes/{id}/organizacoes"))
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return await resposta.Content.ReadAs<IEnumerable<DTOs.Organizacao>>(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Observadores do Contratante.
        /// </summary>
        /// <param name="id">Identificador do Contratante.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Relação de Observadores globais criados pelo contratante.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<IEnumerable<DTOs.Observador>> Observadores(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"contratantes/{id}/observadores"))
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return await resposta.Content.ReadAs<IEnumerable<DTOs.Observador>>(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }
    }
}