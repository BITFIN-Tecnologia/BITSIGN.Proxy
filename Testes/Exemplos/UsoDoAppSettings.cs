// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.Utilitarios;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public class UsoDoAppSettings : Exemplo
    {
        public override async Task Executar(CancellationToken cancellationToken = default)
        {
            using (var proxy = new ProxyDoServico(new Conexao(new AppSettings())))
            {
                //Acesso aos Serviços

                await Task.CompletedTask;
            }
        }

        public class AppSettings : IConfiguracao
        {
            public AppSettings()
            {
                var config = ConfigurationManager.AppSettings;

                this.Ambiente = Enum.Parse<Ambiente>(config["BITSIGN.Proxy.Conexao.Ambiente"]);
                this.CodigoDoContratante = Guid.Parse(config["BITSIGN.Proxy.Conexao.CodigoDoContratante"]);
                this.ChaveDeIntegracao = config["BITSIGN.Proxy.Conexao.ChaveDeIntegracao"];
                this.FormatoDeSerializacao = Enum.Parse<FormatoDeSerializacao>(config["BITSIGN.Proxy.Conexao.FormatoDeSerializacao"]);
                this.Timeout = TimeSpan.Parse(config["BITSIGN.Proxy.Conexao.Timeout"]);
            }

            public Ambiente Ambiente { get; init; }

            public Guid CodigoDoContratante { get; init; }

            public string ChaveDeIntegracao { get; init; }

            public FormatoDeSerializacao FormatoDeSerializacao { get; init; }

            public TimeSpan Timeout { get; init; }
        }
    }
}