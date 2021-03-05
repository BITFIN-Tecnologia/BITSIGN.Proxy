// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao.APIs
{
    /// <summary>
    /// Ajustes relacionados ao processo operacional das assinaturas digitais, callbacks, etc.
    /// </summary>
    public class Configuracoes : API
    {
        /// <summary>
        /// Inicializa a API de configurações.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        public Configuracoes(HttpClient proxy)
            : base(proxy) { }

        /// <summary>
        /// Atualização de configurações.
        /// </summary>
        /// <param name="configuracao">Objeto com os parâmetros para serem atualizados.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        public async Task Atualizar(DTOs.Configuracao configuracao, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Put, "configuracoes")
            {
                Content = new StringContent(Serializador.Serializar(configuracao, this.FormatoDeSerializacao.ToString()))
            })
            {
                await Executar(requisicao, resposta =>
                {
                    resposta.EnsureSuccessStatusCode();
                }, cancellationToken);
            }
        }

        /// <summary>
        /// Renova a chave de integração entre sistemas.
        /// </summary>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Retornará a nova chave de integração gerada pelo serviço.</returns>
        public async Task<string> RenovarChave(CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Patch, "configuracoes/renovarchave"))
                return await Executar(
                    requisicao,
                    async resposta => await resposta.Content.ReadAsStringAsync(cancellationToken),
                    cancellationToken);
        }

        internal FormatoDeSerializacao FormatoDeSerializacao { get; init; } = FormatoDeSerializacao.Json;
    }
}