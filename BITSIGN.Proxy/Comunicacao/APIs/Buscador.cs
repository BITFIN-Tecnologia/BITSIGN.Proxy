// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System.Collections.Generic;
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
        public Buscador(HttpClient proxy, FormatoDeSerializacao formato)
            : base(proxy)
        {
            this.FormatoDeSerializacao = formato;
            this.MimeType = $"application/{formato.ToString().ToLower()}";
        }

        /// <summary>
        /// Relação de Lotes.
        /// </summary>
        /// <param name="parametros">Parâmetros que serão utilizados para filtrar das consultas.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Relação de Lotes que satisfazem os critérios de busca.</returns>
        public async Task<IEnumerable<DTOs.Lote>> Lotes(DTOs.ParametrosDeBusca parametros, CancellationToken cancellationToken = default) =>
            await Buscar<DTOs.Lote>("lotes", parametros, cancellationToken);

        /// <summary>
        /// Relação de Documentos.
        /// </summary>
        /// <param name="parametros">Parâmetros que serão utilizados para filtrar das consultas.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Relação de Documentos que satisfazem os critérios de busca.</returns>
        public async Task<IEnumerable<DTOs.Documento>> Documentos(DTOs.ParametrosDeBusca parametros, CancellationToken cancellationToken = default) =>
            await Buscar<DTOs.Documento>("documentos", parametros, cancellationToken);

        /// <summary>
        /// Relação de Notificações.
        /// </summary>
        /// <param name="parametros">Parâmetros que serão utilizados para filtrar das consultas.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Relação de Notificações que satisfazem os critérios de busca.</returns>
        public async Task<IEnumerable<DTOs.Notificacao>> Notificacoes(DTOs.ParametrosDeBusca parametros, CancellationToken cancellationToken = default) =>
            await Buscar<DTOs.Notificacao>("notificacoes", parametros, cancellationToken);

        /// <summary>
        /// Relação de Callbacks.
        /// </summary>
        /// <param name="parametros">Parâmetros que serão utilizados para filtrar das consultas.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Relação de Callbacks que satisfazem os critérios de busca.</returns>
        public async Task<IEnumerable<DTOs.Notificacao>> Callbacks(DTOs.ParametrosDeBusca parametros, CancellationToken cancellationToken = default) =>
            await Buscar<DTOs.Notificacao>("callbacks", parametros, cancellationToken);

        private async Task<IEnumerable<T>> Buscar<T>(string path, DTOs.ParametrosDeBusca parametros, CancellationToken cancellationToken) where T : class
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, $"buscador/{path}")
            {
                Content = new StringContent(Serializador.Serializar(parametros, this.FormatoDeSerializacao.ToString()), Encoding.UTF8, this.MimeType)
            })
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    resposta.EnsureSuccessStatusCode();

                    return await resposta.Content.ReadAs<IEnumerable<T>>(cancellationToken);
                }, cancellationToken);
            }
        }

        private FormatoDeSerializacao FormatoDeSerializacao { get; init; }

        private string MimeType { get; init; }
    }
}