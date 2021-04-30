// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
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
    /// Detalhamento dos planos contratados, fechamentos e faturas do contratante.
    /// </summary>
    public class Financeiro : API
    {
        /// <summary>
        /// Inicializa a API financeira.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        public Financeiro(HttpClient proxy)
            : base(proxy) { }

        /// <summary>
        /// Planos Contratados.
        /// </summary>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Coleção contendo os planos contratados, suas respectivas características e o status atual de cada um deles.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<IEnumerable<DTOs.PlanoContratado>> Planos(CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, "financeiro/planos"))
                return await Executar(
                    requisicao, 
                    async resposta => await resposta.Content.ReadAs<IEnumerable<DTOs.PlanoContratado>>(cancellationToken),
                    cancellationToken);
        }

        /// <summary>
        /// Fechamentos Mensais.
        /// </summary>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Coleção contendo a versão resumida dos fechamentos dos últimos 12 meses.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<IEnumerable<DTOs.Fechamento>> Fechamentos(CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, "financeiro/fechamentos"))
                return await Executar(
                    requisicao, 
                    async resposta => await resposta.Content.ReadAs<IEnumerable<DTOs.Fechamento>>(cancellationToken),
                    cancellationToken);
        }

        /// <summary>
        /// Fechamento Mensal.
        /// </summary>
        /// <param name="id">Identificador do Fechamento.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Detalhamento completo do fechamento de um ano/mês específico, incluindo os dados de pagamento.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<DTOs.Fechamento> Fechamento(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"financeiro/fechamentos/{id}"))
            {
                return await Executar(requisicao, async resposta =>
                {
                    try
                    {
                        resposta.EnsureSuccessStatusCode();

                        return await resposta.Content.ReadAs<DTOs.Fechamento>(cancellationToken);
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