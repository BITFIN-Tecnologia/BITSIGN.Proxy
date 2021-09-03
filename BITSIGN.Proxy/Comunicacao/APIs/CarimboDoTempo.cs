// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao.APIs
{
    /// <summary>
    /// Gerador de carimbos do tempo para assinaturas e documentos digitais.
    /// </summary>
    public class CarimboDoTempo : API
    {
        /// <summary>
        /// Inicializa a API de carimbos do tempo.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        internal CarimboDoTempo(HttpClient proxy)
            : base(proxy) { }

        /// <summary>
        /// Dado um array de bytes representando a assinatura, este método é capaz de gerar o respectivo carimbo do tempo.
        /// </summary>
        /// <param name="assinatura">Assinatura para qual o carimbo deve ser emitido.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Array de bytes contendo o carimbo do tempo já calculado para a assinatura submetida.</returns>
        public async Task<byte[]> Emitir(byte[] assinatura, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, $"carimbodotempo")
            {
                Content = new ByteArrayContent(assinatura)
            })
            {
                return await this.Executar(requisicao, async resposta => await resposta.Content.ReadAsByteArrayAsync(cancellationToken), cancellationToken);
            }
        }
    }
}