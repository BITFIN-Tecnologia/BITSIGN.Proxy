// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (APIs) da BITSIGN.

using BITSIGN.Proxy.Utilitarios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BITSIGN.Proxy.Configuracoes
{
    /// <summary>
    /// Configurações baseadas no arquivo appsettings.json (JSON).
    /// </summary>
    public sealed class AppSettingsJson : Configuracao
    {
        /// <summary>
        /// Inicializa as configurações extraindo as informações do arquivo appsettings.json.
        /// </summary>
        /// <example>
        /// <code>
        ///{
        ///  "BITSIGN.Proxy": {
        ///    "Conexoes": [
        ///      {
        ///        "Nome": "Aplicação Xpto 1",
        ///        "Ambiente": "Sandbox",
        ///        "Versao": "v1",
        ///        "CodigoDoContratante": "985e0702-e94a-4954-b7a8-1f28c73c8122",
        ///        "CodigoDaAplicacao": "98b4307e-51d4-4f2f-88da-cbb23b903db5",
        ///        "ChaveDeIntegracao": "TWpZd00yTXpPVGN...zWkRVM01qTmhNR0Zq",
        ///        "FormatoDeSerializacao": "Json",
        ///        "Timeout": "00:00:10"
        ///      },
        ///      {
        ///        "Nome": "Aplicação Xpto 2",
        ///        "Ambiente": "Sandbox",
        ///        "Versao": "v1",
        ///        "CodigoDoContratante": "985e0702-e94a-4954-b7a8-1f28c73c8122",
        ///        "CodigoDaAplicacao": "d867b917-3564-4839-a9f0-1fccf4af1852",
        ///        "ChaveDeIntegracao": "zWkRVM01qTmhNR0Zq...TWpZd00yTXpPVGN",
        ///        "FormatoDeSerializacao": "Xml",
        ///        "Timeout": "00:00:20"
        ///      }
        ///    ]
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

            var conexoes =
                from c in config.GetSection("BITSIGN.Proxy:Conexoes").GetChildren()
                select new Conexao(
                    c["Nome"],
                    Enum.Parse<Ambiente>(c["Ambiente"]),
                    c["Versao"],
                    Guid.Parse(c["CodigoDoContratante"]),
                    Guid.Parse(c["CodigoDaAplicacao"]),
                    c["ChaveDeIntegracao"],
                    Enum.Parse<FormatoDeSerializacao>(c["FormatoDeSerializacao"]),
                    TimeSpan.Parse(c["Timeout"]));

            if (VerificarDuplicidades(conexoes, out var nome))
                throw new InvalidOperationException($"Existem duas ou mais conexões com o nome \"{nome}\".");

            this.Conexoes = conexoes;
        }

        private static bool VerificarDuplicidades(IEnumerable<Conexao> conexoes, out string nome)
        {
            nome = conexoes.GroupBy(c => c.Nome).FirstOrDefault(c => c.Count() > 1)?.Key;

            return nome != null;
        }
    }
}