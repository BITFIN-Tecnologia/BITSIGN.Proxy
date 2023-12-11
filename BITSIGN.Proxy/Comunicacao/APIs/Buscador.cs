// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao.APIs
{
    /// <summary>
    /// Buscador avançado de recursos (lotes, documentos, etc.)
    /// </summary>
    public class Buscador : API
    {
        /// <summary>
        /// Inicializa a API de relatórios.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        /// <param name="formato">Formato para serialização dos objetos.</param>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        internal Buscador(HttpClient proxy, FormatoDeSerializacao formato)
            : base(proxy, formato) { }

        /// <summary>
        /// Relação de Lotes.
        /// </summary>
        /// <param name="parametros">Parâmetros que serão utilizados para filtrar das consultas.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Relação de Lotes que satisfazem os critérios de busca.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<DTOs.Lotes> Lotes(DTOs.ParametrosDeBusca parametros, CancellationToken cancellationToken = default) =>
            await Buscar<DTOs.Lotes>("lotes", parametros, cancellationToken);

        /// <summary>
        /// Relação de Documentos.
        /// </summary>
        /// <param name="parametros">Parâmetros que serão utilizados para filtrar das consultas.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Relação de Documentos que satisfazem os critérios de busca.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<DTOs.Documentos> Documentos(DTOs.ParametrosDeBusca parametros, CancellationToken cancellationToken = default) =>
            await Buscar<DTOs.Documentos>("documentos", parametros, cancellationToken);

        /// <summary>
        /// Relação de Notificações.
        /// </summary>
        /// <param name="parametros">Parâmetros que serão utilizados para filtrar das consultas.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Relação de Notificações que satisfazem os critérios de busca.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<DTOs.Notificacoes> Notificacoes(DTOs.ParametrosDeBusca parametros, CancellationToken cancellationToken = default) =>
            await Buscar<DTOs.Notificacoes>("notificacoes", parametros, cancellationToken);

        /// <summary>
        /// Relação de Callbacks.
        /// </summary>
        /// <param name="parametros">Parâmetros que serão utilizados para filtrar das consultas.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Relação de Callbacks que satisfazem os critérios de busca.</returns>
        /// <exception cref="ErroNaRequisicao">Exceção disparada se alguma falha ocorrer durante a requisição ou em seu processamento.</exception>
        public async Task<DTOs.Notificacoes> Callbacks(DTOs.ParametrosDeBusca parametros, CancellationToken cancellationToken = default) =>
            await Buscar<DTOs.Notificacoes>("callbacks", parametros, cancellationToken);

        private async Task<T> Buscar<T>(string path, DTOs.ParametrosDeBusca parametros, CancellationToken cancellationToken) where T : class
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, $"buscador/{path}")
            {
                Content = new StringContent(Serializador.Serializar(parametros, this.FormatoDeSerializacao), Encoding.UTF8, this.MimeType.MediaType)
            })
            {
                return await this.Executar(requisicao, async resposta => await resposta.Content.ReadAs<T>(cancellationToken), cancellationToken);
            }
        }
    }
}