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

        protected Guid CodigoDoContratante { get; } = new("F2BF5A7A-8AE1-46D1-AE16-AD2018FF59A0");

        protected string ChaveDeIntegracao { get; set; } = "2D9D6336501750788D8B8D7EFDEC75D960F31417";

        protected Conexao Conexao { get; }
    }
}