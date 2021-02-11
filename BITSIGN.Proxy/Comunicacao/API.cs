// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

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
        private readonly IGeradorDeRastreio rastreioDeRequisicao;
        private readonly Escopo logger;

        /// <summary>
        /// Inicializa a API.
        /// </summary>
        /// <param name="proxy">Instânca da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        /// <param name="logger">Implementação da interface <see cref="ILogger"/> para gestão e armazenamento de logs gerados pelo proxy.</param>
        /// <param name="rastreioDeRequisicao">Gerador de códigos de rastreio de requisições.</param>
        public API(HttpClient proxy, ILogger logger, IGeradorDeRastreio rastreioDeRequisicao)
        {
            this.proxy = proxy;
            this.logger = new Escopo(logger);
            this.rastreioDeRequisicao = rastreioDeRequisicao;
        }

        private void PreExecucao(HttpRequestMessage requisicao)
        {
            this.logger.Escrever(Severidade.Info, $"Ambiente: {this.Ambiente} - {requisicao.Method} {requisicao.RequestUri}");

            var codigoDeRastreio = this.rastreioDeRequisicao?.Gerar();

            if (!string.IsNullOrWhiteSpace(codigoDeRastreio))
            {
                requisicao.Headers.Add(Protocolo.CodigoDeRastreio, this.rastreioDeRequisicao.Gerar());
                this.logger.Escrever(Severidade.Info, $"Request.{Protocolo.CodigoDeRastreio}: {codigoDeRastreio}");
            }

            if (requisicao.Content != null)
            {
                this.logger.Escrever(Severidade.Info, $"Request.Type: {requisicao.Content.GetType().Name}");
                this.logger.Escrever(Severidade.Info, $"Request.Content-Type: {requisicao.Content.Headers.ContentType}");
            }
        }

        /// <summary>
        /// Configura, envia e captura o retorno da requisição para um determinado serviço.
        /// </summary>
        /// <param name="requisicao">Mensagem de requisição para o serviço.</param>
        /// <param name="analiseDeRetorno">Função que analisa o retorno.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        protected virtual async Task Executar(HttpRequestMessage requisicao, Action<HttpResponseMessage> analiseDeRetorno, CancellationToken cancellationToken = default)
        {
            using (this.logger.Iniciar())
            {
                PreExecucao(requisicao);

                using (var resposta = await this.proxy.SendAsync(requisicao, cancellationToken))
                {
                    this.logger.Escrever(Severidade.Info, $"Request.Headers: {string.Join(";", requisicao.Headers.Select(h => $"{h.Key}={string.Join(",", h.Value)}"))}");

                    try
                    {
                        analiseDeRetorno(resposta);
                    }
                    catch (Exception ex)
                    {
                        this.logger.Escrever(ex);
                        throw;
                    }
                    finally
                    {
                        PosExecucao(resposta);
                    }
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
        /// <returns>Retorna o objeto do tipo <typeparamref name="T"/> pronto para utilização.</returns>
        protected virtual async Task<T> Executar<T>(HttpRequestMessage requisicao, Func<HttpResponseMessage, Task<T>> analiseDeRetorno, CancellationToken cancellationToken = default)
        {
            using (this.logger.Iniciar())
            {
                PreExecucao(requisicao);

                using (var resposta = await this.proxy.SendAsync(requisicao, cancellationToken))
                {
                    try
                    {
                        this.logger.Escrever(Severidade.Info, $"Request.Headers: {string.Join(";", requisicao.Headers.Select(h => $"{h.Key}={string.Join(",", h.Value)}"))}");

                        return await analiseDeRetorno(resposta);
                    }
                    catch (Exception ex)
                    {
                        this.logger.Escrever(ex);
                        throw;
                    }
                    finally
                    {
                        PosExecucao(resposta);
                    }
                }
            }
        }

        private void PosExecucao(HttpResponseMessage resposta)
        {
            if (resposta != null)
            {
                this.logger.Escrever(Severidade.Info, $"Response.Headers: {string.Join(";", resposta.Headers.Select(h => $"{h.Key}={string.Join(",", h.Value)}"))}");

                this.logger.Escrever(Severidade.Info, $"Response.StatusCode: {resposta.StatusCode}");
                this.logger.Escrever(Severidade.Info, $"Response.ReasonPhrase: {resposta.ReasonPhrase}");

                if (resposta.Content != null)
                {
                    this.logger.Escrever(Severidade.Info, $"Response.Type: {resposta.Content.GetType().Name}");
                    this.logger.Escrever(Severidade.Info, $"Response.Content-Type: {resposta.Content.Headers.ContentType}");
                }
            }
        }

        internal Ambiente Ambiente { get; init; }
}
}