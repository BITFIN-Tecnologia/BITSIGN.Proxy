// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        /// <param name="formato">Formato para serialização dos objetos.</param>
        internal Lotes(HttpClient proxy, FormatoDeSerializacao formato)
            : base(proxy, formato) { }

        /// <summary>
        /// Upload de Documentos.
        /// </summary>
        /// <remarks>Endpoint para o envio do lote de documentos para assinatura. O conteúdo deve ser um arquivo compactado contendo o arquivo <b>manifesto.xml</b> e todos os arquivos mencionados dentro dele que deverão ser assinados digitalmente. Para maiores informações da estrutura deste arquivo e do processo, consulte <see href="https://bitsign.com.br/documentacao#integracaoPacotes">este link</see>. O tamanho do <i>payload</i> não poderá ultrapassar <b>20MB</b>.</remarks>
        /// <param name="pacote">O pacote contendo o lote e os respectivos documentos que devem ser encaminhados para assinatura.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>URI onde estará disponível o lote recém criado para consulta.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<Uri> Upload(DTOs.Pacote pacote, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, "lotes")
            {
                Content = new ByteArrayContent(pacote.Serializar())
            })
            {
                return await this.Executar(requisicao, resposta => Task.FromResult(resposta.Headers.Location), cancellationToken);
            }
        }

        /// <summary>
        /// Detalhes do Lote.
        /// </summary>
        /// <param name="id">Identificador do Lote.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Retorna todas as informações do lote que foi submetido para o serviço.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<DTOs.Lote> Detalhes(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"lotes/{id}"))
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return await resposta.Content.ReadAs<DTOs.Lote>(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Alteração do Lote.
        /// </summary>
        /// <remarks>Para aqueles assinantes que ainda estejam pendentes de assinatura, receberão uma nova notificação.</remarks>
        /// <param name="lote">Objeto contendo o lote e suas informações para alteração.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Se a alteração for realizada com sucesso, retornará <c>true</c>, caso contrário, <c>false</c>.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<bool> Alterar(DTOs.Lote lote, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Put, $"lote/{lote.Id}")
            {
                Content = new StringContent(Serializador.Serializar(lote, this.FormatoDeSerializacao.ToString()), Encoding.UTF8, this.MimeType)
            })
            {
                return await this.Executar(requisicao, resposta =>
                {
                    try
                    {
                        return Task.FromResult(resposta.IsSuccessStatusCode);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Exclusão do Lote.
        /// </summary>
        /// <remarks>A exclusão incidirá a cobrança das assinaturas que já foram, eventualmente, realizadas.</remarks>
        /// <param name="id">Identificador do Lote.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Se a exclusão for realizada com sucesso, retornará <c>true</c>, caso contrário, <c>false</c>.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
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
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return Task.FromResult(false);
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
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<IEnumerable<DTOs.Notificacao>> Notificacoes(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"lotes/{id}/notificacoes"))
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return await resposta.Content.ReadAs<IEnumerable<DTOs.Notificacao>>(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Observadores do Lote.
        /// </summary>
        /// <param name="id">Identificador do Lote.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Coleção com os observadores adicionados no lote.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<IEnumerable<DTOs.Observador>> Observadores(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"lotes/{id}/observadores"))
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

        /// <summary>
        /// Anexos do Lote.
        /// </summary>
        /// <param name="id">Identificador do Lote.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Coleção com os arquivos anexados ao lote.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<IEnumerable<DTOs.Anexo>> Anexos(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"lotes/{id}/anexos"))
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return await resposta.Content.ReadAs<IEnumerable<DTOs.Anexo>>(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
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
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<DTOs.Pacote> Pacote(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"lotes/{id}/pacote"))
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return new DTOs.Pacote(await resposta.Content.ReadAsByteArrayAsync(cancellationToken));
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