// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao
{
    /// <summary>
    /// Classe base que define os recursos necessários para comunicação com a API remota.
    /// </summary>
    public abstract class API
    {
        /// <summary>
        /// Proxy HTTP pré-configurado para comunicação com o serviço.
        /// </summary>
        protected readonly HttpClient proxy;

        /// <summary>
        /// Inicializa a API.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        public API(HttpClient proxy) =>
            this.proxy = proxy;

        /// <summary>
        /// Configura, envia e captura o retorno da requisição para um determinado serviço.
        /// </summary>
        /// <param name="requisicao">Mensagem de requisição para o serviço.</param>
        /// <param name="analiseDeRetorno">Função que analisa o retorno.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        protected virtual async Task Executar(HttpRequestMessage requisicao, Action<HttpResponseMessage> analiseDeRetorno, CancellationToken cancellationToken = default)
        {
            using (var resposta = await this.proxy.SendAsync(requisicao, cancellationToken))
            {
                try
                {
                    analiseDeRetorno(resposta);
                }
                catch (HttpRequestException ex)
                {
                    throw new ErroNaRequisicao(ex, await resposta.Content.ReadAs<DTOs.Falha>(cancellationToken));
                }
            }
        }

        /// <summary>
        /// Configura, envia e captura o retorno da requisição para um determinado serviço.
        /// </summary>
        /// <typeparam name="T">Especifica em tipo o conteúdo de retorno será deserializado.</typeparam>
        /// <param name="requisicao">Mensagem de requisição para o serviço.</param>
        /// <param name="analiseDeRetorno">Função que analisa o retorno e constrói o objeto do tipo <typeparamref name="T"/></param>.
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        /// <returns>Retorna o objeto do tipo <typeparamref name="T"/> pronto para utilização.</returns>
        protected virtual async Task<T> Executar<T>(HttpRequestMessage requisicao, Func<HttpResponseMessage, Task<T>> analiseDeRetorno, CancellationToken cancellationToken = default)
        {
            using (var resposta = await this.proxy.SendAsync(requisicao, cancellationToken))
            {
                try
                {
                    return await analiseDeRetorno(resposta);
                }
                catch (HttpRequestException ex)
                {
                    throw new ErroNaRequisicao(ex, await resposta.Content.ReadAs<DTOs.Falha>(cancellationToken));
                }
            }
        }
    }
}