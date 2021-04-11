// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
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
    /// Informações cadastrais e configurações sobre o processo operacional das assinaturas digitais, callbacks, etc.
    /// </summary>
    public class Contratantes : API
    {
        /// <summary>
        /// Inicializa a API de configurações.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        /// <param name="formato">Formato para serialização dos objetos.</param>
        public Contratantes(HttpClient proxy, FormatoDeSerializacao formato)
            : base(proxy)
        {
            this.FormatoDeSerializacao = formato;
        }

        /// <summary>
        /// Detalhes do Contratante.
        /// </summary>
        /// <param name="id">Identificador do Contratante.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Retorna todas as informações cadastrais e configurações.</returns>
        public async Task<DTOs.Contratante> Detalhes(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"contratantes/{id}"))
            {
                return await base.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        resposta.EnsureSuccessStatusCode();

                        return await resposta.Content.ReadAs<DTOs.Contratante>(cancellationToken);
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
        /// Atualização de configurações.
        /// </summary>
        /// <param name="contratante">Contratante e suas configurações para atualização.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        public async Task Atualizar(DTOs.Contratante contratante, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Put, $"contratantes/{contratante.Id}/configuracoes")
            {
                Content = new StringContent(Serializador.Serializar(contratante.Configuracao, this.FormatoDeSerializacao.ToString()))
            })
            {
                await Executar(requisicao, resposta => resposta.EnsureSuccessStatusCode(), cancellationToken);
            }
        }

        /// <summary>
        /// Renova a chave de integração entre sistemas.
        /// </summary>
        /// <param name="id">Identificador do contratante.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Retornará a nova chave de integração gerada pelo serviço.</returns>
        public async Task<string> RenovarChave(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Patch, $"contratantes/{id}/configuracoes/renovarchave"))
                return await Executar(
                    requisicao,
                    async resposta => await resposta.Content.ReadAsStringAsync(cancellationToken),
                    cancellationToken);
        }

        private FormatoDeSerializacao FormatoDeSerializacao { get; set; }
    }
}