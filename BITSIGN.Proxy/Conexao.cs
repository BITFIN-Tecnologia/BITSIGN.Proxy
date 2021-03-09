// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
using System.Collections.Generic;

namespace BITSIGN.Proxy
{
    /// <summary>
    /// Informações necessárias para iniciar a comunicação com o serviço.
    /// </summary>
    public class Conexao
    {
        private static readonly IDictionary<Ambiente, string> apis = new Dictionary<Ambiente, string>(2)
        {
            { Ambiente.Sandbox, "http://localhost:33664/api/{0}/" },
            { Ambiente.Producao, "http://localhost:33664/api/{0}/" },
        };

        private static readonly IDictionary<Ambiente, Uri> status = new Dictionary<Ambiente, Uri>(2)
        {
            { Ambiente.Sandbox, new Uri("http://localhost:33664/status") },
            { Ambiente.Producao, new Uri("http://localhost:33664/status") },
        };

        /// <summary>
        /// Inicializa a conexão com os dados sendo extraídos de um repositório.
        /// </summary>
        /// <param name="configuracao">Implementação que deve ser utilizada para localização das configurações.</param>
        /// <exception cref="ArgumentNullException">Se o parâmetro <paramref name="configuracao"/> for nulo.</exception>
        public Conexao(IConfiguracao configuracao)
            : this(configuracao.Ambiente, configuracao.Versao, configuracao.CodigoDoContratante, configuracao.ChaveDeIntegracao, configuracao.FormatoDeSerializacao)
        {
            this.Timeout = configuracao.Timeout;
        }

        /// <summary>
        /// Inicializa a conexão com o mínimo necessário para estabelecer a comunicação com um dos <see cref="Proxy.Ambiente"/>s.
        /// </summary>
        /// <param name="ambiente">Ambiente de produção ou de testes (Sandbox).</param>
        /// <param name="versao">Versão da API que deve ser utilizada.</param>
        /// <param name="codigoDoContratante">Código exclusivo do contratante.</param>
        /// <param name="chaveDeIntegracao">Chave do contratante para integração entre sistemas.</param>
        /// <param name="formato">Define como será serializado o conteúdo das mensagens trocadas com os serviços. O padrão é <see cref="FormatoDeSerializacao.Json"/>.</param>
        /// <exception cref="ArgumentException">Se o <paramref name="codigoDoContratante"/> ou o <paramref name="chaveDeIntegracao"/> forem <see cref="Guid.Empty"/>.</exception>
        public Conexao(Ambiente ambiente, string versao, Guid codigoDoContratante, string chaveDeIntegracao, FormatoDeSerializacao formato = FormatoDeSerializacao.Json)
        {
            this.Ambiente = ambiente;
            this.Versao = versao;

            this.CodigoDoContratante =
                codigoDoContratante != Guid.Empty ? codigoDoContratante : throw new ArgumentException("Código do Contratante não informado.", nameof(codigoDoContratante));

            this.ChaveDeIntegracao =
                !string.IsNullOrWhiteSpace(chaveDeIntegracao) ? chaveDeIntegracao : throw new ArgumentException("Chave de Integração não informada.", nameof(chaveDeIntegracao));

            this.FormatoDeSerializacao = formato;
        }

        /// <summary>
        /// Ambiente de Sandbox ou Produção.
        /// </summary>
        public Ambiente Ambiente { get; }

        /// <summary>
        /// Versão da API.
        /// </summary>
        public string Versao { get; }

        /// <summary>
        /// Código do Contratante.
        /// </summary>
        public Guid CodigoDoContratante { get; }

        /// <summary>
        /// Chave de Integração gerado para o contratante.
        /// </summary>
        public string ChaveDeIntegracao { get; }

        /// <summary>
        /// Formato da serialização das mensagens trocadas com os serviços.
        /// </summary>
        public FormatoDeSerializacao FormatoDeSerializacao { get; }

        /// <summary>
        /// Define o tempo máximo de espera permitido para executar uma requisição. O tempo padrão é de 100 segundos.
        /// </summary>
        public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(100);

        /// <summary>
        /// Endereço HTTP para o serviço, variando de acordo com o <see cref="Ambiente"/>.
        /// </summary>
        public Uri Url => new Uri(string.Format(apis[this.Ambiente], this.Versao));

        /// <summary>
        /// Endpoint que resume o status atual dos serviços e seus recursos.
        /// </summary>
        public Uri Status => status[this.Ambiente];
    }
}