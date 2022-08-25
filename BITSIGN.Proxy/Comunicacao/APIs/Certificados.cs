// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao.APIs
{
    /// <summary>
    /// Análise e manipulação de certificados.
    /// </summary>
    public class Certificados : API
    {
        /// <summary>
        /// Inicializa a API de certificados.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        /// <param name="formato">Formato para serialização dos objetos.</param>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        internal Certificados(HttpClient proxy, FormatoDeSerializacao formato)
            : base(proxy, formato) { }

        /// <summary>
        /// Emissão de Certificado.
        /// </summary>
        /// <param name="parametros">Informações para emissão do certificado.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Arquivo PFX codificado em Base64.</returns>
        public async Task<string> Emitir(DTOs.EmissaoDeCertificado parametros, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, "certificados/emitir")
            {
                Content = new StringContent(Serializador.Serializar(parametros, this.FormatoDeSerializacao))
            })
            {
                requisicao.Content.Headers.ContentType = this.MimeType;

                return await base.Executar(requisicao, async resposta =>
                {
                    try
                    {
                        return await resposta.Content.ReadAsStringAsync(cancellationToken);
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Titularidade do Certificado.
        /// </summary>
        /// <param name="id">Identificador do Certificado.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Arquivo PDF com o Termo de Titularidade.</returns>
        public async Task<byte[]> TermoDeTitularidade(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"certificados/{id}/titularidade"))
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

        /// <summary>
        /// Revogação de Certificado.
        /// </summary>
        /// <param name="id">Identificador do Certificado.</param>
        /// <param name="parametros">Dados complementares para revogar o certificado.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns></returns>
        public async Task<bool> Revogar(Guid id, DTOs.RevogacaoDeCertificado parametros, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, $"certificados/{id}/revogar")
            {
                Content = new StringContent(Serializador.Serializar(parametros, this.FormatoDeSerializacao))
            })
            {
                requisicao.Content.Headers.ContentType = this.MimeType;

                return await this.Executar(requisicao, resposta => Task.FromResult(resposta.IsSuccessStatusCode), cancellationToken);
            }
        }

        /// <summary>
        /// Validação de Certificado.
        /// </summary>
        /// <param name="analise">Parâmetros para análise do certificado.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Informações sobre o certificado e status de validação.</returns>
        public async Task<DTOs.ResultadoDaAnaliseDeCertificado> Validar(DTOs.AnaliseDeCertificado analise, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, "certificados/validar")
            {
                Content = new StringContent(Serializador.Serializar(analise, this.FormatoDeSerializacao))
            })
            {
                requisicao.Content.Headers.ContentType = this.MimeType;

                return await this.Executar(requisicao, async resposta =>
                {
                    return await resposta.Content.ReadAs<DTOs.ResultadoDaAnaliseDeCertificado>(cancellationToken);
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Relação de Certificados Emitidos.
        /// </summary>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Certificados Emitidos.</returns>
        public async Task<IEnumerable<DTOs.Emissao>> Validar(CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, "certificados/relacao"))
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    return await resposta.Content.ReadAs<IEnumerable<DTOs.Emissao>>(cancellationToken);
                }, cancellationToken);
            }
        }

        /// <summary>
        /// "Cadeia Privada de Certificação.
        /// </summary>
        /// <param name="id">Identificador da Cadeia Privada (Organização).</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Certificados Raiz e Intermediário(s) em formato P7B.</returns>
        public async Task<byte[]> Cadeia(Guid id, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"certificados/cadeia/privada/{id}"))
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

        /// <summary>
        /// Repositório de Certificados Revogados.
        /// </summary>
        /// <param name="alias">Alias da Organização que deseja baixar a relação de certificados revogados.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Arquivo CRL.</returns>
        public async Task<byte[]> Crl(string alias, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Get, $"certificados/crls/{alias}.crl"))
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