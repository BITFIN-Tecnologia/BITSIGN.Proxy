﻿// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
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
    /// Download dos documentos enviados, assinados e manifestos de assinaturas.
    /// </summary>
    public class Documentos : API
    {
        /// <summary>
        /// Inicializa a API de documentos.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        internal Documentos(HttpClient proxy)
            : base(proxy) { }

        /// <summary>
        /// Detalhes do Documento.
        /// </summary>
        /// <param name="id">Identificador do Documento.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Retorna todas as informações do documento que foi submetido para assinatura.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<DTOs.Documento> Detalhes(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"documentos/{id}"))
            {
                return await base.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return await resposta.Content.ReadAs<DTOs.Documento>(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Exclusão do Documento.
        /// </summary>
        /// <remarks>Depois de excluído, se não houver mais documentos no lote, ele também será removido. A exclusão incidirá a cobrança das assinaturas que já foram, eventualmente, realizadas.</remarks>
        /// <param name="id">Identificador do Documento.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Se a exclusão for realizada com sucesso, retornará <c>true</c>, caso contrário, <c>false</c>.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<bool> Excluir(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Delete, $"documentos/{id}"))
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
        /// Documento Original.
        /// </summary>
        /// <param name="id">Identificador do Documento.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Um <c>array</c> de <see cref="byte"/>s que corresponde ao documento original enviado.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<byte[]> Original(Guid id, CancellationToken cancellationToken = default) =>
            await Executar(id, "original", cancellationToken);

        /// <summary>
        /// Documento Assinado.
        /// </summary>
        /// <param name="id">Identificador do Documento.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Um <c>array</c> de <see cref="byte"/>s que corresponde ao documento assinado.</returns>
        /// <remarks>Este arquivo é aquele que possui validade jurídica.</remarks>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<byte[]> Assinado(Guid id, CancellationToken cancellationToken = default) =>
            await Executar(id, "assinado", cancellationToken);

        /// <summary>
        /// Manifesto de Assinaturas.
        /// </summary>
        /// <param name="id">Identificador do Documento.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Um <c>array</c> de <see cref="byte"/>s que corresponde à um arquivo PDF com os detalhes dos assinantes e as características das assinaturas.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<byte[]> Manifesto(Guid id, CancellationToken cancellationToken = default) =>
            await Executar(id, "manifesto", cancellationToken);

        private async Task<byte[]> Executar(Guid id, string path, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"documentos/{id}/{path}"))
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