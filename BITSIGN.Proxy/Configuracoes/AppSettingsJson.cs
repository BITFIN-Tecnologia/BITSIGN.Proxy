// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BITSIGN.Proxy.Configuracoes
{
    /// <summary>
    /// Configurações baseadas no arquivo appsettings.json (JSON).
    /// </summary>
    public sealed class AppSettingsJson : IConfiguracao
    {
        /// <summary>
        /// Inicializa as configurações extraindo as informações do arquivo appsettings.json.
        /// </summary>
        /// <example>
        /// <code>
        ///"BITSIGN.Proxy": {
        ///  "Conexao": {
        ///    "Ambiente": "Sandbox",
        ///    "Versao": "v1",
        ///    "CodigoDoContratante": "985e0702-e94a-4954-b7a8-1f28c73c8122",
        ///    "ChaveDeIntegracao": "TWpZd00yTXpPVGN0TmpFMk9TMDBaRGRqTFdFMk1XTXROR1kzWkRVM01qTmhNR0Zq",
        ///    "FormatoDeSerializacao": "Json",
        ///    "Timeout": "00:00:10"
        ///  }
        ///}
        /// </code>
        /// </example>
        public AppSettingsJson()
        {
            var config = 
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                    .Build();

            this.Ambiente = Enum.Parse<Ambiente>(config["BITSIGN.Proxy:Conexao:Ambiente"]);
            this.Url = config["BITSIGN.Proxy:Conexao:Url"];
            this.Status = config["BITSIGN.Proxy:Conexao:Status"];
            this.Versao = config["BITSIGN.Proxy:Conexao:Versao"];
            this.CodigoDoContratante = Guid.Parse(config["BITSIGN.Proxy:Conexao:CodigoDoContratante"]);
            this.ChaveDeIntegracao = config["BITSIGN.Proxy:Conexao:ChaveDeIntegracao"];
            this.FormatoDeSerializacao = Enum.Parse<FormatoDeSerializacao>(config["BITSIGN.Proxy:Conexao:FormatoDeSerializacao"]);
            this.Timeout = TimeSpan.Parse(config["BITSIGN.Proxy:Conexao:Timeout"]);
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