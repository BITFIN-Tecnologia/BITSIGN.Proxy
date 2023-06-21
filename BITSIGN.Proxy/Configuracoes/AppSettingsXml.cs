// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
using System.Configuration;

namespace BITSIGN.Proxy.Configuracoes
{
    /// <summary>
    /// Configurações baseadas no arquivo app.config (XML).
    /// </summary>
    public sealed class AppSettingsXml : IConfiguracao
    {
        /// <summary>
        /// Inicializa as configurações extraindo as informações do arquivo app.config.
        /// </summary>
        /// <example>
        /// <code>
        /// <appSettings>
        ///     <add key="BITSIGN.Proxy.Conexao.Nome"
        ///             value="Aplicação Xpto" />
        ///     <add key="BITSIGN.Proxy.Conexao.Ambiente"
        ///             value="Sandbox" />
        ///     <add key="BITSIGN.Proxy.Conexao.Versao"
        ///             value="v1" />
        ///     <add key="BITSIGN.Proxy.Conexao.CodigoDoContratante"
        ///             value="985e0702-e94a-4954-b7a8-1f28c73c8122" />
        ///     <add key="BITSIGN.Proxy.Conexao.CodigoDaAplicacao"
        ///             value="98b4307e-51d4-4f2f-88da-cbb23b903db5" />
        ///     <add key="BITSIGN.Proxy.Conexao.ChaveDeIntegracao"
        ///             value="TWpZd00yTXpPVGN0TmpFMk9TMDBaRGRqTFdFMk1XTXROR1kzWkRVM01qTmhNR0Zq" />
        ///     <add key="BITSIGN.Proxy.Conexao.FormatoDeSerializacao"
        ///             value="Json" />
        ///     <add key="BITSIGN.Proxy.Conexao.Timeout"
        ///             value="00:00:10" />
        /// </appSettings>
        /// </code>
        /// </example>
        public AppSettingsXml()
        {
            var config = ConfigurationManager.AppSettings;

            this.Nome = config["BITSIGN.Proxy.Conexao.Nome"];
            this.Ambiente = Enum.Parse<Ambiente>(config["BITSIGN.Proxy.Conexao.Ambiente"]);
            this.Url = config["BITSIGN.Proxy.Conexao.Url"];
            this.Status = config["BITSIGN.Proxy.Conexao.Status"];
            this.Versao = config["BITSIGN.Proxy.Conexao.Versao"];
            this.CodigoDoContratante = Guid.Parse(config["BITSIGN.Proxy.Conexao.CodigoDoContratante"]);
            this.CodigoDaAplicacao = Guid.Parse(config["BITSIGN.Proxy.Conexao.CodigoDaAplicacao"]);
            this.ChaveDeIntegracao = config["BITSIGN.Proxy.Conexao.ChaveDeIntegracao"];
            this.FormatoDeSerializacao = Enum.Parse<FormatoDeSerializacao>(config["BITSIGN.Proxy.Conexao.FormatoDeSerializacao"]);
            this.Timeout = TimeSpan.Parse(config["BITSIGN.Proxy.Conexao.Timeout"]);
        }

        /// <summary>
        /// Identifica à qual aplicação se refere a conexão.
        /// </summary>
        public string Nome { get; init; }

        /// <summary>
        /// Ambiente de Sandbox ou Produção.
        /// </summary>
        public Ambiente Ambiente { get; init; }

        /// <summary>
        /// Endereço base (HTTP) das APIs. Somente é utilizado quando a solução estiver hospedada localmente.
        /// </summary>
        public string Url { get; init; }

        /// <summary>
        /// Endpoint que resume o status atual dos serviços e seus recursos. Somente é utilizado quando a solução estiver hospedada localmente.
        /// </summary>
        public string Status { get; init; }

        /// <summary>
        /// Versão das APIs.
        /// </summary>
        public string Versao { get; init; }

        /// <summary>
        /// Código do Contratante.
        /// </summary>
        public Guid CodigoDoContratante { get; init; }

        /// <summary>
        /// Código identificador da Aplicação.
        /// </summary>
        public Guid CodigoDaAplicacao { get; init; }

        /// <summary>
        /// Chave de integração da Aplicação.
        /// </summary>
        public string ChaveDeIntegracao { get; init; }

        /// <summary>
        /// Formato da serialização das mensagens trocadas com os serviços.
        /// </summary>
        public FormatoDeSerializacao FormatoDeSerializacao { get; init; }

        /// <summary>
        /// Define o tempo máximo de espera permitido para executar uma requisição.
        /// </summary>
        public TimeSpan Timeout { get; init; }
    }
}