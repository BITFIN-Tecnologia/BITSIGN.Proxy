// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao.APIs
{
    /// <summary>
    /// Análise da situação dos serviços e recursos.
    /// </summary>
    public class Status
    {
        private readonly Uri url;

        /// <summary>
        /// Inicializa o monitoramento de status dos serviços.
        /// </summary>
        /// <param name="url">Endereço onde está localizado o relatório de status.</param>
        public Status(Uri url) =>
            this.url = url;

        /// <summary>
        /// Atualiza o status dos serviços.
        /// </summary>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Relatório com o status atual dos serviços e recursos.</returns>
        public async Task<DTOs.StatusDosServicos> Atualizar(CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
                using (var requisicao = new HttpRequestMessage(HttpMethod.Get, this.url))
                    using (var resposta = await client.SendAsync(requisicao, cancellationToken))
                        return await resposta.Content.ReadAs<DTOs.StatusDosServicos>(cancellationToken);
        }
    }
}