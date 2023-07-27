// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao
{
    internal class RastreioDaRequisicao : HttpClientHandler
    {
        private const string CodigoDeRastreio = "BS-Tracking";

        private readonly ILogger logger;
        private readonly IRastreio rastreioDeRequisicao;

        public RastreioDaRequisicao(ILogger logger, IRastreio rastreioDeRequisicao)
        {
            this.logger = logger;
            this.rastreioDeRequisicao = rastreioDeRequisicao;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var id = this.rastreioDeRequisicao?.Gerar() ?? Guid.NewGuid().ToString();

            if (this.rastreioDeRequisicao != null)
                request.Headers.Add(CodigoDeRastreio, id);

            Log(id, Severidade.Info, "INÍCIO DO ESCOPO");
            Log(id, Severidade.Info, $"{request.Method} {request.RequestUri}");

            if (request.Content != null)
            {
                Log(id, Severidade.Info, $"Request.Type: {request.Content.GetType().Name}");
                Log(id, Severidade.Info, $"Request.Content-Type: {request.Content.Headers.ContentType?.MediaType}");
            }

            Log(id, Severidade.Info, $"Request.Headers: {string.Join(";", request.Headers.OrderBy(static h => h.Key).Select(static h => $"{h.Key}={string.Join(",", h.Value)}"))}");

            HttpResponseMessage resposta = null;

            try
            {
                resposta = await base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                Log(id, Severidade.Excecao, ex.ToString());
                throw;
            }
            finally
            {
                if (resposta != null)
                {
                    Log(id, Severidade.Info, $"Response.Headers: {string.Join(";", resposta.Headers.OrderBy(static h => h.Key).Select(static h => $"{h.Key}={string.Join(",", h.Value)}"))}");

                    Log(id, Severidade.Info, $"Response.StatusCode: {resposta.StatusCode}");
                    Log(id, Severidade.Info, $"Response.ReasonPhrase: {resposta.ReasonPhrase}");

                    if (resposta.Content != null)
                    {
                        Log(id, Severidade.Info, $"Response.Type: {resposta.Content.GetType().Name}");
                        Log(id, Severidade.Info, $"Response.Content-Type: {resposta.Content.Headers.ContentType?.MediaType}");
                    }
                }
            }

            Log(id, Severidade.Info, "FIM DO ESCOPO");
            this.logger?.QuebrarLinha();

            return resposta;
        }

        private void Log(string id, Severidade severidade, string mensagem) =>
            this.logger?.Escrever(severidade, $"{id} - {mensagem}");
    }
}