// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Comunicacao;
using BITSIGN.Proxy.Comunicacao.APIs;
using BITSIGN.Proxy.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BITSIGN.Proxy
{
    /// <summary>
    /// Abstrai toda a comunicação com os serviços (APIs) de assinaturas da BITSIGN.
    /// </summary>
    public class ProxyDoServico : IDisposable
    {
        private readonly HttpClient proxy;

        /// <summary>
        /// Inicializa o proxy.
        /// </summary>
        /// <param name="conexao">Dados da conexão com o ambiente.</param>
        /// <param name="logger">Instância de <see cref="ILogger"/> para gestão e armazenamento de logs gerados pelo proxy.</param>
        /// <param name="rastreioDeRequisicao">Gerador de códigos de rastreio de requisições.</param>
        /// <param name="webProxy">Especifica um proxy para ser utilizado ao realizar as requisições para os serviços.</param>
        /// <exception cref="ArgumentNullException">Se a <paramref name="conexao"/> não for informada.</exception>
        public ProxyDoServico(Conexao conexao, ILogger logger = null, IRastreio rastreioDeRequisicao = null, IWebProxy webProxy = null)
        {
            this.Conexao = conexao ?? throw new ArgumentNullException(nameof(conexao));

            this.proxy = new(new RastreioDaRequisicao(logger, rastreioDeRequisicao)
            {
                Proxy = webProxy,
                UseProxy = webProxy != null
            })
            {
                BaseAddress = conexao.Url,
                DefaultRequestVersion = HttpVersion.Version20,
                DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
                Timeout = conexao.Timeout
            };

            this.proxy.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{conexao.CodigoDoContratante}:{conexao.ChaveDeIntegracao}")));

            this.proxy.DefaultRequestHeaders.Add("Accept", $"application/{conexao.FormatoDeSerializacao.ToString().ToLower()}");

            this.Lotes = new(proxy, conexao.FormatoDeSerializacao);
            this.Documentos = new(proxy);
            this.Financeiro = new(proxy);
            this.Contratantes = new(proxy);
            this.Aplicacoes = new(proxy, conexao.FormatoDeSerializacao);
            this.Notificacoes = new(proxy);
            this.Anexos = new(proxy);
            this.Buscador = new(proxy, conexao.FormatoDeSerializacao);
            this.CarimboDoTempo = new(proxy);
            this.Dumps = new(proxy, conexao.Ambiente);
            this.Certificados = new(proxy, conexao.FormatoDeSerializacao);
            this.Status = new(conexao.Status);
            this.Uploads = new(proxy);
        }

        /// <summary>
        /// Dados de conexão com um dos <see cref="Ambiente"/>s disponíveis.
        /// </summary>
        public Conexao Conexao { get; }

        /// <summary>
        /// API de Lotes.
        /// </summary>
        public Lotes Lotes { get; }

        /// <summary>
        /// API de Documentos.
        /// </summary>
        public Documentos Documentos { get; }

        /// <summary>
        /// API da área Financeira.
        /// </summary>
        public Financeiro Financeiro { get; }

        /// <summary>
        /// API de Contratantes.
        /// </summary>
        public Contratantes Contratantes { get; }

        /// <summary>
        /// API de Aplicações.
        /// </summary>
        public Aplicacoes Aplicacoes { get; }

        /// <summary>
        /// API de Notificações.
        /// </summary>
        public Notificacoes Notificacoes { get; }

        /// <summary>
        /// API de Anexos.
        /// </summary>
        public Anexos Anexos { get; }

        /// <summary>
        /// API de Busca de Recursos.
        /// </summary>
        public Buscador Buscador { get; }

        /// <summary>
        /// API para emissão de carimbos do tempo.
        /// </summary>
        public CarimboDoTempo CarimboDoTempo { get; }

        /// <summary>
        /// API para visualização de Dumps.
        /// </summary>
        public Dumps Dumps { get; }

        /// <summary>
        /// API de Certificados.
        /// </summary>
        public Certificados Certificados { get; }

        /// <summary>
        /// API de status dos Serviços.
        /// </summary>
        public Status Status { get; }

        /// <summary>
        /// API de upload de Arquivos.
        /// </summary>
        public Uploads Uploads { get; }

        /// <summary>
        /// Encerra e remove os recursos de comunicação utilizados por esta classe.
        /// </summary>
        public void Dispose()
        {
            this.proxy?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}