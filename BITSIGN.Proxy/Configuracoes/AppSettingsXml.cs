// Copyright (c) 2021 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using System;
using System.Configuration;

namespace BITSIGN.Proxy.Configuracoes
{
    /// <summary>
    /// Configurações baseadas no arquivo app/web.config (XML).
    /// </summary>
    public sealed class AppSettingsXml : Configuracao
    {
        /// <summary>
        /// Inicializa as configurações extraindo as informações do arquivo app/web.config.
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
        ///             value="TWpZd00yTXpPVGN...zWkRVM01qTmhNR0Zq" />
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

            this.Conexoes = new[]
            {
                new Conexao(
                    config["BITSIGN.Proxy.Conexao.Nome"],
                    Enum.Parse<Ambiente>(config["BITSIGN.Proxy.Conexao.Ambiente"]),
                    config["BITSIGN.Proxy.Conexao.Versao"],
                    Guid.Parse(config["BITSIGN.Proxy.Conexao.CodigoDoContratante"]),
                    Guid.Parse(config["BITSIGN.Proxy.Conexao.CodigoDaAplicacao"]),
                    config["BITSIGN.Proxy.Conexao.ChaveDeIntegracao"],
                    Enum.Parse<FormatoDeSerializacao>(config["BITSIGN.Proxy.Conexao.FormatoDeSerializacao"]),
                    TimeSpan.Parse(config["BITSIGN.Proxy.Conexao.Timeout"]))
            };
        }
    }
}