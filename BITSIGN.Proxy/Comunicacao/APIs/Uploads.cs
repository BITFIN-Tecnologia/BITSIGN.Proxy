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
    /// Armazenamento de arquivos para posterior assinatura.
    /// </summary>
    public class Uploads : API
    {
        /// <summary>
        /// Inicializa a API de uploads.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        internal Uploads(HttpClient proxy)
            : base(proxy) { }

        /// <summary>
        /// Upload de Arquivos.
        /// </summary>
        /// <param name="nomeDoArquivo">Nome do Arquivo.</param>
        /// <param name="conteudoDoArquivo">Conteúdo do Arquivo.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Dados do arquivo armazenado.</returns>
        public async Task<DTOs.Upload> Enviar(string nomeDoArquivo, byte[] conteudoDoArquivo, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, "uploads")
            {
                Content = new MultipartFormDataContent()
                {
                    { new ByteArrayContent(conteudoDoArquivo), "file", nomeDoArquivo }
                }
            })
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return await resposta.Content.ReadAs<DTOs.Upload>(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Download de Arquivos.
        /// </summary>
        /// <param name="id">Identificador do Arquivo.</param>
        /// <param name="nomeDoArquivo">Nome do Arquivo enviado.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Conteúdo do Arquivo (em bytes).</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<byte[]> Download(Guid id, string nomeDoArquivo, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"uploads/{id}/{nomeDoArquivo}"))
            {
                return await base.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return await resposta.Content.ReadAsByteArrayAsync(cancellationToken);
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