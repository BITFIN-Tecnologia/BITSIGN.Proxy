// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao.APIs
{
    /// <summary>
    /// Envio e gestão dos lotes de documentos para assinatura.
    /// </summary>
    public class Lotes : API
    {
        private static readonly MediaTypeHeaderValue Zip = new("application/zip");

        /// <summary>
        /// Inicializa a API de lotes.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        /// <param name="formato">Formato para serialização dos objetos.</param>
        internal Lotes(HttpClient proxy, FormatoDeSerializacao formato)
            : base(proxy, formato) { }

        /// <summary>
        /// Upload de Lote.
        /// </summary>
        /// <remarks>Utilize esta opção quando todos os documentos a serem assinados estão disponíveis através de HTTP[S], que deverão estar informados na propriedade <see cref="DTOs.Documento.Download"/>.</remarks>
        /// <param name="lote">Lote contendo os documentos que devem ser encaminhados para assinatura.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns><see cref="Tuple{T1, T2}"/> informando a URL onde o lote criado está acessível e o Id do mesmo.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<(Uri Url, Guid Id)> Upload(DTOs.Lote lote, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, "lotes")
            {
                Content = new StringContent(Serializador.Serializar(lote, this.FormatoDeSerializacao, "Lote"))
            })
            {
                requisicao.Content.Headers.ContentType = this.MimeType;

                return await this.Executar(requisicao, async resposta =>
                {
                    var info = Serializador.Deserializar<Dictionary<string, string>>(await resposta.Content.ReadAsStringAsync(cancellationToken), this.FormatoDeSerializacao.ToString());

                    return (resposta.Headers.Location, Guid.Parse(info["id"]));
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Upload de Pacote.
        /// </summary>
        /// <remarks>Endpoint para o envio do lote de documentos para assinatura, representado pela instância da classe <see cref="DTOs.Pacote"/>. Para maiores informações da estrutura deste arquivo e do processo, consulte <see href="https://bitsign.com.br/documentacao#integracaoPacotes">este link</see>. O tamanho do conteúdo não poderá ultrapassar 20MB.</remarks>
        /// <param name="pacote">Pacote contendo o lote e os respectivos documentos que devem ser encaminhados para assinatura.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns><see cref="Tuple{T1, T2}"/> informando a URL onde o lote criado está acessível e o Id do mesmo.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<(Uri Url, Guid Id)> Upload(DTOs.Pacote pacote, CancellationToken cancellationToken = default) =>
            await Upload(pacote.Serializar(), cancellationToken);

        /// <summary>
        /// Upload de Pacote.
        /// </summary>
        /// <remarks>Endpoint para o envio do lote de documentos para assinatura. O conteúdo deve ser um arquivo compactado contendo o arquivo de manifesto e todos os arquivos mencionados dentro dele que deverão ser assinados digitalmente. Para maiores informações da estrutura deste arquivo e do processo, consulte <see href="https://bitsign.com.br/documentacao#integracaoPacotes">este link</see>. O tamanho do arquivo não poderá ultrapassar 20MB.</remarks>
        /// <param name="pacote">Arquivo em formato ZIP contendo o pacote.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns><see cref="Tuple{T1, T2}"/> informando a URL onde o lote criado está acessível e o Id do mesmo.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<(Uri Url, Guid Id)> Upload(byte[] pacote, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, "lotes")
            {
                Content = new ByteArrayContent(pacote)
            })
            {
                requisicao.Content.Headers.ContentType = Zip;

                return await this.Executar(requisicao, async resposta =>
                {
                    var info = Serializador.Deserializar<Dictionary<string, string>>(await resposta.Content.ReadAsStringAsync(cancellationToken), this.FormatoDeSerializacao.ToString());

                    return (resposta.Headers.Location, Guid.Parse(info["id"]));
                }, cancellationToken);
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
        /// <remarks>Possibilita a alteração de informações de um determinado lote.</remarks>
        /// <param name="lote">Objeto contendo o lote e suas informações para alteração.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Se a alteração for realizada com sucesso, retornará <c>true</c>, caso contrário, <c>false</c>.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<bool> Alterar(DTOs.Lote lote, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Put, $"lotes/{lote.Id}")
            {
                Content = new StringContent(Serializador.Serializar(lote, this.FormatoDeSerializacao), Encoding.UTF8, this.MimeType.MediaType)
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
        /// Reabertura do Lote.
        /// </summary>
        /// <remarks>Reabre o lote para a coleta das assinaturas faltantes. Assinantes que estejam pendentes serão notificados.</remarks>
        /// <param name="id">Identificador do Lote.</param>
        /// <param name="dataDeExpiracao">Nova data de expiração do lote.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Se a reabertura for realizada com sucesso, retornará <c>true</c>, caso contrário, <c>false</c>.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<bool> Reabrir(Guid id, DateTime dataDeExpiracao, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Patch, $"lotes/{id}/reabrir")
            {
                Content = new StringContent(Serializador.Serializar(new DTOs.Lote() { Id = id, DataDeExpiracao = dataDeExpiracao }, this.FormatoDeSerializacao), Encoding.UTF8, this.MimeType.MediaType)
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
        /// Notificação de Assinantes
        /// </summary>
        /// <remarks>Cria e enfileira a notificação para os assinantes que ainda estão pendentes de assinatura em um determinado lote.</remarks>
        /// <param name="id">Identificador do Lote.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Se a reabertura for realizada com sucesso, retornará <c>true</c>, caso contrário, <c>false</c>.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<bool> Notificar(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"lotes/{id}/notificar"))
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