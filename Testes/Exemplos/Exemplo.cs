// Copyright (c) 2021 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código de exemplo de consumo dos serviços (APIs) da BITSIGN utilizando a
// biblioteca/pacote BITFIN.BITSIGN.Proxy.

using BITSIGN.Proxy;
using BITSIGN.Proxy.Utilitarios;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testes.Exemplos
{
    public abstract class Exemplo
    {
        public Exemplo() =>
            this.Conexao = new Conexao(Ambiente.Sandbox, this.Versao, this.CodigoDoContratante, this.ChaveDeIntegracao, FormatoDeSerializacao.Json);

        public abstract Task Executar(CancellationToken cancellationToken = default);

        protected string Versao { get; } = "v1";

        protected Guid CodigoDoContratante { get; } = new Guid("613ECC3A-8A7D-4C9A-B7B1-D2451D69BE30");

        protected string ChaveDeIntegracao { get; set; } = "WWpBMk1tRmtaVFF0T0dSak1DMDBNelJqTFRnek1EY3RNR0ppTkRObE1UWTRaVGMw";

        protected Conexao Conexao { get; }
    }
}