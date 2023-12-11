// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
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
    /// Repositório de requisições geradas em ambiente de <see cref="Ambiente.Sandbox"/>.
    /// </summary>
    public class Dumps : API
    {
        private readonly Ambiente ambiente;

        /// <summary>
        /// Repositório de requisições geradas em ambiente de SANDBOX.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        /// <param name="ambiente">Ambiente conectado.</param>
        internal Dumps(HttpClient proxy, Ambiente ambiente)
            : base(proxy)
        {
            this.ambiente = ambiente;
        }

        /// <summary>
        /// Relação de Requisições.
        /// </summary>
        /// <param name="id">Id do repositório, gerado no momento da criação.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Últimos 100 dumps gerados, incluindo suas características.</returns>
        /// <exception cref="InvalidOperationException">Exceção disparada se não estiver em ambiente de <see cref="Ambiente.Sandbox"/>.</exception>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<IEnumerable<DTOs.Dump>> Relacao(string id, CancellationToken cancellationToken = default)
        {
            AvaliarAmbiente();

            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"dumps/{id}"))
                return await Executar(
                    requisicao,
                    async resposta => await resposta.Content.ReadAs<IEnumerable<DTOs.Dump>>(cancellationToken),
                    cancellationToken);
        }

        /// <summary>
        /// Conteúdo da Requisição.
        /// </summary>
        /// <param name="id">Id do repositório, gerado no momento da criação.</param>
        /// <param name="requisicaoId">Identificador da requisição gerada.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Uma <see cref="string"/> representando o conteúdo armazenado.</returns>
        /// <exception cref="InvalidOperationException">Exceção disparada se não estiver em ambiente de <see cref="Ambiente.Sandbox"/>.</exception>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<string> Conteudo(string id, Guid requisicaoId, CancellationToken cancellationToken = default)
        {
            AvaliarAmbiente();

            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"dumps/{id}/{requisicaoId}"))
            {
                return await base.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return await resposta.Content.ReadAsStringAsync(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }

        private void AvaliarAmbiente()
        {
            if (this.ambiente != Ambiente.Sandbox)
                throw new InvalidOperationException("Este serviço/API só está acessível em ambiente de SANDBOX.");
        }
    }
}