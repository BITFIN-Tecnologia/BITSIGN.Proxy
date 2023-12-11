// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BITSIGN.Proxy.Comunicacao.APIs
{
    /// <summary>
    /// Funcionalidades periféricas para documentos e assinaturas.
    /// </summary>
    public class Utilitarios : API
    {
        /// <summary>
        /// Inicializa a API de utilitários.
        /// </summary>
        /// <param name="proxy">Instância da classe <see cref="HttpClient"/> gerada pelo proxy.</param>
        internal Utilitarios(HttpClient proxy)
            : base(proxy) { }

        /// <summary>
        /// Conformidade de PDF.
        /// </summary>
        /// <param name="nomeDoArquivo">Nome do Arquivo.</param>
        /// <param name="conteudo">Conteúdo do Arquivo.</param>
        /// <param name="cancellationToken">Instrução para eventual cancelamento da requisição.</param>
        /// <returns>Dados da análise de conformidade.</returns>
        public async Task<DTOs.Conformidade> Conformidade(string nomeDoArquivo, byte[] conteudo, CancellationToken cancellationToken = default)
        {
            using (var requisicao = new HttpRequestMessage(HttpMethod.Post, "utilitarios/conformidade")
            {
                Content = new MultipartFormDataContent()
                {
                    { new ByteArrayContent(conteudo), "file", nomeDoArquivo }
                }
            })
            {
                return await this.Executar(requisicao, async resposta =>
                {
                    return await resposta.Content.ReadAs<DTOs.Conformidade>(cancellationToken);
                }, cancellationToken);
            }
        }
    }
}