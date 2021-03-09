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

        protected Guid CodigoDoContratante { get; } = new Guid("F54D4A2F-A7E6-4A06-9455-A04C7938F601");

        protected string ChaveDeIntegracao { get; set; } = "TmprNFlXVTNaV1V0TXpneE9DMDBNV0U0TFRrM05tRXRPV1kwT1RZeU16QXlObUl4";

        protected Conexao Conexao { get; }
    }
}