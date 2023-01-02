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
        public AppSettingsXml()
        {
            var config = ConfigurationManager.AppSettings;

            this.Ambiente = Enum.Parse<Ambiente>(config["BITSIGN.Proxy.Conexao.Ambiente"]);
            this.Url = config["BITSIGN.Proxy.Conexao.Url"];
            this.Status = config["BITSIGN.Proxy.Conexao.Status"];
            this.Versao = config["BITSIGN.Proxy.Conexao.Versao"];
            this.CodigoDoContratante = Guid.Parse(config["BITSIGN.Proxy.Conexao.CodigoDoContratante"]);
            this.ChaveDeIntegracao = config["BITSIGN.Proxy.Conexao.ChaveDeIntegracao"];
            this.FormatoDeSerializacao = Enum.Parse<FormatoDeSerializacao>(config["BITSIGN.Proxy.Conexao.FormatoDeSerializacao"]);
            this.Timeout = TimeSpan.Parse(config["BITSIGN.Proxy.Conexao.Timeout"]);
        }

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
        /// Chave de Integração gerado para o contratante.
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