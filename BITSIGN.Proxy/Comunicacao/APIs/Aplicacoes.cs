// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao.APIs
{
    /// <summary>
    /// Configurações que guiam o processo de assinaturas, configurações operacionais e integração.
    /// </summary>
    public class Aplicacoes : API
    {
        /// <summary>
        /// Inicializa a API de aplicações.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        /// <param name="formato">Formato para serialização dos objetos.</param>
        public Aplicacoes(HttpClient proxy, FormatoDeSerializacao formato)
            : base(proxy)
        {
            this.FormatoDeSerializacao = formato;
            this.MimeType = $"application/{formato.ToString().ToLower()}";
        }

        /// <summary>
        /// Detalhes da Aplicação.
        /// </summary>
        /// <param name="id">Identificador da Aplicação.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Retorna todas as informações cadastrais e configurações.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<DTOs.Aplicacao> Detalhes(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"aplicacoes/{id}"))
            {
                return await base.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        resposta.EnsureSuccessStatusCode();

                        return await resposta.Content.ReadAs<DTOs.Aplicacao>(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Atualização das Configurações.
        /// </summary>
        /// <param name="aplicacao">Aplicação e suas configurações para atualização.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task Atualizar(DTOs.Aplicacao aplicacao, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Put, $"aplicacoes/{aplicacao.Id}/configuracoes")
            {
                Content = new StringContent(Serializador.Serializar(aplicacao, this.FormatoDeSerializacao.ToString()), Encoding.UTF8, this.MimeType)
            })
            {
                await Executar(requisicao, resposta => resposta.EnsureSuccessStatusCode(), cancellationToken);
            }
        }

        /// <summary>
        /// Renovação da Chave.
        /// </summary>
        /// <param name="id">Identificador da Aplicação.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Renova e retorna a nova chave de integração gerada pelo serviço.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<string> RenovarChave(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Patch, $"aplicacoes/{id}/configuracoes/renovarchave"))
                return await Executar(
                    requisicao,
                    async resposta => await resposta.Content.ReadAsStringAsync(cancellationToken),
                    cancellationToken);
        }

        private FormatoDeSerializacao FormatoDeSerializacao { get; set; }

        private string MimeType { get; init; }
    }
}