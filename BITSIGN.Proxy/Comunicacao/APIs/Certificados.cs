// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
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
        /// Validação de Certificado.
        /// </summary>
        /// <param name="analise">Parâmetros para análise do certificado.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Informações sobre o certificado e status de validação.</returns>
        public async Task<DTOs.ResultadoDaAnalise> Validar(DTOs.Analise analise, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, "certificados/validar")
            {
                Content = new StringContent(Serializador.Serializar(analise, this.FormatoDeSerializacao))
            })
            {
                requisicao.Content.Headers.ContentType = this.MimeType;

                return await this.Executar(requisicao, async resposta =>
                {
                    return await resposta.Content.ReadAs<DTOs.ResultadoDaAnalise>(cancellationToken);
                }, cancellationToken);
            }
        }
    }
}